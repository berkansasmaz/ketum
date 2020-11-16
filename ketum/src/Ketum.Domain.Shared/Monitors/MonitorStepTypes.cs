namespace Ketum.Monitors
{
    public enum MonitorStepTypes : byte
    {
        Unknown = 0,
        Request = 1,
        StatusCode = 2,
        HeaderExists = 3,
        BodyContains = 4
    }
}