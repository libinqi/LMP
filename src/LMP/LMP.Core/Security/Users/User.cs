using LMP.Authorization.Users;
using LMP.Core.MultiTenancy;
using LMP.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMP.Core.Security.Users
{
    public class User : LMPUser<Tenant, User>
    {
        public override string ToString()
        {
            return string.Format("[User {0}] {1}", Id, UserName);
        }
    }
}
