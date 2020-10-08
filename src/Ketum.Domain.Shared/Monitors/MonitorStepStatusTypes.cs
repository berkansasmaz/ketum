namespace Ketum.Monitors
{
    public enum MonitorStepStatusTypes : byte
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