using Abp.Web.Mvc.Views;
using LMP.TaskSystem;

namespace LMP.Web.Views
{
    public abstract class LMPWebViewPageBase : LMPWebViewPageBase<dynamic>
    {

    }

    public abstract class LMPWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected LMPWebViewPageBase()
        {
            LocalizationSourceName = "AbpWeb";
        }
    }
}