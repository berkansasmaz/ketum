using System;
using System.ComponentModel.DataAnnotations;

namespace Ketum.Entity
{
    public class KTDMonitorAlert
    {
        [Key]
        public Guid MonitorAlertId { get; set; }
        public Guid MonitorId { get; set; }
        public string Title { get; set; }
        public KTDMonitorAlertChannelTypes ChannelType { get; set; }
        public string Settings { get; set; }
    }
    
    public enum KTDMonitorAlertChannelTypes : short
    {
        Unknown = 0,
        Email = 1,
        SMS = 2,
        Webhook = 3,
        Slack = 4,
        Telegram = 5
    }
}