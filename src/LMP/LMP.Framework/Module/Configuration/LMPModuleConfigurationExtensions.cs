using Abp.Configuration.Startup;

namespace LMP.Module.Configuration
{
    /// <summary>
    /// Extension methods for module zero configurations.
    /// </summary>
    public static class LMPModuleConfigurationExtensions
    {
        /// <summary>
        /// Used to configure module zero.
        /// </summary>
        /// <param name="moduleConfigurations"></param>
        /// <returns></returns>
        public static ILMPModuleConfig Zero(this IModuleConfigurations moduleConfigurations)
        {
            return moduleConfigurations.AbpConfiguration
                .GetOrCreate("LMPModuleConfig",
                    () => moduleConfigurations.AbpConfiguration.IocManager.Resolve<ILMPModuleConfig>()
                );
        }
    }
}