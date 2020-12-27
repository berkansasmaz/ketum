using System;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
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
            Guid userId,
            [NotNull] string name,
            [NotNull] string url,
            int interval)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrWhiteSpace(url, nameof(url));
            
            if (interval < 1)
            {
                throw new UserFriendlyException("Interval value cannot be less than one");
            }
            
            var isExistingMonitorName = _monitorRepository.Any(x => x.CreatorId == userId && x.Name.Equals(name));

            if (isExistingMonitorName)
            {
                throw new UserFriendlyException("This project name is already in use. Please choose a different name.");
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
                newMonitorStep);

            return await _monitorRepository.InsertAsync(newMonitor);
        }

        public async Task<Monitor> UpdateAsync(
            Guid id,
            Guid userId,
            [NotNull] string name,
            [NotNull] string url,
            int interval)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrWhiteSpace(url, nameof(url));

            if (interval < 1)
            {
                throw new UserFriendlyException("Interval value cannot be less than one");
            }

            var monitor = await _monitorRepository.GetAsync(id);

            if (monitor.CreatorId != userId)
            {
                return null;
            }
            
            var isExistingMonitorName = _monitorRepository
                .Any(x => x.CreatorId == monitor.CreatorId && x.Id != id && x.Name.Equals(name));

            if (isExistingMonitorName)
            {
                throw new UserFriendlyException("This project name is already in use. Please choose a different name.");
            }

            monitor.SetName(name);
            monitor.MonitorStep.SetUrl(url);
            monitor.MonitorStep.Interval = interval;

            return await _monitorRepository.UpdateAsync(monitor);
        }
    }
}