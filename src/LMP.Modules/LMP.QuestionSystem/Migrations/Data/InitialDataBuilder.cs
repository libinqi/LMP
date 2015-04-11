using LMP.Authorization.Roles;
using LMP.Authorization.Users;
using LMP.Core.MultiTenancy;
using LMP.Core.Security.Roles;
using LMP.Core.Security.Users;
using LMP.QuestionSystem.EntityFramework;
using System.Linq;

namespace LMP.QuestionSystem.Migrations.Data
{
    public class InitialDataBuilder
    {
        public void Build(QuestionSystemDbContext context)
        {
            CreateQuestions(context);
        }

        private void CreateQuestions(QuestionSystemDbContext context)
        {
        }
    }
}