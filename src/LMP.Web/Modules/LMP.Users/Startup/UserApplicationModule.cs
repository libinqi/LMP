using System.Reflection;
using Abp.Modules;
using Abp.WebApi.Controllers.Dynamic.Builders;
using Abp.Application.Services;
using Abp.WebApi;
using LMP.Core;
using Abp.AutoMapper;
using Abp.EntityFramework;

namespace LMP.Users
{
    [DependsOn(typeof(AbpWebApiModule), typeof(LMPCoreModule), typeof(AbpAutoMapperModule), typeof(AbpEntityFrameworkModule))] //We declare depended modules explicitly
    public class UserApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            //This code is used to register classes to dependency injection system for this assembly using conventions.
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            DynamicApiControllerBuilder
            .ForAll<IApplicationService>(Assembly.GetExecutingAssembly(), "users")
            .Build();
        }
    }
}
