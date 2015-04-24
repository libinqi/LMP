using LMP.Authorization.Roles;
using LMP.Authorization.Users;
using LMP.Core.MultiTenancy;
using LMP.Core.Security.Roles;
using LMP.Core.Security.Users;
using LMP.QuestionSystem.Domain.Questions;
using LMP.QuestionSystem.EntityFramework;
using System.Linq;
using System.Linq.Dynamic;
using EntityFramework.DynamicFilters;

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
            //Default tenant

            var defaultTenant = context.Tenants.FirstOrDefault(t => t.TenancyName == "Default");
            if (defaultTenant == null)
            {
                defaultTenant = context.Tenants.Add(new Tenant("Default", "Default"));
                context.SaveChanges();
            }

            //Admin role for 'Default' tenant

            var adminRoleForDefaultTenant = context.Roles.FirstOrDefault(r => r.TenantId == defaultTenant.Id && r.Name == "Admin");
            if (adminRoleForDefaultTenant == null)
            {
                adminRoleForDefaultTenant = context.Roles.Add(new Role(defaultTenant.Id, "Admin", "Admin"));
                context.SaveChanges();

                //Permission definitions for Admin of 'Default' tenant
                context.Permissions.Add(new RolePermissionSetting { RoleId = adminRoleForDefaultTenant.Id, Name = "CanDeleteAnswers", IsGranted = true });
                context.Permissions.Add(new RolePermissionSetting { RoleId = adminRoleForDefaultTenant.Id, Name = "CanDeleteQuestions", IsGranted = true });
                context.SaveChanges();
            }
            else
            {
                //Permission definitions for Admin of 'Default' tenant
                context.Permissions.Add(new RolePermissionSetting { RoleId = adminRoleForDefaultTenant.Id, Name = "CanDeleteAnswers", IsGranted = true });
                context.Permissions.Add(new RolePermissionSetting { RoleId = adminRoleForDefaultTenant.Id, Name = "CanDeleteQuestions", IsGranted = true });
                context.SaveChanges();
            }

            //User role for 'Default' tenant

            var userRoleForDefaultTenant = context.Roles.FirstOrDefault(r => r.TenantId == defaultTenant.Id && r.Name == "User");
            if (userRoleForDefaultTenant == null)
            {
                userRoleForDefaultTenant = context.Roles.Add(new Role(defaultTenant.Id, "User", "User"));
                context.SaveChanges();

                //Permission definitions for User of 'Default' tenant
                context.Permissions.Add(new RolePermissionSetting { RoleId = userRoleForDefaultTenant.Id, Name = "CanCreateQuestions", IsGranted = true });
                context.SaveChanges();
            }
            else
            {
                context.Permissions.Add(new RolePermissionSetting { RoleId = userRoleForDefaultTenant.Id, Name = "CanCreateQuestions", IsGranted = true });
                context.SaveChanges();
            }

            //Admin for 'Default' tenant

            var adminUserForDefaultTenant = context.Users.FirstOrDefault(u => u.TenantId == defaultTenant.Id && u.UserName == "admin");
            if (adminUserForDefaultTenant == null)
            {
                adminUserForDefaultTenant = context.Users.Add(
                    new User
                    {
                        TenantId = defaultTenant.Id,
                        UserName = "admin",
                        Name = "admin",
                        Surname = "Administrator",
                        EmailAddress = "admin@admin.com",
                        IsEmailConfirmed = true,
                        Password = "AM4OLBpptxBYmM79lGOX9egzZk3vIQU3d/gFCJzaBjAPXzYIK3tQ2N7X4fcrHtElTw==" //123qwe
                    });
                context.SaveChanges();

                context.UserRoles.Add(new UserRole(adminUserForDefaultTenant.Id, adminRoleForDefaultTenant.Id));
                context.UserRoles.Add(new UserRole(adminUserForDefaultTenant.Id, userRoleForDefaultTenant.Id));
                context.SaveChanges();

                var question1 = context.Questions.Add(
                    new Question(
                        "生命、宇宙和一切的终极问题的答案是什么?",
                        "生命、宇宙和一切的终极问题的答案是什么? 请回答这个问题!"
                        )
                    );
                context.SaveChanges();

                question1.CreatorUserId = adminUserForDefaultTenant.Id;
                context.SaveChanges();
            }
            else
            {
                context.UserRoles.Add(new UserRole(adminUserForDefaultTenant.Id, adminRoleForDefaultTenant.Id));
                context.UserRoles.Add(new UserRole(adminUserForDefaultTenant.Id, userRoleForDefaultTenant.Id));
                context.SaveChanges();

                var question1 = context.Questions.Add(
                new Question(
                    "生命、宇宙和一切的终极问题的答案是什么?",
                    "生命、宇宙和一切的终极问题的答案是什么? 请回答这个问题!"
                    )
                );
                context.SaveChanges();

                question1.CreatorUserId = adminUserForDefaultTenant.Id;
                context.SaveChanges();
            }

            //User 'Dev' for 'Default' tenant

            var devUserForDefaultTenant = context.Users.FirstOrDefault(u => u.TenantId == defaultTenant.Id && u.UserName == "dev");
            if (devUserForDefaultTenant == null)
            {
                devUserForDefaultTenant = context.Users.Add(
                    new User
                    {
                        TenantId = defaultTenant.Id,
                        UserName = "dev",
                        Name = "Web Dev",
                        Surname = "UED",
                        EmailAddress = "dev@dev.com",
                        IsEmailConfirmed = true,
                        Password = "AM4OLBpptxBYmM79lGOX9egzZk3vIQU3d/gFCJzaBjAPXzYIK3tQ2N7X4fcrHtElTw==" //123qwe
                    });
                context.SaveChanges();

                context.UserRoles.Add(new UserRole(devUserForDefaultTenant.Id, userRoleForDefaultTenant.Id));
                context.SaveChanges();

                var question2 = context.Questions.Add(
                    new Question(
                        "Jquery的替换方法，在我的js函数中调用不工作",
                        @"我想实现的是这样的一个功能，当用户点击Checkbox框为选中的时候样式变成绿色的。 但是,我也想用户通过点击Checkbox框的文本内容同时也可以为“选中”状态，同时改变文本的内容。"
                        )
                    );
                context.SaveChanges();

                question2.CreatorUserId = devUserForDefaultTenant.Id;
                context.SaveChanges();
            }
        }
    }
}