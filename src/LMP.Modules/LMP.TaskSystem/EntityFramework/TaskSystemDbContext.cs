using Abp.EntityFramework;
using LMP.TaskSystem.Domain;
using System.Data.Common;
using System.Data.Entity;

namespace LMP.TaskSystem.EntityFramework
{
    public class TaskSystemDbContext : AbpDbContext
    {
        //TODO: Define an IDbSet for each Entity...

        //Example:
        public virtual IDbSet<Task> Tasks { get; set; }

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public TaskSystemDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in TaskSystemDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of TaskSystemDbContext since ABP automatically handles it.
         */
        public TaskSystemDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        public TaskSystemDbContext(DbConnection connection)
            : base(connection, true)
        {

        }
    }
}
