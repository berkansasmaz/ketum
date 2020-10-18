using Ketum.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Ketum.Permissions
{
    public class KetumPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var monitor = context.AddGroup(KetumPermissions.Monitoring.MonitorGroup, L("Permission:Monitoring"));

            var communityArticles =
                monitor.AddPermission(KetumPermissions.Monitoring.Default, L("Permission:Monitoring"));
            communityArticles.AddChild(KetumPermissions.Monitoring.Create, L("Permission:Create"));
            communityArticles.AddChild(KetumPermissions.Monitoring.Update, L("Permission:Edit"));
            communityArticles.AddChild(KetumPermissions.Monitoring.Delete, L("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<KetumResource>(name);
        }
    }
}