using System;
using Volo.Abp.Application.Dtos;

namespace Ketum.Monitors
{
    public class MonitorDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public MonitorStatusTypes MonitorStatus { get; set; }
    }
}