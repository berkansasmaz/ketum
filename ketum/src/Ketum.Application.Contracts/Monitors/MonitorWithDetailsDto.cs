using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Ketum.Monitors
{
    public class MonitorWithDetailsDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }

        public MonitorStatusTypes MonitorStatus { get; set; }

        public double UpTimePercent { get; set; }

        public List<double> UpTimes { get; set; }

        public double DownTime { get; set; }

        public double DownTimePercent { get; set; }

        public double LoadTime { get; set; }

        public List<double> LoadTimes { get; set; }
        
        public List<string> DateTimes { get; set; }

        public int MonitoredTime { get; set; }

        public MonitorStepDto MonitorStep { get; set; }

        public MonitorWithDetailsDto()
        {
            UpTimes = new List<double>();
            LoadTimes = new List<double>();
            DateTimes = new List<string>();
        }
    }
}