using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Ketum.Monitors
{
    public class MonitorStep : Entity<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; }

        public Guid MonitorId { get; protected set; }

        public string Url { get; protected set; }

        public int Interval { get; protected set; }

        public MonitorStepTypes Type { get; protected set; }

        public MonitorStepStatusTypes Status { get; set; }

        public List<MonitorStepLog> MonitorStepLogs { get; protected set; }

        protected MonitorStep()
        {
        }

        public MonitorStep(
            Guid id,
            Guid monitorId,
            string url,
            int interval,
            MonitorStepTypes type,
            MonitorStepStatusTypes status)
            : base(id)
        {
            MonitorId = monitorId;
            Url = url;
            Interval = interval;
            Type = type;
            Status = status;

            MonitorStepLogs = new List<MonitorStepLog>();
        }

        public MonitorStep SetUrl([NotNull] string url)
        {
            Check.NotNullOrWhiteSpace(url, nameof(url));

            Url = url;

            return this;
        }
    }
}