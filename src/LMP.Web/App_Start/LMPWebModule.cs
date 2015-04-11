using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Abp.Localization;
using Abp.Localization.Sources;
using Abp.Localization.Sources.Xml;
using Abp.Modules;
using LMP.TaskSystem;
using LMP.QuestionSystem;
using LMP.Core;
using LMP.Module;
using LMP.Users;

namespace LMP.Web
{
    [DependsOn(typeof(TaskSystemApplicationModule), typeof(QuestionSystemApplicationModule), typeof(UserApplicationModule))]
    public class LMPWebModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Add/remove languages for your application
            Configuration.Localization.Languages.Add(new LanguageInfo("zh-cn", "中文", "famfamfam-flag-cn", true));
            Configuration.Localization.Languages.Add(new LanguageInfo("en", "English", "famfamfam-flag-england"));

            //Add a localization source
            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    "TaskSystem",
                    new XmlFileLocalizationDictionaryProvider(
                        HttpContext.Current.Server.MapPath("~/Localization/TaskSystem")
                        )
                    )
                );

            Configuration.Localization.Sources.Add(
            new DictionaryBasedLocalizationSource(
                "QuestionSystem",
                new XmlFileLocalizationDictionaryProvider(
                    HttpContext.Current.Server.MapPath("~/Localization/QuestionSystem")
                    )
                )
            );

            Configuration.Localization.Sources.Add(
            new DictionaryBasedLocalizationSource(
                "Users",
                new XmlFileLocalizationDictionaryProvider(
                    HttpContext.Current.Server.MapPath("~/Localization/Users")
                    )
                )
            );

            //Configure navigation/menu
            Configuration.Navigation.Providers.Add<LMPNavigationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}