using System.Collections.Generic;

namespace Ketum.Monitors
{
    public class MonitorStepDto
    {
        public string Url { get; set; }

        public int Interval { get; set; }

        public MonitorStepTypes Type { get; set; }

        public MonitorStepStatusTypes Status { get; set; }

        public List<MonitorStepLogDto> MonitorStepLogs { get; set; }
    }
}