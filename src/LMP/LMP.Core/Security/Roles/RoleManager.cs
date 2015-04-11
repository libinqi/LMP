using Abp.Authorization;
using Abp.Domain.Uow;
using LMP.Authorization.Roles;
using LMP.Core.MultiTenancy;
using LMP.Core.Security.Users;
using LMP.Module.Configuration;

namespace LMP.Core.Security.Roles
{
    public class RoleManager : LMPRoleManager<Tenant, Role, User>
    {
        public RoleManager(RoleStore store, IPermissionManager permissionManager, IRoleManagementConfig roleManagementConfig, IUnitOfWorkManager unitOfWorkManager)
            : base(store, permissionManager, roleManagementConfig, unitOfWorkManager)
        {
        }
    }
}