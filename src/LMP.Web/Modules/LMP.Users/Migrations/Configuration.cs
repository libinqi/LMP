using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using LMP.Users.EntityFramework;
using LMP.Users.Migrations.Data;
using LMP.Core.Data.MySql;

namespace LMP.Users.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<UserDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;

            ContextKey = "UserDbContext";

            // ע��mysql����������
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
            //Ǩ����ʷ���ˣ�����mysql�ֶγ������.
            SetHistoryContextFactory("MySql.Data.MySqlClient", (conn, schema) => new MySqlHistoryContext(conn, schema));
        }

        protected override void Seed(UserDbContext context)
        {
            new InitialDataBuilder().Build(context);
        }
    }
}
