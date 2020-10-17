using System;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Ketum.Monitors
{
    public class MonitorStepLog : Entity<Guid>, IMultiTenant
    {      
        public Guid? TenantId { get; }

        public Guid MonitorStepId { get; protected set; }

        public DateTime StartDate { get; protected set; }
        
        public DateTime? EndDate { get; set; }
        
        public MonitorStepStatusTypes Status { get; set; }
        
        public string Log { get; protected set; }
        
        public int Interval { get; protected set; }

        protected MonitorStepLog()
        {

        }

        public MonitorStepLog(
            Guid id,
            Guid monitorStepId,
            DateTime startDate,
            MonitorStepStatusTypes status,
            int interval,
            DateTime? endDate = null,
            Guid? tenantId = null)
            : base(id)
        {
            MonitorStepId = monitorStepId;
            StartDate = startDate;
            Status = status;
            Interval = interval;
            TenantId = tenantId;
        }

        public MonitorStepLog SetLog([NotNull] string log)
        {
            Check.NotNullOrWhiteSpace(log, nameof(log));

            Log = log;

            return this;
        }
    }
}