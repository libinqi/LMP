using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMP.QuestionSystem
{
    public class QuestionSystemAppServiceBase : ApplicationService
    {
        public QuestionSystemAppServiceBase() {
            LocalizationSourceName = QuestionSystemConsts.LocalizationSourceName;
        }
    }
}
