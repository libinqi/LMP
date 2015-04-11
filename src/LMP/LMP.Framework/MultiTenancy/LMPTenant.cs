using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Configuration;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using LMP.Authorization.Users;
using LMP.Configuration;

namespace LMP.MultiTenancy
{
    /// <summary>
    /// Represents a Tenant of the application.
    /// </summary>
    [Table("LMPTenants")]
    public class LMPTenant<TTenant, TUser> : FullAuditedEntity<int, TUser>, IPassivable
        where TUser : LMPUser<TTenant, TUser>
        where TTenant : LMPTenant<TTenant, TUser>
    {
        /// <summary>
        /// Max length of the <see cref="TenancyName"/> property.
        /// </summary>
        public const int MaxTenancyNameLength = 64;

        /// <summary>
        /// Max length of the <see cref="Name"/> property.
        /// </summary>
        public const int MaxNameLength = 128;
        
        /// <summary>
        /// Tenancy name. This property is the UNIQUE name of this Tenant.
        /// It can be used as subdomain name in a web application.
        /// </summary>
        [Required]
        [StringLength(MaxTenancyNameLength)]
        public virtual string TenancyName { get; set; }

        /// <summary>
        /// Display name of the Tenant.
        /// </summary>
        [Required]
        [StringLength(MaxNameLength)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Is this tenant active?
        /// If as tenant is not active, no user of this tenant can use the application.
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// Defined settings for this tenant.
        /// </summary>
        [ForeignKey("TenantId")]
        public virtual ICollection<Setting> Settings { get; set; }

        /// <summary>
        /// Creates a new tenant.
        /// </summary>
        public LMPTenant()
        {
            IsActive = true;
        }

        /// <summary>
        /// Creates a new tenant.
        /// </summary>
        /// <param name="tenancyName">UNIQUE name of this Tenant</param>
        /// <param name="name">Display name of the Tenant</param>
        public LMPTenant(string tenancyName, string name)
            : this()
        {
            TenancyName = tenancyName;
            Name = name;
        }
    }
}
