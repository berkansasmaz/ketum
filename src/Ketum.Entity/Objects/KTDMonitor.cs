using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ketum.Entity
{
    [Table("Monitor")]
    public class KTDMonitor
    {
        [Key] public Guid MonitorId { get; set; }

        public Guid UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Name { get; set; }
        public KTDMonitorStatusTypes MonitorStatus { get; set; }
        public KTDTestStatusTypes TestStatus { get; set; }
        public DateTime LastCheckDate { get; set; }
        public decimal UpTime { get; set; }
        public int LoadTime { get; set; }
        public int MonitorTime { get; set; }
    }

    public enum KTDMonitorStatusTypes : short
    {
        Unknown = 0,
        Up = 1,
        Down = 2,
        Warning = 3
    }

    public enum KTDTestStatusTypes : short
    {
        Unknown = 0,
        AllPassed = 1,
        Fail = 2,
        Warning = 3
    }
}