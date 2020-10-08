using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Ketum.Monitors
{
    public class MonitorStep : FullAuditedEntity<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; }

        public Guid MonitorId { get; protected set; }

        public string Url { get; protected set; }

        public int Interval { get; protected set; }

        public MonitorStepTypes Type { get; protected set; }
       
        public MonitorStepStatusTypes Status { get; protected set; }

        public MonitorStepLog MonitorStepLog { get; protected set; }

        protected MonitorStep()
        {
            
        }
    }
}