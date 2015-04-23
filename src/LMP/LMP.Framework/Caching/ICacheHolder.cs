using Abp.Dependency;
using System;

namespace LMP.Caching
{
    public interface ICacheHolder : ISingletonDependency {
        ICache<TKey, TResult> GetCache<TKey, TResult>(Type component);
    }
}
