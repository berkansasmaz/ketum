using Ketum.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Ketum.Blazor
{
    public class KetumComponentBase : AbpComponentBase
    {
        public KetumComponentBase()
        {
            LocalizationResource = typeof(KetumResource);
        }
    }
}
