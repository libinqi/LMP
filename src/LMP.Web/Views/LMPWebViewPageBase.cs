using Abp.Web.Mvc.Views;

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