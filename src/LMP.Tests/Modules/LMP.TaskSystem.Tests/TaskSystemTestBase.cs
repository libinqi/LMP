using System;
using System.Data.Common;
using Abp.Collections;
using Abp.Modules;
using Abp.TestBase;
using Castle.MicroKernel.Registration;
using LMP.TaskSystem.Tests.InitialData;
using LMP.TaskSystem;
using LMP.TaskSystem.EntityFramework;

namespace LMP.TaskSystem.Tests
{
    /// <summary>
    /// This is base class for all our test classes.
    /// It prepares ABP system, modules and a fake, in-memory database.
    /// Seeds database with initial data (<see cref="TaskSystemInitialDataBuilder"/>).
    /// Provides methods to easily work with DbContext.
    /// </summary>
    public abstract class TaskSystemTestBase : AbpIntegratedTestBase
    {
        protected TaskSystemTestBase()
        {
            //Fake DbConnection using Effort!
            LocalIocManager.IocContainer.Register(
                Component.For<DbConnection>()
                    .UsingFactoryMethod(Effort.DbConnectionFactory.CreateTransient)
                    .LifestyleSingleton()
                );

            //Seed initial data
            UsingDbContext(context => new TaskSystemInitialDataBuilder().Build(context));
        }

        protected override void AddModules(ITypeList<AbpModule> modules)
        {
            base.AddModules(modules);

            //Adding testing modules. Depended modules of these modules are automatically added.
            modules.Add<TaskSystemApplicationModule>();
        }

        public void UsingDbContext(Action<TaskSystemDbContext> action)
        {
            using (var context = LocalIocManager.Resolve<TaskSystemDbContext>())
            {
                action(context);
                context.SaveChanges();
            }
        }

        public T UsingDbContext<T>(Func<TaskSystemDbContext, T> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<TaskSystemDbContext>())
            {
                result = func(context);
                context.SaveChanges();
            }

            return result;
        }
    }
}
