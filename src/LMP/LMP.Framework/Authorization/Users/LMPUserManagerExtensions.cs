using System;
using Abp.MultiTenancy;
using Abp.Threading;
using LMP.MultiTenancy;
using LMP.Authorization.Roles;

namespace LMP.Authorization.Users
{
    /// <summary>
    /// Extension methods for <see cref="LMPUserManager{TTenant,TRole,TUser}"/>.
    /// </summary>
    public static class LMPUserManagerExtensions
    {
        /// <summary>
        /// Check whether a user is granted for a permission.
        /// </summary>
        /// <param name="manager">User manager</param>
        /// <param name="userId">User id</param>
        /// <param name="permissionName">Permission name</param>
        public static bool IsGranted<TTenant, TRole, TUser>(LMPUserManager<TTenant, TRole, TUser> manager, long userId, string permissionName)
            where TTenant : LMPTenant<TTenant, TUser>
            where TRole : LMPRole<TTenant, TUser>, new()
            where TUser : LMPUser<TTenant, TUser>
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }

            return AsyncHelper.RunSync(() => manager.IsGrantedAsync(userId, permissionName));
        }

        public static LMPUserManager<TTenant, TRole, TUser>.AbpLoginResult Login<TTenant, TRole, TUser>(LMPUserManager<TTenant, TRole, TUser> manager, string userNameOrEmailAddress, string plainPassword, string tenancyName = null)
            where TTenant : LMPTenant<TTenant, TUser>
            where TRole : LMPRole<TTenant, TUser>, new()
            where TUser : LMPUser<TTenant, TUser>
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }

            return AsyncHelper.RunSync(() => manager.LoginAsync(userNameOrEmailAddress, plainPassword, tenancyName));
        }
    }
}