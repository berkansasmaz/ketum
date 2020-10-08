using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Ketum.Monitors
{
    public class Monitor : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; }

        public string Name { get; protected set; }

        public MonitorStatusTypes MonitorStatus { get; protected set; }

        public TestStatusTypes TestStatus { get; protected set; }

        public decimal UpTime { get; protected set; }

        public int LoadTime { get; protected set; }

        public int MonitorTime { get; protected set; }

        public ICollection<MonitorStep> MonitorSteps { get; protected set; }

        protected Monitor()
        {

        }
    }
}