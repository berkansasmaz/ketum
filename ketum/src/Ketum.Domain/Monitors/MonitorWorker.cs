using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Guids;
using Volo.Abp.Threading;
using Volo.Abp.Uow;

namespace Ketum.Monitors
{
    public class MonitorWorker : AsyncPeriodicBackgroundWorkerBase
    {
        private readonly IDistributedEventBus _distributedEventBus;

        public MonitorWorker(
            AbpAsyncTimer timer,
            IServiceScopeFactory serviceScopeFactory, 
            IDistributedEventBus distributedEventBus)
            : base(timer, serviceScopeFactory)
        {
            _distributedEventBus = distributedEventBus;
            Timer.Period = (int) KetumConsts.MonitorWorkerPeriod.TotalMilliseconds;
        }

        [UnitOfWork]
        protected override async Task DoWorkAsync(PeriodicBackgroundWorkerContext workerContext)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            Logger.LogInformation("Starting at {0}: Monitoring the website’s health...", stopwatch.Elapsed.Milliseconds);

            var guidGenerator = workerContext.ServiceProvider.GetRequiredService<IGuidGenerator>();
            var unitOfWorkManager = workerContext.ServiceProvider.GetRequiredService<UnitOfWorkManager>();
            var monitorRepository = workerContext.ServiceProvider.GetRequiredService<IMonitorRepository>();
            var clientFactory = workerContext.ServiceProvider.GetRequiredService<IHttpClientFactory>();


            var monitors = await monitorRepository.GetListByStepFilterAsync(MonitorStepTypes.Request);

            monitors = monitors
                .Where(x =>
                    x.LastModificationTime == null
                        ? x.CreationTime.ToUniversalTime().AddSeconds(x.MonitorStep.Interval) < DateTime.UtcNow
                        : x.LastModificationTime?.ToUniversalTime().AddMinutes(x.MonitorStep.Interval) <
                          DateTime.UtcNow)
                .ToList();

            foreach (var monitor in monitors)
            {
                if (!monitor.MonitorStep.Url.IsNullOrEmpty())
                {
                    var logInterval = monitor.MonitorStep.Interval + TimeSpan.FromMilliseconds(Timer.Period).Minutes;
                    var monitorStepLog = new MonitorStepLog(
                        guidGenerator.Create(),
                        monitor.Id,
                        DateTime.UtcNow,
                        MonitorStepStatusTypes.Processing,
                        logInterval);

                    monitor.AddMonitorStepLog(monitorStepLog);

                    await monitorRepository.UpdateAsync(monitor);

                    await unitOfWorkManager.Current.SaveChangesAsync();

                    try
                    {
                        var client = clientFactory.CreateClient();
                        client.Timeout = TimeSpan.FromSeconds(15);
                        var response = await client.GetAsync(monitor.MonitorStep.Url);
                        if (response.IsSuccessStatusCode)
                        {
                            monitorStepLog.Status = MonitorStepStatusTypes.Success;
                        }
                        else
                        {
                            monitorStepLog.Status = MonitorStepStatusTypes.Fail;
                        }
                    }
                    catch (HttpRequestException rex)
                    {
                        monitorStepLog.SetLog(rex.Message);
                        monitorStepLog.Status = MonitorStepStatusTypes.Fail;
                    }
                    catch (Exception ex)
                    {
                        monitorStepLog.SetLog(ex.Message);
                        monitorStepLog.Status = MonitorStepStatusTypes.Error;
                    }
                    finally
                    {
                        monitorStepLog.EndDate = DateTime.UtcNow;
                    }

                    switch (monitorStepLog.Status)
                    {
                        case MonitorStepStatusTypes.Success:
                            monitor.MonitorStep.Status = MonitorStepStatusTypes.Success;
                            monitor.MonitorStatus = MonitorStatusTypes.Up;
                            break;
                        case MonitorStepStatusTypes.Error:
                            monitor.MonitorStep.Status = MonitorStepStatusTypes.Error;
                            monitor.MonitorStatus = MonitorStatusTypes.Warning;
                            break;
                        default:
                            monitor.MonitorStep.Status = MonitorStepStatusTypes.Fail;
                            monitor.MonitorStatus = MonitorStatusTypes.Down;
                            break;
                    }
                }

                monitor.LastModificationTime = DateTime.UtcNow;

                await monitorRepository.UpdateAsync(monitor);
                await unitOfWorkManager.Current.SaveChangesAsync();

                await PublishUserNotifyEvent(monitor);
            }

            stopwatch.Stop();
            var stopwatchElapsed = stopwatch.Elapsed;
            Logger.LogInformation("Completed at {0} ms: Monitoring the website’s health...", stopwatchElapsed.TotalMilliseconds);
        }

        private async Task PublishUserNotifyEvent(Monitor monitor)
        {
            await _distributedEventBus.PublishAsync(
                new MonitorEto(
                    monitor.CreatorId,
                    monitor.LastModificationTime,
                    monitor.Name,
                    monitor.MonitorStep.Url,
                    monitor.MonitorStatus)
            );
        }
    }
}