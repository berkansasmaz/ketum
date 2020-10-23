using System;

namespace Ketum
{
    public static class KetumConsts
    {
        public const string DbTablePrefix = "Ktm";

        public const string DbSchema = null;

        public const int MaxMonitorsProcessedCount = 20;

        public static readonly TimeSpan MonitorWorkerPeriod = TimeSpan.FromMinutes(1);
    }
}