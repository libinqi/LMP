using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
using LMP.QuestionSystem.Migrations.Data;
using LMP.QuestionSystem.EntityFramework;
using LMP.Core.Data.MySql;

namespace LMP.QuestionSystem.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<QuestionSystemDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "QuestionSystemDbContext";

            // 注册mysql代码生成器
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
            //迁移历史过滤，避免mysql字段长度溢出.
            SetHistoryContextFactory("MySql.Data.MySqlClient", (conn, schema) => new MySqlHistoryContext(conn, schema)); 
        }

        protected override void Seed(QuestionSystemDbContext context)
        {
            new InitialDataBuilder().Build(context);
        }
    }
}
