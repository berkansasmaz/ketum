using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Ketum.Monitors
{
    public class Monitor : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public const string DefaultSorting = "CreationTime desc";

        public Guid? TenantId { get; }

        public string Name { get; protected set; }

        public MonitorStatusTypes MonitorStatus { get; set; }

        public MonitorStep MonitorStep { get; protected set; }

        protected Monitor()
        {
        }

        public Monitor(
            Guid id,
            [NotNull] string name,
            MonitorStatusTypes monitorStatus,
            MonitorStep monitorStep,
            Guid? tenantId = null)
            : base(id)
        {
            Name = Check.NotNullOrWhiteSpace(name, nameof(name));
            MonitorStatus = monitorStatus;
            MonitorStep = monitorStep;
            TenantId = tenantId;
        }

        public Monitor SetName([NotNull] string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            Name = name;

            return this;
        }

        public Monitor AddMonitorStepLog(MonitorStepLog monitorStepLog)
        {
            MonitorStep.MonitorStepLogs.AddIfNotContains(monitorStepLog);

            return this;
        }

        public Monitor SetMonitorStatusType(Monitor monitor)
        {
            MonitorStatus = monitor.MonitorStatus;
            
            if (monitor.MonitorStatus.IsIn(MonitorStatusTypes.Down, MonitorStatusTypes.Warning))
            {
                AddDistributedEvent(                
                    new MonitorEto(
                        monitor.CreatorId,
                        monitor.LastModificationTime,
                        monitor.Name,
                        monitor.MonitorStep.Url,
                        monitor.MonitorStatus)
                );
            }

            return this;
        }
    }
}