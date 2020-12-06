using Volo.Abp.Bundling;

namespace Ketum.Blazor
{
    public class KetumBundleContributer : IBundleContributor
    {
        public void AddScripts(BundleContext context)
        {
        }

        public void AddStyles(BundleContext context)
        {
            context.Add("main.css");
        }
    }
}
