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

namespace LMP.Module
{
    /// <summary>
    /// lmp module.
    /// </summary>
    public class LMPModule : AbpModule
    {
        /// <summary>
        /// Current version of the zero module.
        /// </summary>
        public const string CurrentVersion = "0.5.13.0";

        public override void PreInitialize()
        {
            //IocManager.Register<IAssemblyFinder, LMPAssemblyFinder>();
            IocManager.Register<IUserManagementConfig, UserManagementConfig>();
            IocManager.Register<IRoleManagementConfig, RoleManagementConfig>();
            IocManager.Register<ILMPModuleConfig, LMPModuleConfig>();


            //IocManager.Register<IAsyncTokenProvider, DefaultAsyncTokenProvider>();
            //IocManager.Register<ICacheContextAccessor, DefaultCacheContextAccessor>();
            //IocManager.Register<ICacheHolder, DefaultCacheHolder>();
            ////IocManager.Register<ICacheManager, DefaultCacheManager>();
            //IocManager.Register<IParallelCacheContext, DefaultParallelCacheContext>();
            ////IocManager.Register<IVolatileProvider, DefaultVirtualPathProvider>();
            //IocManager.Register<IVolatileToken, LMP.Caching.Signals.Token>();
            //IocManager.Register<IHttpContextAccessor, HttpContextAccessor>();

            //IocManager.Register<IAppDataFolder, AppDataFolder>();
            //IocManager.Register<IAppDataFolderRoot, AppDataFolderRoot>();
            //IocManager.Register<IAssemblyProbingFolder, DefaultAssemblyProbingFolder>();
            //IocManager.Register<IExtensionDependenciesManager, DefaultExtensionDependenciesManager>();
            //IocManager.Register<ICustomVirtualPathProvider, DynamicModuleVirtualPathProvider>();
            //IocManager.Register<IVirtualPathMonitor, DefaultVirtualPathMonitor>();
            //IocManager.Register<IVirtualPathProvider, DefaultVirtualPathProvider>();
            //IocManager.Register<IWebSiteFolder, WebSiteFolder>();


            //IocManager.Register<IProjectFileParser, DefaultProjectFileParser>();
            //IocManager.Register<IExtensionHarvester, ExtensionHarvester>();
            //IocManager.Register<IDependenciesFolder, DefaultDependenciesFolder>();
            //IocManager.Register<ICriticalErrorProvider, DefaultCriticalErrorProvider>();

            //IocManager.IocContainer.Register(
            //    Component.For<IExtensionFolders, ModuleFolders>().ImplementedBy<ModuleFolders>().LifestyleSingleton().DependsOn(Dependency.OnValue("paths", new[] { "~/Modules" })),
            //    Component.For<ICacheManager, DefaultCacheManager>().ImplementedBy<DefaultCacheManager>().LifestyleSingleton().DependsOn(Dependency.OnValue("component", typeof(string)))
            //);

            ////IocManager.Register<IExtensionFolders, ModuleFolders>();

            ////IocManager.Register<IExtensionLoader, CoreExtensionLoader>();
            ////IocManager.Register<IExtensionLoader, ReferencedExtensionLoader>();
            //IocManager.Register<IExtensionLoader, PrecompiledExtensionLoader>();
            //IocManager.Register<IExtensionLoader, DynamicExtensionLoader>();

            //IocManager.Register<IAssemblyLoader, DefaultAssemblyLoader>();
            //IocManager.Register<IAssemblyNameResolver, AppDomainAssemblyNameResolver>();
            //IocManager.Register<IBuildManager, DefaultBuildManager>();
            //IocManager.Register<IHostEnvironment, DefaultHostEnvironment>();
            //IocManager.Register<IExtensionManager, ExtensionManager>();

            //IocManager.Register<IExtensionLoaderCoordinator, ExtensionLoaderCoordinator>();
            //IocManager.Register<IExtensionMonitoringCoordinator, ExtensionMonitoringCoordinator>();

        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());


            //var extensionLoaderCoordinator = IocManager.IocContainer.Resolve<IExtensionLoaderCoordinator>();
            //extensionLoaderCoordinator.SetupExtensions();

            //var cacheManager = IocManager.IocContainer.Resolve<ICacheManager>();
            //var extensionMonitoringCoordinator = IocManager.IocContainer.Resolve<IExtensionMonitoringCoordinator>();

            //cacheManager.Get("LMPHost_Extensions",
            //            ctx =>
            //            {
            //                extensionMonitoringCoordinator.MonitorExtensions(ctx.Monitor);
            //                return "";
            //            });
        }
    }
}
