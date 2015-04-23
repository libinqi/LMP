using System;
using Abp.Dependency;
using Abp.Web;
using Castle.Facilities.Logging;
using System.Data.Entity;
using LMP.TaskSystem.EntityFramework;
using LMP.Module.Environment.Extensions.Folders;
using Castle.MicroKernel.Registration;
using LMP.Module.Environment.Extensions;
using LMP.Caching;

namespace LMP.Web
{
    public class MvcApplication : AbpWebApplication
    {
        protected override void Application_Start(object sender, EventArgs e)
        {
            IocManager.Instance.IocContainer.AddFacility<LoggingFacility>(f => f.UseLog4Net().WithConfig("log4net.config"));
            base.Application_Start(sender, e);

            //IocManager.Instance.IocContainer.Register(
            //        Component.For<IExtensionHarvester, ExtensionHarvester>().ImplementedBy<ExtensionHarvester>().LifestyleSingleton(),
            //        Component.For<IExtensionFolders, ModuleFolders>().ImplementedBy<ModuleFolders>().LifestyleSingleton().DependsOn(Dependency.OnValue("paths", "~/LMP.Modules"))
            //    );

            //var moduleFolders = IocManager.Instance.IocContainer.Resolve<IExtensionFolders>(new[] { "~/LMP.Modules" });
            //moduleFolders.AvailableExtensions();
            var extensionLoaderCoordinator = IocManager.Instance.IocContainer.Resolve<IExtensionLoaderCoordinator>();
            extensionLoaderCoordinator.SetupExtensions();

            var cacheManager = IocManager.Instance.IocContainer.Resolve<ICacheManager>();
            var extensionMonitoringCoordinator = IocManager.Instance.IocContainer.Resolve<IExtensionMonitoringCoordinator>();

            cacheManager.Get("LMPHost_Extensions",
                        ctx =>
                        {
                            extensionMonitoringCoordinator.MonitorExtensions(ctx.Monitor);
                            return "";
                        });
        }
    }
}
