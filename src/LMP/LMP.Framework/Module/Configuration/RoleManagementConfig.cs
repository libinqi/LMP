using System.Collections.Generic;

namespace LMP.Module.Configuration
{
    internal class RoleManagementConfig : IRoleManagementConfig
    {
        public List<StaticRoleDefinition> StaticRoles { get; private set; }

        public RoleManagementConfig()
        {
            StaticRoles = new List<StaticRoleDefinition>();
        }
    }
}