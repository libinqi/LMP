using LMP.Data.MySql;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using LMP.Users.EntityFramework;
using LMP.Users.Migrations.Data;

namespace LMP.Users.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<UserDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;

            ContextKey = "UserDbContext";

            // 注册mysql代码生成器
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
            //迁移历史过滤，避免mysql字段长度溢出.
            SetHistoryContextFactory("MySql.Data.MySqlClient", (conn, schema) => new MySqlHistoryContext(conn, schema));
        }

        protected override void Seed(UserDbContext context)
        {
            new InitialDataBuilder().Build(context);
        }
    }
}
