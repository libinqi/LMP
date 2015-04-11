using System.Reflection;
using Abp.Modules;
using LMP.Module.Configuration;

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
            IocManager.Register<IUserManagementConfig, UserManagementConfig>();
            IocManager.Register<IRoleManagementConfig, RoleManagementConfig>();
            IocManager.Register<ILMPModuleConfig, LMPModuleConfig>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
