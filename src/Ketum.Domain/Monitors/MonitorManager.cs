using System;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;

namespace Ketum.Monitors
{
    public class MonitorManager : KetumDomainServiceBase
    {
        private readonly IMonitorRepository _monitorRepository;

        public MonitorManager(IMonitorRepository monitorRepository)
        {
            _monitorRepository = monitorRepository;
        }

        public async Task<Monitor> CreateAsync(
            Guid monitorId,
            Guid monitorStepId,
            [NotNull] string name, 
            [NotNull] string url,
            int interval)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrWhiteSpace(url, nameof(url));

            var monitors = await _monitorRepository.GetListAsync();

            var isExistingMonitorName = monitors.Any(x => x.Name == name);

            if (isExistingMonitorName)
            {
                 throw new BusinessException(message: "This project name is already in use. Please choose a different name.");
            }

            var newMonitorStep = new MonitorStep(
                monitorStepId, 
                monitorId,
                url,
                interval,
                MonitorStepTypes.Request,
                MonitorStepStatusTypes.Unknown);

            var newMonitor = new Monitor(
                monitorId, 
                name,
                MonitorStatusTypes.Unknown,
                TestStatusTypes.Unknown,
                newMonitorStep);
            
            var newMonitorStepLog = new MonitorStepLog(
                GuidGenerator.Create(),
                monitorStepId,
                DateTime.Now,
                MonitorStepStatusTypes.Unknown,
                newMonitorStep.Interval);

            return await _monitorRepository.InsertAsync(newMonitor);
        }

        public async Task<Monitor> UpdateAsync(
            Guid id,
            [NotNull] string name,
            [NotNull] string url)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrWhiteSpace(url, nameof(url));

            var monitor = await _monitorRepository.GetAsync(id);

            if (monitor.Name == name)
            {
                throw  new BusinessException(message: "This monitor name already exists.");
            }

            monitor.SetName(name);
            monitor.MonitorStep.Url = url;

            return await _monitorRepository.UpdateAsync(monitor);
        }
    }
}