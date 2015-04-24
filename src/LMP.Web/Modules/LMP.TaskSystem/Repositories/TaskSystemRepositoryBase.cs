using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using LMP.TaskSystem.EntityFramework;

namespace LMP.TaskSystem.Repositories
{
    public abstract class TaskSystemRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<TaskSystemDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected TaskSystemRepositoryBase(IDbContextProvider<TaskSystemDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }

    public abstract class TaskSystemRepositoryBase<TEntity> : TaskSystemRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected TaskSystemRepositoryBase(IDbContextProvider<TaskSystemDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}
