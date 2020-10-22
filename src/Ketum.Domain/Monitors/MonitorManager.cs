using System;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.Users;

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

            var monitors = await _monitorRepository.Where(x => x.CreatorId == userId).ToListAsync();

            var isExistingMonitorName = monitors.Any(x => x.Name == name);

            if (isExistingMonitorName)
            {
                throw new BusinessException(
                    message: "This project name is already in use. Please choose a different name.");
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
            [NotNull] string url)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrWhiteSpace(url, nameof(url));

            var monitor = await _monitorRepository.GetAsync(id);

            if (monitor.CreatorId != userId)
            {
                return null;
            }

            var monitors = await _monitorRepository.GetListAsync();

            monitors = monitors
                .Where(x => x.CreatorId == monitor.CreatorId)
                .Where(x => x.Id != id)
                .ToList();

            var isExistingMonitorName = monitors.Any(x => x.Name == name);

            if (isExistingMonitorName)
            {
                throw new BusinessException(
                    message: "This project name is already in use. Please choose a different name.");
            }

            monitor.SetName(name);
            monitor.MonitorStep.SetUrl(url);

            return await _monitorRepository.UpdateAsync(monitor);
        }
    }
}