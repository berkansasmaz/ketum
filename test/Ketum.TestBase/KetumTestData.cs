using System;
using Volo.Abp.DependencyInjection;

namespace Ketum
{
    public class KetumTestData : ISingletonDependency
    {
        public Guid UserId1 { get; } = Guid.NewGuid();

        public Guid MonitorId1 { get; } = Guid.NewGuid();

        public Guid MonitorId2 { get; } = Guid.NewGuid();

        public Guid MonitorStepId1 { get; } = Guid.NewGuid();

        public Guid MonitorStepId2 { get; } = Guid.NewGuid();

        public string MonitorName1 { get; } = "monitor-name-1";

        public string MonitorName2 { get; } = "monitor-name-2";

        public string MonitorUrl1 { get; } = "https://ketum.io";

        public string MonitorUrl2 { get; } = "https://berkansasmaz.com";

        public int MonitorInterval1 = 1;

        public int MonitorInterval2 = 2;



    }
}