using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ketum.Entity
{
    [Table("MonitorStepLog")]
    public class KTDMonitorStepLog
    {
        [Key] public Guid MonitorStepLogId { get; set; }

        public Guid MonitorStepId { get; set; }
        public Guid MonitorId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public KTDMonitorStepStatusTypes Status { get; set; }
        public string Log { get; set; }
        public int Interval { get; set; }
    }
}