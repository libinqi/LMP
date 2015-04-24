using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
using LMP.TaskSystem.Migrations.Data;
using LMP.TaskSystem.EntityFramework;
using LMP.Core.Data.MySql;

namespace LMP.TaskSystem.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<TaskSystemDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "TaskSystemDbContext";

            // ע��mysql����������
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
            //Ǩ����ʷ���ˣ�����mysql�ֶγ������.
            SetHistoryContextFactory("MySql.Data.MySqlClient", (conn, schema) => new MySqlHistoryContext(conn, schema)); 
        }

        protected override void Seed(TaskSystemDbContext context)
        {
            new InitialDataBuilder().Build(context);
        }
    }
}
