using LMP.Authorization.Roles;
using LMP.Authorization.Users;
using LMP.Core.MultiTenancy;
using LMP.Core.Security.Roles;
using LMP.Core.Security.Users;
using LMP.TaskSystem.Domain;
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
            //Default tenant

            var defaultTenant = context.Tenants.FirstOrDefault(t => t.TenancyName == "Default");
            if (defaultTenant == null)
            {
                defaultTenant = context.Tenants.Add(new Tenant("Default", "Default"));
                context.SaveChanges();
            }

            //User role for 'Default' tenant

            var userRoleForDefaultTenant = context.Roles.FirstOrDefault(r => r.TenantId == defaultTenant.Id && r.Name == "User");
            if (userRoleForDefaultTenant == null)
            {
                userRoleForDefaultTenant = context.Roles.Add(new Role(defaultTenant.Id, "User", "User"));
                context.SaveChanges();

            }

            //hezhiwei for 'Default' tenant

            var UserForDefaultTenant = context.Users.FirstOrDefault(u => u.TenantId == defaultTenant.Id && u.UserName == "hezhiwei");
            if (UserForDefaultTenant == null)
            {
                UserForDefaultTenant = context.Users.Add(
                    new User
                    {
                        TenantId = defaultTenant.Id,
                        UserName = "hezhiwei",
                        Name = "hezhiwei",
                        Surname = "��־��",
                        EmailAddress = "hezhiwei@jt56.org",
                        IsEmailConfirmed = true,
                        Password = "AM4OLBpptxBYmM79lGOX9egzZk3vIQU3d/gFCJzaBjAPXzYIK3tQ2N7X4fcrHtElTw==" //123qwe
                    });
                context.SaveChanges();

                context.UserRoles.Add(new UserRole(UserForDefaultTenant.Id, userRoleForDefaultTenant.Id));
                context.SaveChanges();

                var task = context.Tasks.Add(
                    new Task {
                        Description="����Ż���վ��̨������Ƽ�ԭ��ͼ",
                        State=TaskState.Active
                     }
                    );
                context.SaveChanges();

                task.CreatorUserId = UserForDefaultTenant.Id;
                task.AssignedUserId = UserForDefaultTenant.Id;
                context.SaveChanges();
            }

            //deweifan for 'Default' tenant

            UserForDefaultTenant = context.Users.FirstOrDefault(u => u.TenantId == defaultTenant.Id && u.UserName == "deweifan");
            if (UserForDefaultTenant == null)
            {
                UserForDefaultTenant = context.Users.Add(
                    new User
                    {
                        TenantId = defaultTenant.Id,
                        UserName = "dengweifan",
                        Name = "dengweifan",
                        Surname = "��쿷�",
                        EmailAddress = "dengweifan@jt56.org",
                        IsEmailConfirmed = true,
                        Password = "AM4OLBpptxBYmM79lGOX9egzZk3vIQU3d/gFCJzaBjAPXzYIK3tQ2N7X4fcrHtElTw==" //123qwe
                    });
                context.SaveChanges();

                context.UserRoles.Add(new UserRole(UserForDefaultTenant.Id, userRoleForDefaultTenant.Id));
                context.SaveChanges();

                var task = context.Tasks.Add(
                    new Task
                    {
                        Description = "���¡��ͣ���ͻ��˵Ľӿڸ���",
                        State = TaskState.Active
                    }
                    );
                context.SaveChanges();

                task.CreatorUserId = UserForDefaultTenant.Id;
                task.AssignedUserId = UserForDefaultTenant.Id;
                context.SaveChanges();
            }

            //libinqi for 'Default' tenant

            UserForDefaultTenant = context.Users.FirstOrDefault(u => u.TenantId == defaultTenant.Id && u.UserName == "libinqi");
            if (UserForDefaultTenant == null)
            {
                UserForDefaultTenant = context.Users.Add(
                    new User
                    {
                        TenantId = defaultTenant.Id,
                        UserName = "libinqi",
                        Name = "libinqi",
                        Surname = "�����",
                        EmailAddress = "libinqi@jt56.org",
                        IsEmailConfirmed = true,
                        Password = "AM4OLBpptxBYmM79lGOX9egzZk3vIQU3d/gFCJzaBjAPXzYIK3tQ2N7X4fcrHtElTw==" //123qwe
                    });
                context.SaveChanges();

                context.UserRoles.Add(new UserRole(UserForDefaultTenant.Id, userRoleForDefaultTenant.Id));
                context.SaveChanges();

                var task = context.Tasks.Add(
                    new Task
                    {
                        Description = "���ƽ̨Ӧ�ÿ�����ܵĴ��DEMO�Ŀ���",
                        State = TaskState.Active
                    }
                    );
                context.SaveChanges();

                task.CreatorUserId = UserForDefaultTenant.Id;
                task.AssignedUserId = UserForDefaultTenant.Id;
                context.SaveChanges();
            }
        
        }
    }
}