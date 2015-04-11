namespace LMP.QuestionSystem.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(unicode: false),
                        State = c.Byte(nullable: false),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LMPUsers", t => t.CreatorUserId)
                .Index(t => t.CreatorUserId);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "CreatorUserId", "dbo.LMPUsers");
            DropIndex("dbo.Tasks", new[] { "CreatorUserId" });
            DropTable("dbo.Tasks");
        }
    }
}
