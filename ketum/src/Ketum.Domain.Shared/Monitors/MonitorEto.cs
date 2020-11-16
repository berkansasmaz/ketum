using System;

namespace Ketum.Monitors
{
    public class MonitorEto
    {
        public Guid? CreatorId { get; set; }

        public DateTime? LastModificationTime { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public MonitorStatusTypes MonitorStatus { get; set; }

        public MonitorEto(
            Guid? creatorId, 
            DateTime? lastModificationTime, 
            string name, 
            string url, 
            MonitorStatusTypes monitorStatus)
        {
            CreatorId = creatorId;
            LastModificationTime = lastModificationTime;
            Name = name;
            Url = url;
            MonitorStatus = monitorStatus;
        }
    }
}