using LMP.Authorization.Roles;
using LMP.Authorization.Users;
using LMP.Core.MultiTenancy;
using LMP.Core.Security.Users;
using LMP.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMP.Core.Security.Roles
{
    public class Role : LMPRole<Tenant, User>
    {
        public Role()
        {

        }

        public Role(int? tenantId, string name, string displayName)
            : base(tenantId, name, displayName)
        {

        }
    }
}
