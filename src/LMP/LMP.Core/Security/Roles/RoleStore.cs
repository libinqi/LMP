using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using LMP.Authorization.Roles;
using LMP.Core.MultiTenancy;
using LMP.Core.Security.Users;

namespace LMP.Core.Security.Roles
{
    public class RoleStore : LMPRoleStore<Tenant, Role, User>
    {
        public RoleStore(
            IRepository<Role> roleRepository,
            IRepository<RolePermissionSetting, long> rolePermissionSettingRepository,
            IAbpSession session)
            : base(
                roleRepository,
                rolePermissionSettingRepository,
                session)
        {
        }
    }
}