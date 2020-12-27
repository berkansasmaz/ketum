using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Ketum.Monitors
{
    public static class MonitorsEfCoreQueryableExtensions
    {
        public static IQueryable<Monitor> IncludeDetails(this IQueryable<Monitor> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                .Include(x => x.MonitorStep)
                .Include(x => x.MonitorStep.MonitorStepLogs);
        }
        
        public static IQueryable<Monitor> IncludeStepDetails(this IQueryable<Monitor> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                .Include(x => x.MonitorStep);
        }
    }
}