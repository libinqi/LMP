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
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(unicode: false),
                        QuestionId = c.Int(nullable: false),
                        IsAccepted = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LMPUsers", t => t.CreatorUserId)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId)
                .Index(t => t.CreatorUserId);            
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(unicode: false),
                        Text = c.String(unicode: false),
                        VoteCount = c.Int(nullable: false),
                        AnswerCount = c.Int(nullable: false),
                        ViewCount = c.Int(nullable: false),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LMPUsers", t => t.CreatorUserId)
                .Index(t => t.CreatorUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "CreatorUserId", "dbo.LMPUsers");
            DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Answers", "CreatorUserId", "dbo.LMPUsers");        
            DropIndex("dbo.Questions", new[] { "CreatorUserId" });          
            DropIndex("dbo.Answers", new[] { "CreatorUserId" });
            DropIndex("dbo.Answers", new[] { "QuestionId" });
            DropTable("dbo.Questions");         
            DropTable("dbo.Answers");
        }
    }
}
