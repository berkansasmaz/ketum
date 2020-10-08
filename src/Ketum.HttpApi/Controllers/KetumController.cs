using Ketum.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Ketum.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class KetumController : AbpController
    {
        protected KetumController()
        {
            LocalizationResource = typeof(KetumResource);
        }
    }
}