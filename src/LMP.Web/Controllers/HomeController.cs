using Abp.Web.Mvc.Authorization;
using LMP.Module.Environment.Extensions.Folders;
using System.Web.Mvc;

namespace LMP.Web.Controllers
{
    [AbpAuthorize]
    public class HomeController : LMPControllerBase
    {
        public ActionResult Index()
        {
            return View("~/App/Main/views/layout/layout.cshtml"); //Layout of the angular application.
        }
    }
}