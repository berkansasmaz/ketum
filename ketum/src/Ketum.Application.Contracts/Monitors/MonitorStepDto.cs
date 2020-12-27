using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Ketum.Monitors
{
    public class MonitorStepDto : EntityDto<Guid>
    {
        public string Url { get; set; }

        public int Interval { get; set; }

        public MonitorStepTypes Type { get; set; }

        public MonitorStepStatusTypes Status { get; set; }

        public List<MonitorStepLogDto> MonitorStepLogs { get; set; }
    }
}