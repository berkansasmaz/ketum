using System;
using System.Collections.Generic;
using System.Text;
using Ketum.Localization;
using Volo.Abp.Application.Services;

namespace Ketum
{
    /* Inherit your application services from this class.
     */
    public abstract class KetumAppService : ApplicationService
    {
        protected KetumAppService()
        {
            LocalizationResource = typeof(KetumResource);
        }
    }
}
