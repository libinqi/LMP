using LMP.Core.Security.Users;
using LMP.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMP.Core.MultiTenancy
{
    public class Tenant : LMPTenant<Tenant, User>
    {
        protected Tenant()
        {

        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}
