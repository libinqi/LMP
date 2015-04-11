using System.Reflection;
using Abp.EntityFramework;
using Abp.Modules;
using LMP.Module;
using Abp.Runtime.Session;
using LMP.Runtime.Session;

namespace LMP.Core
{
    [DependsOn(typeof(LMPModule))]
    public class LMPCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<IAbpSession, LMPSession>();
        }
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
