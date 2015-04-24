using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using LMP.QuestionSystem.EntityFramework;

namespace LMP.QuestionSystem.Repositories
{
    public abstract class QuestionSystemRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<QuestionSystemDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected QuestionSystemRepositoryBase(IDbContextProvider<QuestionSystemDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }

    public abstract class QuestionSystemRepositoryBase<TEntity> : QuestionSystemRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected QuestionSystemRepositoryBase(IDbContextProvider<QuestionSystemDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}
