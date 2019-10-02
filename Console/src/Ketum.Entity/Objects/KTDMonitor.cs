using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ketum.Entity
{
    [Table("Monitor")]
    public class KTDMonitor
    {
        [Key]
        public Guid MonitorId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Name { get; set; }
        public KTDMonitorStatusTypes MonitorStatus { get; set; }
        public KTDTestStatusTypes TestStatus { get; set; }
        public DateTime LastCheckDate { get; set; }
        public decimal UpTime { get; set; }
        public int LoadTime { get; set; }
        public short MonitorTime { get; set; }

    }

    public enum KTDMonitorStatusTypes : short
    {
        Down = 0,
        Up = 1,
        Warning = 3
    }
    public enum KTDTestStatusTypes : short
    {
        Fail = 0,
        AllPassed = 1,
        Warning = 2

    }
}