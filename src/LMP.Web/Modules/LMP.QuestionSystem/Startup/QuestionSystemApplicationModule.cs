using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;
using Abp.WebApi.Controllers.Dynamic.Builders;
using Abp.Application.Services;
using Abp.WebApi;
using LMP.Core;
using LMP.QuestionSystem.Configuration;
using LMP.QuestionSystem.Authorization;
using Abp.EntityFramework;
using LMP.QuestionSystem.Navigation;
using Abp.Localization.Sources;
using Abp.Localization.Sources.Xml;
using System.Web;

namespace LMP.QuestionSystem
{
    //[DependsOn(typeof(AbpWebApiModule), typeof(LMPCoreModule), typeof(AbpAutoMapperModule), typeof(AbpEntityFrameworkModule))] //We declare depended modules explicitly
    public class QuestionSystemApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            //This code is used to register classes to dependency injection system for this assembly using conventions.
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Configuration.Authorization.Providers.Add<QuestionSystemAuthorizationProvider>();
            Configuration.Settings.Providers.Add<QuestionsSettingProvider>();

            Configuration.Navigation.Providers.Add<QuestionSystemMenu>();

            Configuration.Localization.Sources.Add(
            new DictionaryBasedLocalizationSource(
                "QuestionSystem",
                new XmlFileLocalizationDictionaryProvider(
                    HttpContext.Current.Server.MapPath("~/Modules/LMP.QuestionSystem/Localization/QuestionSystem")
                    )
                )
            );

            DynamicApiControllerBuilder
            .ForAll<IApplicationService>(Assembly.GetExecutingAssembly(), "questionsystem")
            .Build();
        }
    }
}
