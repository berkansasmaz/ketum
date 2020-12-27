using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Ketum.Monitors
{
    public interface IMonitorAppService : IApplicationService
    {
        Task<PagedResultDto<MonitorDto>> GetListAsync(GetMonitorsRequestInput input);

        Task<MonitorWithDetailsDto> GetAsync(Guid id, GetMonitorRequestInput input);

        Task<int> GetMonitorStepLogCountAsync(Guid monitorStepId);
        
        Task CreateAsync(CreateMonitorDto input);

        Task UpdateAsync(Guid id, UpdateMonitorDto input);

        Task DeleteAsync(Guid id);
    }
}