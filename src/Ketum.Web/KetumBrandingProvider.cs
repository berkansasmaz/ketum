using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Components;
using Volo.Abp.DependencyInjection;

namespace Ketum.Web
{
    [Dependency(ReplaceServices = true)]
    public class KetumBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "Ketum";
    }
}
