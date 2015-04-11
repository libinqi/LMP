using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using LMP.Authorization.Users;

namespace LMP.MultiTenancy
{
    /// <summary>
    /// Implement this interface for an entity which must have Tenant.
    /// </summary>
    public interface IMustHaveTenant<TTenant, TUser> : IMustHaveTenant, IFilterByTenant
        where TTenant : LMPTenant<TTenant, TUser>
        where TUser : LMPUser<TTenant, TUser>
    {
        /// <summary>
        /// Tenant.
        /// </summary>
        TTenant Tenant { get; set; }
    }
}
