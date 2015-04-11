using System.Collections.Generic;

namespace LMP.Module.Configuration
{
    public interface IRoleManagementConfig
    {
        List<StaticRoleDefinition> StaticRoles { get; }
    }
}