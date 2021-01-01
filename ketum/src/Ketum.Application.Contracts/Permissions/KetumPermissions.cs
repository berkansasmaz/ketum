namespace Ketum.Permissions
{
    public static class KetumPermissions
    {
        public const string GroupName = "Ketum";

        public static class Monitors
        {
            private const string MonitorGroup = GroupName + ".Monitors";

            public const string Default = MonitorGroup;
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }
    }
}