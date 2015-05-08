using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;
using Abp.WebApi.Controllers.Dynamic.Builders;
using Abp.Application.Services;
using Abp.WebApi;
using LMP.Core;
using Abp.EntityFramework;
using LMP.TaskSystem.Navigation;
using Abp.Localization.Sources;
using Abp.Localization.Sources.Xml;
using System.Web;
using LMP.Module;

namespace LMP.TaskSystem
{
    //[DependsOn(typeof(AbpWebApiModule), typeof(LMPCoreModule), typeof(AbpAutoMapperModule), typeof(AbpEntityFrameworkModule))] //We declare depended modules explicitly
    [DependsOn(typeof(LMPModule), typeof(LMPCoreModule), typeof(AbpAutoMapperModule), typeof(AbpEntityFrameworkModule))]
    public class TaskSystemApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            //This code is used to register classes to dependency injection system for this assembly using conventions.
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Configuration.Navigation.Providers.Add<TaskSystemMenu>();

            //Configuration.Localization.Sources.Add(
            //new DictionaryBasedLocalizationSource(
            //    "TaskSystem",
            //    new XmlFileLocalizationDictionaryProvider(
            //        HttpContext.Current.Server.MapPath("~/Modules/LMP.TaskSystem/Localization/TaskSystem")
            //        )
            //    )
            //);

            DynamicApiControllerBuilder
            .ForAll<IApplicationService>(Assembly.GetExecutingAssembly(), "tasksystem")
            .Build();
        }
    }
}
