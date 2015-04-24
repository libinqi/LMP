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
using LMP.Core.EntityFramework;

namespace LMP.Users.EntityFramework
{
    /// <summary>
    /// DbContext for User.
    /// </summary>
    public class UserDbContext : LMPDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for each Entity...

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
