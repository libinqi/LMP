using Abp;
using Abp.Reflection;
using Abp.Localization;
using Castle.MicroKernel.Registration;
using LMP.Module.Reflection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using LMP.Startup;
using Abp.Web;

namespace LMP.Web.Mvc
{
    public class LMPWebApplication : HttpApplication
    {
        protected LMPWebApplication()
        {
        }

        protected virtual void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected virtual void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpCookie cookie = base.Request.Cookies["Abp.Localization.CultureName"];
            if ((cookie != null) && CultureInfo.GetCultureInfo(cookie.Value) != null)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(cookie.Value);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(cookie.Value);
            }
        }

        protected virtual void Application_End(object sender, EventArgs e)
        {
            AbpBootstrapper.Dispose();
        }

        protected virtual void Application_EndRequest(object sender, EventArgs e)
        {
        }

        protected virtual void Application_Error(object sender, EventArgs e)
        {
        }

        protected virtual void Application_Start(object sender, EventArgs e)
        {
            new LMPStarter().Initialize();

            AbpBootstrapper = new AbpBootstrapper();
            AbpBootstrapper.IocManager.IocContainer.Register(
                Component.For<IAssemblyFinder>().ImplementedBy<LMPAssemblyFinder>(),
                Component.For<ITypeFinder>().ImplementedBy<LMP.Module.Reflection.TypeFinder>()
            );
            AbpBootstrapper.Initialize();

        }

        protected virtual void Session_End(object sender, EventArgs e)
        {
        }

        protected virtual void Session_Start(object sender, EventArgs e)
        {
        }

        private AbpBootstrapper AbpBootstrapper { get; set; }
    }
}
