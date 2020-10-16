using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Ketum.Monitors
{
    public class MonitorWithDetailsDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }

        public MonitorStatusTypes MonitorStatus { get; set; }

        public TestStatusTypes TestStatus { get; set; }

        public string Url { get; set; }

        public decimal UpTime { get; set; }

        public List<double> UpTimes { get; set; }

        public double DownTime { get; set; }

        public double DownTimePercent { get; set; }

        public int LoadTime { get; set; }

        public List<double> LoadTimes { get; set; }

        public int MonitorTime { get; set; }

        public MonitorStepStatusTypes StepStatus { get; set; }
    }
}