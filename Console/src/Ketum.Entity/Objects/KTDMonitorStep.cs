using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ketum.Entity

{
    [Table("MonitorStep")]
    public class KTDMonitorStep
    {
        [Key]
        public Guid MonitorStepId { get; set; }
        public Guid MonitorId { get; set; }
        public KTDMonitorStepTypes Type { get; set; }
        public string Settings { get; set; }
    }

    public enum KTDMonitorStepTypes : short
    {
        Request = 1,
        StatusCode = 2,
        HeaderExists = 3,
        BodyContains = 4,
    }
}