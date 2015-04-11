using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMP.Users
{
    public class UserAppServiceBase : ApplicationService
    {
        public UserAppServiceBase()
        {
            LocalizationSourceName = UserConsts.LocalizationSourceName;
        }
    }
}
