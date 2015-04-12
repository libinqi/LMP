using System.Data.Entity.Migrations;
using System.Linq;
using LMP.TaskSystem.Domain;
using LMP.TaskSystem;
using LMP.TaskSystem.EntityFramework;
using LMP.Core.Security.Users;

namespace LMP.TaskSystem.Tests.InitialData
{
    public class TaskSystemInitialDataBuilder
    {
        public void Build(TaskSystemDbContext context)
        {
            //添加一些用户

            context.Users.AddOrUpdate(
                p => p.Name,
                new User { Name = "hezhiwei" },
                new User { Name = "deweifan" },
                new User { Name = "libinqi" }
                );

            context.SaveChanges();

            //添加一些任务

            context.Tasks.AddOrUpdate(
                t => t.Description,
                new Task
                {
                    Description = "my initial task 1"
                },
                new Task
                {
                    Description = "my initial task 2",
                    State = TaskState.Completed
                },
                new Task
                {
                    Description = "my initial task 3",
                    CreatorUser = context.Users.Single(p => p.Name == "hezhiwei")
                },
                new Task
                {
                    Description = "my initial task 4",
                    CreatorUser = context.Users.Single(p => p.Name == "deweifan"),
                    State = TaskState.Completed
                });

            context.SaveChanges();
        }
    }
}
