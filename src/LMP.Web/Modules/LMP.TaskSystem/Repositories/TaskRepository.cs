using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using Abp.EntityFramework;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using LMP.TaskSystem.Domain;
using LMP.TaskSystem.EntityFramework;

namespace LMP.TaskSystem.Repositories
{
    /// <summary>
    /// Implements <see cref="ITaskRepository"/> for EntityFramework ORM.
    /// </summary>
    public class TaskRepository :TaskSystemRepositoryBase<Task, int>, ITaskRepository
    {
        public TaskRepository(IDbContextProvider<TaskSystemDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public List<Task> GetAllWithPeople(long? assignedPersonId, TaskState? state)
        {
            //In repository methods, we do not deal with create/dispose DB connections, DbContexes and transactions. ABP handles it.

            var query = GetAll(); //GetAll() returns IQueryable<T>, so we can query over it.
            //var query = Context.Tasks.AsQueryable(); //Alternatively, we can directly use EF's DbContext object.
            //var query = Table.AsQueryable(); //Another alternative: We can directly use 'Table' property instead of 'Context.Tasks', they are identical.

            //Add some Where conditions...

            if (assignedPersonId.HasValue)
            {
                query = query.Where(task => task.CreatorUserId == assignedPersonId.Value);
            }

            if (state.HasValue)
            {
                query = query.Where(task => task.State == state);
            }

            return query
                .OrderByDescending(task => task.CreationTime)
                .Include(task => task.CreatorUser) //Include assigned person in a single query
                .ToList();
        }
    }
}
