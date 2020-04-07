using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ketum.Entity
{
    [Table("MonitorAlertLog")]
    public class KTDMonitorAlertLog
    {
        [Key]
        public Guid MonitorAlertLogId { get; set; }
        public Guid MonitorAlertId { get; set; }
        public Guid MonitorId { get; set; }
        public string Log { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public KTDMonitorAlertLogStatusTypes Status { get; set; }
    }
    
    public enum KTDMonitorAlertLogStatusTypes : short
    {
        Unknown = 0,
        Pending = 1,
        Processing = 2,
        Success = 3,
        Warning = 4,
        Fail = 5,
        Error = 6
    }
}