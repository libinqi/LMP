using System.Collections.Generic;
using System.Collections.ObjectModel;
using LMP.Data.MySql;
using System.Data.Entity.Migrations;
using LMP.TaskSystem.Migrations.Data;
using LMP.TaskSystem.EntityFramework;

namespace LMP.QuestionSystem.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<TaskSystemDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "TaskSystemDbContext";

            // 注册mysql代码生成器
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
            //迁移历史过滤，避免mysql字段长度溢出.
            SetHistoryContextFactory("MySql.Data.MySqlClient", (conn, schema) => new MySqlHistoryContext(conn, schema)); 
        }

        protected override void Seed(TaskSystemDbContext context)
        {
            new InitialDataBuilder().Build(context);
        }
    }
}
