using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Ketum
{
    [Dependency(ReplaceServices = true)]
    public class KetumBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "Ketum";
    }
}
