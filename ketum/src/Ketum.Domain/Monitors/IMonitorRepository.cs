using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Ketum.Monitors
{
    public interface IMonitorRepository : IRepository<Monitor, Guid>
    {
        Task<Monitor> GetAsync(
            Guid id,
            int skipCount,
            int maxResultCount,
            CancellationToken cancellationToken = default);

        Task<int> GetMonitorStepLogCountAsync(
            Guid monitorStepId,
            CancellationToken cancellationToken = default);
        
        Task<List<Monitor>> GetListAsync(
            string sorting,
            int skipCount,
            int maxResultCount,
            Guid userId,
            CancellationToken cancellationToken = default);

        Task<List<Monitor>> GetListByStepFilterAsync(
            MonitorStepTypes monitorStepTypes,
            CancellationToken cancellationToken = default);
    }
}