using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Ketum.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Ketum.Web
{
	public class KTBSMonitoring : IHostedService,	IDisposable
	{
   		public IServiceProvider Services { get; }    
		private	CancellationToken _token;
	
		public KTBSMonitoring(IServiceProvider services)
		{
			this.Services = services;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			_token = cancellationToken;
			DoWorkAsync();
			return Task.CompletedTask;
		}

		private async void DoWorkAsync()
		{
			 while (true)
            {
                using (var scope = Services.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<KTDBContext>();
                    var steps = await db.MonitorSteps
                                        .Where(x =>
                                            x.Type == KTDMonitorStepTypes.Request && x.Status != KTDMonitorStepStatusTypes.Processing
                                        )
                                        .OrderBy(x => x.LastCheckDate)
                                        .Take(20)
                                        .ToListAsync();

                    foreach (var step in steps)
                    {
                        var settings = step.SettingsAsRequest();
                        if (!string.IsNullOrEmpty(settings.Url))
                        {
                            var log = new KTDMonitorStepLog
                            {
                                MonitorId = step.MonitorId,
                                MonitorStepId = step.MonitorStepId,
                                StartDate = DateTime.UtcNow,
                                Interval = step.Interval,
                                Status = KTDMonitorStepStatusTypes.Processing
                            };
                            db.Add(log);
                            await db.SaveChangesAsync(_token);

                            try
                            {
                                var client = new HttpClient();
                                client.Timeout = TimeSpan.FromSeconds(15);
                                var result = await client.GetAsync(settings.Url, _token);
                                if (result.IsSuccessStatusCode)
                                {
                                    log.Status = KTDMonitorStepStatusTypes.Success;
                                }
                                else
                                {
                                    log.Status = KTDMonitorStepStatusTypes.Fail;
                                }
                            }
                            catch (HttpRequestException rex)
                            {
                                log.Log = rex.Message;
                                log.Status = KTDMonitorStepStatusTypes.Fail;
                            }
                            catch (Exception ex)
                            {
                                log.Log = ex.Message;
                                log.Status = KTDMonitorStepStatusTypes.Error;
                            }
                            finally
                            {
                                log.EndDate = DateTime.UtcNow;
                            }

                            if (log.Status == KTDMonitorStepStatusTypes.Success)
                                step.Status = KTDMonitorStepStatusTypes.Success;
                            else if (log.Status == KTDMonitorStepStatusTypes.Error)
                                step.Status = KTDMonitorStepStatusTypes.Error;
                            else
                                step.Status = KTDMonitorStepStatusTypes.Fail;
                        }
                        step.LastCheckDate = DateTime.UtcNow;
                        await db.SaveChangesAsync(_token);
                    }
                }
                await Task.Delay(500, _token);
            }
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}

		public void Dispose()
		{
			//Return
		}
	}
}