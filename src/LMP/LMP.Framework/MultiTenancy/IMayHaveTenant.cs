using Abp.Domain.Entities;
using LMP.Authorization.Users;

namespace LMP.MultiTenancy
{
    /// <summary>
    /// Implement this interface for an entity which may have Tenant.
    /// </summary>
    public interface IMayHaveTenant<TTenant, TUser> : IMayHaveTenant
        where TTenant : LMPTenant<TTenant, TUser>
        where TUser : LMPUser<TTenant, TUser>
    {
        /// <summary>
        /// Tenant.
        /// </summary>
        TTenant Tenant { get; set; }
    }
}