using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Ketum.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Ketum.Monitors
{
    public class EfCoreMonitorRepository : EfCoreRepository<KetumDbContext, Monitor, Guid>, IMonitorRepository
    {
        public EfCoreMonitorRepository(IDbContextProvider<KetumDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public Task<Monitor> GetAsync(
            Guid id, 
            int skipCount, 
            int maxResultCount,
            CancellationToken cancellationToken = default)
        {
            var query = DbSet
                .Where(x => x.Id == id)
                .Include(x => x.MonitorStep)
                .Include(x => x.MonitorStep.MonitorStepLogs
                    .OrderByDescending(x => x.StartDate)
                    .Skip(skipCount)
                    .Take(maxResultCount));
            return query.SingleAsync(GetCancellationToken(cancellationToken));
        }

        public Task<int> GetMonitorStepLogCountAsync(
            Guid monitorStepId, 
            CancellationToken cancellationToken = default)
        {
            return DbContext.MonitorStepLogs
                .CountAsync(x => x.MonitorStepId == monitorStepId, GetCancellationToken(cancellationToken));
        }

        public async Task<List<Monitor>> GetListAsync(
            string sorting,
            int skipCount,
            int maxResultCount,
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            var query = DbSet
                .IncludeStepDetails()
                .Where(x => x.CreatorId == userId)
                .OrderBy(string.IsNullOrWhiteSpace(sorting) ? Monitor.DefaultSorting : sorting);

            return await query.PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<List<Monitor>> GetListByStepFilterAsync(
            MonitorStepTypes monitorStepType,
            CancellationToken cancellationToken = default)
        {
            var query = DbSet
                .IncludeDetails(true)
                .Where(x => x.MonitorStep.Type == monitorStepType)
                .Where(x => x.MonitorStep.Status != MonitorStepStatusTypes.Processing)
                .OrderBy(x => x.LastModificationTime)
                .Take(KetumConsts.MaxMonitorsProcessedCount);

            return await query.ToListAsync(GetCancellationToken(cancellationToken));
        }

        public override IQueryable<Monitor> WithDetails()
        {
            return GetQueryable().IncludeDetails();
        }
    }
}