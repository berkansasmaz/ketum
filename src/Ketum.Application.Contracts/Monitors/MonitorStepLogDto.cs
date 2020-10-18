using System;

namespace Ketum.Monitors
{
    public class MonitorStepLogDto
    {
        public DateTime StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public MonitorStepStatusTypes Status { get; set; }
        
        public string Log { get; set; }
        
        public int Interval { get; set; }
    }
}