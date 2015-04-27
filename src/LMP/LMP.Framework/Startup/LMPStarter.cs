using System.Reflection;
using Abp.Modules;
using LMP.Module.Configuration;
using LMP.Module.Environment.Extensions.Compilers;
using LMP.Module.Environment.Extensions.Folders;
using LMP.FileSystems.Dependencies;
using LMP.Module.Environment.Extensions;
using LMP.Module.Environment.Extensions.Loaders;
using LMP.Module.Environment;
using LMP.FileSystems.AppData;
using LMP.FileSystems.VirtualPath;
using LMP.FileSystems.WebSite;
using LMP.Caching;
using Castle.MicroKernel.Registration;
using LMP.Web.Mvc;
using Abp.Reflection;
using LMP.Module.Reflection;
using Abp.Dependency;


namespace LMP.Startup
{
    public class LMPStarter
    {
        public void Initialize()
        {
            IocManager.Instance.Register<IAsyncTokenProvider, DefaultAsyncTokenProvider>();
            IocManager.Instance.Register<ICacheContextAccessor, DefaultCacheContextAccessor>();
            IocManager.Instance.Register<ICacheHolder, DefaultCacheHolder>();
            //IocManager.Instance.Register<ICacheManager, DefaultCacheManager>();
            IocManager.Instance.Register<IParallelCacheContext, DefaultParallelCacheContext>();
            //IocManager.Instance.Register<IVolatileProvider, DefaultVirtualPathProvider>();
            IocManager.Instance.Register<IVolatileToken, LMP.Caching.Signals.Token>();
            IocManager.Instance.Register<IHttpContextAccessor, HttpContextAccessor>();

            IocManager.Instance.Register<IAppDataFolder, AppDataFolder>();
            IocManager.Instance.Register<IAppDataFolderRoot, AppDataFolderRoot>();
            IocManager.Instance.Register<IAssemblyProbingFolder, DefaultAssemblyProbingFolder>();
            IocManager.Instance.Register<IExtensionDependenciesManager, DefaultExtensionDependenciesManager>();
            IocManager.Instance.Register<IVirtualPathMonitor, DefaultVirtualPathMonitor>();
            IocManager.Instance.Register<IVirtualPathProvider, DefaultVirtualPathProvider>();
            IocManager.Instance.Register<IWebSiteFolder, WebSiteFolder>();


            IocManager.Instance.Register<IProjectFileParser, DefaultProjectFileParser>();
            IocManager.Instance.Register<IExtensionHarvester, ExtensionHarvester>();
            IocManager.Instance.Register<IDependenciesFolder, DefaultDependenciesFolder>();
            IocManager.Instance.Register<ICriticalErrorProvider, DefaultCriticalErrorProvider>();

            IocManager.Instance.IocContainer.Register(
                Component.For<IExtensionFolders, ModuleFolders>().ImplementedBy<ModuleFolders>().LifestyleSingleton().DependsOn(Dependency.OnValue("paths", new[] { "~/Modules" })),
                Component.For<ICacheManager, DefaultCacheManager>().ImplementedBy<DefaultCacheManager>().LifestyleSingleton().DependsOn(Dependency.OnValue("component", typeof(string)))
            );

            //IocManager.Instance.Register<IExtensionFolders, ModuleFolders>();

            //IocManager.Instance.Register<IExtensionLoader, CoreExtensionLoader>();
            //IocManager.Instance.Register<IExtensionLoader, ReferencedExtensionLoader>();
            IocManager.Instance.Register<IExtensionLoader, PrecompiledExtensionLoader>();
            //IocManager.Instance.Register<IExtensionLoader, DynamicExtensionLoader>();

            IocManager.Instance.Register<IAssemblyLoader, DefaultAssemblyLoader>();
            IocManager.Instance.Register<IAssemblyNameResolver, AppDomainAssemblyNameResolver>();
            IocManager.Instance.Register<IBuildManager, DefaultBuildManager>();
            IocManager.Instance.Register<IHostEnvironment, DefaultHostEnvironment>();
            IocManager.Instance.Register<IExtensionManager, ExtensionManager>();

            IocManager.Instance.Register<IExtensionLoaderCoordinator, ExtensionLoaderCoordinator>();
            IocManager.Instance.Register<IExtensionMonitoringCoordinator, ExtensionMonitoringCoordinator>();

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
