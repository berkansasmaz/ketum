namespace Ketum.Permissions
{
    public static class KetumPermissions
    {
        public const string GroupName = "Ketum";

        public static class Monitoring
        {
            public const string MonitorGroup = GroupName + ".Monitoring";

            public const string Default = MonitorGroup;
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }
    }
}