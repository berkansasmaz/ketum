using Volo.Abp.Settings;

namespace Ketum.Settings
{
    public class KetumSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(KetumSettings.MySetting1));
        }
    }
}