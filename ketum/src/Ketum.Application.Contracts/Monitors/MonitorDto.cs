using System;
using Volo.Abp.Application.Dtos;

namespace Ketum.Monitors
{
    public class MonitorDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }

        public MonitorStatusTypes MonitorStatus { get; set; }
        
        public MonitorStepDto MonitorStep { get; set; }
    }
}