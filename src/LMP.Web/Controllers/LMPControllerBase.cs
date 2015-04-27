using Abp.Web.Mvc.Controllers;

namespace LMP.Web.Controllers
{
    public abstract class LMPControllerBase : AbpController
    {
        protected LMPControllerBase()
        {
            LocalizationSourceName = "AbpWeb";
        }
    }
}