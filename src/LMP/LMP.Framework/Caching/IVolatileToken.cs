using Abp.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMP.Caching
{
    public interface IVolatileToken
    {
        bool IsCurrent { get; }
    }
}
