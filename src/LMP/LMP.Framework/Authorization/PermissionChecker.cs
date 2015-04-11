using System.Threading.Tasks;
using Abp.Dependency;
using Abp.MultiTenancy;
using Abp.Runtime.Session;
using Castle.Core.Logging;
using Abp.Authorization;
using LMP.Authorization.Roles;
using LMP.Authorization.Users;
using LMP.MultiTenancy;

namespace LMP.Authorization
{
    /// <summary>
    /// Application should inherit this class to implement <see cref="IPermissionChecker"/>.
    /// </summary>
    /// <typeparam name="TTenant"></typeparam>
    /// <typeparam name="TRole"></typeparam>
    /// <typeparam name="TUser"></typeparam>
    public abstract class PermissionChecker<TTenant, TRole, TUser> : IPermissionChecker, ITransientDependency
        where TRole : LMPRole<TTenant, TUser>, new()
        where TUser : LMPUser<TTenant, TUser>
        where TTenant : LMPTenant<TTenant, TUser>
    {
        private readonly LMPUserManager<TTenant, TRole, TUser> _userManager;

        public ILogger Logger { get; set; }

        public IAbpSession AbpSession { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected PermissionChecker(LMPUserManager<TTenant, TRole, TUser> userManager)
        {
            _userManager = userManager;

            Logger = NullLogger.Instance;
            AbpSession = NullAbpSession.Instance;
        }

        public async Task<bool> IsGrantedAsync(string permissionName)
        {
            return AbpSession.UserId.HasValue && await _userManager.IsGrantedAsync(AbpSession.UserId.Value, permissionName);
        }

        public async Task<bool> IsGrantedAsync(long userId, string permissionName)
        {
            return await _userManager.IsGrantedAsync(userId, permissionName);
        }
    }
}
