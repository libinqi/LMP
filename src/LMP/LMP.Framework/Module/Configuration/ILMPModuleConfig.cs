using System.Security.Policy;

namespace LMP.Module.Configuration
{
    /// <summary>
    /// Configuration options for zero module.
    /// </summary>
    public interface ILMPModuleConfig
    {
        /// <summary>
        /// Gets user management config.
        /// </summary>
        IUserManagementConfig UserManagement { get; }
        
        /// <summary>
        /// Gets role management config.
        /// </summary>
        IRoleManagementConfig RoleManagement { get; }
    }
}