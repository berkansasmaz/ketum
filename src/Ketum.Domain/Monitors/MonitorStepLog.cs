using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Ketum.Monitors
{
    public class MonitorStepLog : Entity<Guid>, IMultiTenant
    {      
        public Guid? TenantId { get; }

        public Guid MonitorStepId { get; protected set; }

        public DateTime StartDate { get; protected set; }
        
        public DateTime EndDate { get; protected set; }
        
        public MonitorStepStatusTypes Status { get; protected set; }
        
        public string Log { get; set; }
        
        public int Interval { get; protected set; }

        protected MonitorStepLog()
        {

        }
    }
}