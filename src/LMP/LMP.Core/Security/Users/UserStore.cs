using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using LMP.Authorization.Users;
using LMP.Core.MultiTenancy;
using LMP.Core.Security.Roles;

namespace LMP.Core.Security.Users
{
    public class UserStore : LMPUserStore<Tenant, Role, User>
    {
        public UserStore(
            IRepository<User, long> userRepository,
            IRepository<UserLogin, long> userLoginRepository,
            IRepository<UserRole, long> userRoleRepository,
            IRepository<Role> roleRepository,
            IRepository<UserPermissionSetting, long> userPermissionSettingRepository,
            IAbpSession session,
            IUnitOfWorkManager unitOfWorkManager)
            : base(
                userRepository,
                userLoginRepository,
                userRoleRepository,
                roleRepository,
                userPermissionSettingRepository,
                session,
                unitOfWorkManager)
        {
        }
    }
}