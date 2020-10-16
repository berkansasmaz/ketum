using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;

namespace Ketum.Monitors
{
    public interface IMonitorRepository : IBasicRepository<Monitor, Guid>
    {
        Task<List<Monitor>> GetListAsync(
            string sorting,
            int skipCount,
            int maxResultCount,
            [CanBeNull] string name = null,
            CancellationToken cancellationToken = default);

        Task<List<Monitor>> GetListByStepFilterAsync(
            int resultCount,
            MonitorStepTypes monitorStepTypes,
            CancellationToken cancellationToken = default);

        Task<int> GetCountByFilterAsync(
            string name,
            CancellationToken cancellationToken = default);
    }
}