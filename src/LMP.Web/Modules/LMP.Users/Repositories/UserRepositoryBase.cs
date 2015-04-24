using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using LMP.Users.EntityFramework;

namespace LMP.QuestionSystem.Repositories
{
    public abstract class UserRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<UserDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected UserRepositoryBase(IDbContextProvider<UserDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }

    public abstract class UserRepositoryBase<TEntity> : UserRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected UserRepositoryBase(IDbContextProvider<UserDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}
