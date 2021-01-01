using Ketum.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Ketum.Permissions
{
    public class KetumPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var monitor = context.AddGroup(KetumPermissions.Monitors.Default, L("Permission:Monitors"));

            var communityArticles = monitor.AddPermission(KetumPermissions.Monitors.Default, L("Permission:Monitors"));
            communityArticles.AddChild(KetumPermissions.Monitors.Create, L("Permission:Create"));
            communityArticles.AddChild(KetumPermissions.Monitors.Update, L("Permission:Edit"));
            communityArticles.AddChild(KetumPermissions.Monitors.Delete, L("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<KetumResource>(name);
        }
    }
}
