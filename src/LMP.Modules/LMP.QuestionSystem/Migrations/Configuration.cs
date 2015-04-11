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

            // ע��mysql����������
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
            //Ǩ����ʷ���ˣ�����mysql�ֶγ������.
            SetHistoryContextFactory("MySql.Data.MySqlClient", (conn, schema) => new MySqlHistoryContext(conn, schema)); 
        }

        protected override void Seed(QuestionSystemDbContext context)
        {
            new InitialDataBuilder().Build(context);
        }
    }
}
