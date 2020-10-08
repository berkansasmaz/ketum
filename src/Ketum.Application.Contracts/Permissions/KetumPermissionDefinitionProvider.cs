using Ketum.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Ketum.Permissions
{
    public class KetumPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(KetumPermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(KetumPermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<KetumResource>(name);
        }
    }
}
