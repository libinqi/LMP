using Abp.Web.Mvc.Controllers;
using LMP.TaskSystem;

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