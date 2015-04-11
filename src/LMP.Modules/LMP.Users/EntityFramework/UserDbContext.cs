using System.Data.Common;
using System.Data.Entity;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Configuration;
using Abp.EntityFramework;
using Abp.MultiTenancy;
using LMP.Authorization.Roles;
using LMP.MultiTenancy;
using LMP.Authorization.Users;
using LMP.Authorization;
using LMP.Configuration;
using LMP.Auditing;
using LMP.Core.MultiTenancy;
using LMP.Core.Security.Roles;
using LMP.Core.Security.Users;

namespace LMP.Users.EntityFramework
{
    /// <summary>
    /// DbContext for User.
    /// </summary>
    public class UserDbContext : AbpDbContext
    {
        /// <summary>
        /// Tenants
        /// </summary>
        public virtual IDbSet<Tenant> Tenants { get; set; }

        /// <summary>
        /// Roles.
        /// </summary>
        public virtual IDbSet<Role> Roles { get; set; }

        /// <summary>
        /// Users.
        /// </summary>
        public virtual IDbSet<User> Users { get; set; }

        /// <summary>
        /// User logins.
        /// </summary>
        public virtual IDbSet<UserLogin> UserLogins { get; set; }

        /// <summary>
        /// User roles.
        /// </summary>
        public virtual IDbSet<UserRole> UserRoles { get; set; }

        /// <summary>
        /// Permissions.
        /// </summary>
        public virtual IDbSet<PermissionSetting> Permissions { get; set; }

        /// <summary>
        /// Role permissions.
        /// </summary>
        public virtual IDbSet<RolePermissionSetting> RolePermissions { get; set; }
        
        /// <summary>
        /// User permissions.
        /// </summary>
        public virtual IDbSet<UserPermissionSetting> UserPermissions { get; set; }

        /// <summary>
        /// Settings.
        /// </summary>
        public virtual IDbSet<Setting> Settings { get; set; }

        /// <summary>
        /// Audit logs.
        /// </summary>
        public virtual IDbSet<AuditLog> AuditLogs { get; set; }

        public UserDbContext()
            : base("Default")
        {

        }

        public UserDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            
        }

        //This constructor is used in tests
        public UserDbContext(DbConnection connection)
            : base(connection, true)
        {

        }
    }
}
