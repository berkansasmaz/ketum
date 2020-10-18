using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;

namespace Ketum.Monitors
{
    public class MonitorDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public MonitorStatusTypes MonitorStatus { get; set; }

        public decimal UpTime { get; set; }

        public int LoadTime { get; set; }

        public int MonitorTime { get; set; }
    }
}