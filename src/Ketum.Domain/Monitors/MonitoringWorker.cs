using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Guids;
using Volo.Abp.Threading;

namespace Ketum.Monitors
{
    public class MonitoringWorker : AsyncPeriodicBackgroundWorkerBase
    {
        public MonitoringWorker(
            AbpTimer timer,
            IServiceScopeFactory serviceScopeFactory)
            : base(timer, serviceScopeFactory)
        {
            Timer.Period = 1000;
        }

        protected override async Task DoWorkAsync(PeriodicBackgroundWorkerContext workerContext)
        {
            var monitorRepository = workerContext.ServiceProvider.GetService<IMonitorRepository>();

            var monitor = await monitorRepository.GetAsync(Guid.Parse("E3076832-D653-79BC-002B-39F8403C4EB0"));

            monitor.SetName("stackoverflow-example");
            monitor.MonitorStep.Url = "http://stackoverflow.com/";
            
            await monitorRepository.UpdateAsync(monitor, true);
        }
    }
}