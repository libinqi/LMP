namespace LMP.TaskSystem.Migrations
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
                        Id = c.Long(nullable: false, identity: true),
                        AssignedUserId = c.Long(),
                        Description = c.String(unicode: false),
                        State = c.Byte(nullable: false),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LMPUsers", t => t.AssignedUserId)
                .ForeignKey("dbo.LMPUsers", t => t.CreatorUserId)
                .Index(t => t.AssignedUserId)
                .Index(t => t.CreatorUserId);            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "CreatorUserId", "dbo.LMPUsers");
            DropForeignKey("dbo.Tasks", "AssignedUserId", "dbo.LMPUsers");          
            DropIndex("dbo.Tasks", new[] { "CreatorUserId" });
            DropIndex("dbo.Tasks", new[] { "AssignedUserId" });            
            DropTable("dbo.Tasks");
            
        }
    }
}
