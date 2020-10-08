using Ketum.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Ketum.Web.Pages
{
    public abstract class KetumPageModel : AbpPageModel
    {
        protected KetumPageModel()
        {
            LocalizationResourceType = typeof(KetumResource);
        }
    }
}