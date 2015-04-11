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

namespace Abp.Zero.EntityFramework
{
    /// <summary>
    /// DbContext for LMP Core.
    /// </summary>
    public abstract class LMPDbContext<TTenant, TRole, TUser> : AbpDbContext
        where TRole : LMPRole<TTenant, TUser>
        where TTenant : LMPTenant<TTenant, TUser>
        where TUser : LMPUser<TTenant, TUser>
    {
        /// <summary>
        /// Tenants
        /// </summary>
        public virtual IDbSet<TTenant> Tenants { get; set; }

        /// <summary>
        /// Roles.
        /// </summary>
        public virtual IDbSet<TRole> Roles { get; set; }

        /// <summary>
        /// Users.
        /// </summary>
        public virtual IDbSet<TUser> Users { get; set; }

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

        /// <summary>
        /// Default constructor.
        /// Do not directly instantiate this class. Instead, use dependency injection!
        /// </summary>
        protected LMPDbContext()
        {

        }

        /// <summary>
        /// Constructor with connection string parameter.
        /// </summary>
        /// <param name="nameOrConnectionString">Connection string or a name in connection strings in configuration file</param>
        protected LMPDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        /// <summary>
        /// This constructor can be used for unit tests.
        /// </summary>
        protected LMPDbContext(DbConnection dbConnection, bool contextOwnsConnection)
            : base(dbConnection, contextOwnsConnection)
        {
            
        }
    }
}
