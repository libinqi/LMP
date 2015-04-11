using Abp.EntityFramework;
using LMP.Core.EntityFramework;
using LMP.Core.MultiTenancy;
using LMP.Core.Security.Roles;
using LMP.Core.Security.Users;
using LMP.QuestionSystem.Domain.Questions;
using System.Data.Common;
using System.Data.Entity;

namespace LMP.QuestionSystem.EntityFramework
{
    public class QuestionSystemDbContext : LMPDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for each Entity...

        //Example:
        public virtual IDbSet<Question> Questions { get; set; }

        public virtual IDbSet<Answer> Answers { get; set; }

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public QuestionSystemDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in QuestionSystemDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of QuestionSystemDbContext since ABP automatically handles it.
         */
        public QuestionSystemDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        public QuestionSystemDbContext(DbConnection connection)
            : base(connection, true)
        {

        }
    }
}
