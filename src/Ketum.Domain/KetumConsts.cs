using System;

namespace Ketum
{
    public static class KetumConsts
    {
        public const string DbTablePrefix = "Ktm";

        public const string DbSchema = null;

        public static readonly TimeSpan MonitorWorkerPeriod = TimeSpan.FromMinutes(1);
    }
}
