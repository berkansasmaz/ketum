using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Ketum.Monitors
{
    public interface IMonitorRepository : IRepository<Monitor, Guid>
    {
        Task<List<Monitor>> GetListAsync(
            string sorting,
            int skipCount,
            int maxResultCount,
            Guid userId,
            CancellationToken cancellationToken = default);

        Task<List<Monitor>> GetListByStepFilterAsync(
            int maxResultCount,
            MonitorStepTypes monitorStepTypes,
            CancellationToken cancellationToken = default);

        Task<int> GetCountByFilterAsync(
            Guid userId,
            CancellationToken cancellationToken = default);
    }
}