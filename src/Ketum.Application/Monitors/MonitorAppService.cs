using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Ketum.Monitors
{
    public class MonitorAppService : KetumAppService, IMonitorAppService
    {
        private readonly MonitorManager _monitorManager;
        private readonly IMonitorRepository _monitorRepository;

        public MonitorAppService(
            MonitorManager monitorManager, 
            IMonitorRepository monitorRepository)
        {
            _monitorManager = monitorManager;
            _monitorRepository = monitorRepository;
        }

        public async Task<PagedResultDto<MonitorDto>> GetListAsync(GetMonitorsRequestInput input)
        {
            var count = await _monitorRepository.GetCountByFilterAsync(input.Name);

            var monitors = await _monitorRepository.GetListAsync(
                input.Sorting,
                input.SkipCount,
                input.MaxResultCount,
                input.Name);

            return new PagedResultDto<MonitorDto>(
                count,
                ObjectMapper.Map<List<Monitor>, List<MonitorDto>>(monitors)
            );
        }

        public async Task<MonitorWithDetailsDto> GetAsync(Guid id)
        {
            var monitor = await _monitorRepository.GetAsync(id);

            return ObjectMapper.Map<Monitor, MonitorWithDetailsDto>(monitor);
        }

        public async Task CreateAsync(CreateMonitorDto input)
        {
            await _monitorManager.CreateAsync(
                GuidGenerator.Create(),
                GuidGenerator.Create(),
                input.Name,
                input.Url,
                input.Interval);
        }

        public async Task UpdateAsync(Guid id, UpdateMonitorDto input)
        {
            await _monitorManager.UpdateAsync(
                id,
                input.Name,
                input.Url);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _monitorRepository.DeleteAsync(id);
        }
    }
}