using LMP.Authorization.Roles;
using LMP.Authorization.Users;
using LMP.Core.MultiTenancy;
using LMP.Core.Security.Roles;
using LMP.Core.Security.Users;
using LMP.TaskSystem.EntityFramework;
using System.Linq;

namespace LMP.TaskSystem.Migrations.Data
{
    public class InitialDataBuilder
    {
        public void Build(TaskSystemDbContext context)
        {
            CreateTasks(context);
        }

        private void CreateTasks(TaskSystemDbContext context)
        {
        }
    }
}