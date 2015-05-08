namespace LMP.Users.Migrations
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
                "dbo.LMPAuditLogs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        UserId = c.Long(),
                        ServiceName = c.String(maxLength: 256, storeType: "nvarchar"),
                        MethodName = c.String(maxLength: 256, storeType: "nvarchar"),
                        Parameters = c.String(maxLength: 1024, storeType: "nvarchar"),
                        ExecutionTime = c.DateTime(nullable: false, precision: 0),
                        ExecutionDuration = c.Int(nullable: false),
                        ClientIpAddress = c.String(maxLength: 64, storeType: "nvarchar"),
                        ClientName = c.String(maxLength: 128, storeType: "nvarchar"),
                        BrowserInfo = c.String(maxLength: 256, storeType: "nvarchar"),
                        Exception = c.String(maxLength: 2000, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LMPPermissions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        IsGranted = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                        RoleId = c.Int(),
                        UserId = c.Long(),
                        Discriminator = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LMPUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.LMPRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.LMPRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenantId = c.Int(),
                        Name = c.String(nullable: false, maxLength: 32, storeType: "nvarchar"),
                        DisplayName = c.String(nullable: false, maxLength: 64, storeType: "nvarchar"),
                        IsStatic = c.Boolean(nullable: false),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Role_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LMPUsers", t => t.CreatorUserId)
                .ForeignKey("dbo.LMPUsers", t => t.LastModifierUserId)
                .ForeignKey("dbo.LMPTenants", t => t.TenantId)
                .Index(t => t.TenantId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "dbo.LMPUsers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        Name = c.String(nullable: false, maxLength: 32, storeType: "nvarchar"),
                        Surname = c.String(nullable: false, maxLength: 32, storeType: "nvarchar"),
                        UserName = c.String(nullable: false, maxLength: 32, storeType: "nvarchar"),
                        Password = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        EmailAddress = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        IsEmailConfirmed = c.Boolean(nullable: false),
                        EmailConfirmationCode = c.String(maxLength: 128, storeType: "nvarchar"),
                        PasswordResetCode = c.String(maxLength: 128, storeType: "nvarchar"),
                        LastLoginTime = c.DateTime(precision: 0),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_User_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_User_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LMPUsers", t => t.CreatorUserId)
                .ForeignKey("dbo.LMPUsers", t => t.DeleterUserId)
                .ForeignKey("dbo.LMPUsers", t => t.LastModifierUserId)
                .ForeignKey("dbo.LMPTenants", t => t.TenantId)
                .Index(t => t.TenantId)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "dbo.LMPUserLogins",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        LoginProvider = c.String(unicode: false),
                        ProviderKey = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LMPUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.LMPUserRoles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        RoleId = c.Int(nullable: false),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LMPUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.LMPSettings",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        UserId = c.Long(),
                        Name = c.String(unicode: false),
                        Value = c.String(unicode: false),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LMPUsers", t => t.UserId)
                .ForeignKey("dbo.LMPTenants", t => t.TenantId)
                .Index(t => t.TenantId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.LMPTenants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenancyName = c.String(nullable: false, maxLength: 64, storeType: "nvarchar"),
                        Name = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Tenant_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LMPUsers", t => t.CreatorUserId)
                .ForeignKey("dbo.LMPUsers", t => t.DeleterUserId)
                .ForeignKey("dbo.LMPUsers", t => t.LastModifierUserId)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LMPRoles", "TenantId", "dbo.LMPTenants");
            DropForeignKey("dbo.LMPPermissions", "RoleId", "dbo.LMPRoles");
            DropForeignKey("dbo.LMPRoles", "LastModifierUserId", "dbo.LMPUsers");
            DropForeignKey("dbo.LMPRoles", "CreatorUserId", "dbo.LMPUsers");
            DropForeignKey("dbo.LMPUsers", "TenantId", "dbo.LMPTenants");
            DropForeignKey("dbo.LMPSettings", "TenantId", "dbo.LMPTenants");
            DropForeignKey("dbo.LMPTenants", "LastModifierUserId", "dbo.LMPUsers");
            DropForeignKey("dbo.LMPTenants", "DeleterUserId", "dbo.LMPUsers");
            DropForeignKey("dbo.LMPTenants", "CreatorUserId", "dbo.LMPUsers");
            DropForeignKey("dbo.LMPSettings", "UserId", "dbo.LMPUsers");
            DropForeignKey("dbo.LMPUserRoles", "UserId", "dbo.LMPUsers");
            DropForeignKey("dbo.LMPPermissions", "UserId", "dbo.LMPUsers");
            DropForeignKey("dbo.LMPUserLogins", "UserId", "dbo.LMPUsers");
            DropForeignKey("dbo.LMPUsers", "LastModifierUserId", "dbo.LMPUsers");
            DropForeignKey("dbo.LMPUsers", "DeleterUserId", "dbo.LMPUsers");
            DropForeignKey("dbo.LMPUsers", "CreatorUserId", "dbo.LMPUsers");
            DropIndex("dbo.LMPTenants", new[] { "CreatorUserId" });
            DropIndex("dbo.LMPTenants", new[] { "LastModifierUserId" });
            DropIndex("dbo.LMPTenants", new[] { "DeleterUserId" });
            DropIndex("dbo.LMPSettings", new[] { "UserId" });
            DropIndex("dbo.LMPSettings", new[] { "TenantId" });
            DropIndex("dbo.LMPUserRoles", new[] { "UserId" });
            DropIndex("dbo.LMPUserLogins", new[] { "UserId" });
            DropIndex("dbo.LMPUsers", new[] { "CreatorUserId" });
            DropIndex("dbo.LMPUsers", new[] { "LastModifierUserId" });
            DropIndex("dbo.LMPUsers", new[] { "DeleterUserId" });
            DropIndex("dbo.LMPUsers", new[] { "TenantId" });
            DropIndex("dbo.LMPRoles", new[] { "CreatorUserId" });
            DropIndex("dbo.LMPRoles", new[] { "LastModifierUserId" });
            DropIndex("dbo.LMPRoles", new[] { "TenantId" });
            DropIndex("dbo.LMPPermissions", new[] { "UserId" });
            DropIndex("dbo.LMPPermissions", new[] { "RoleId" });
            DropTable("dbo.LMPTenants",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Tenant_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.LMPSettings");
            DropTable("dbo.LMPUserRoles");
            DropTable("dbo.LMPUserLogins");
            DropTable("dbo.LMPUsers",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_User_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_User_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.LMPRoles",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Role_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.LMPPermissions");
            DropTable("dbo.LMPAuditLogs");
        }
    }
}
