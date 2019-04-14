namespace NTAP.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "App.Role",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(nullable: false, maxLength: 255),
                        IsDeleted = c.Boolean(nullable: false),
                        CreateBy = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateBy = c.Int(),
                        UpdateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.RoleID)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "App.UserRole",
                c => new
                    {
                        UserID = c.Int(nullable: false),
                        RoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.RoleID })
                .ForeignKey("App.Role", t => t.RoleID, cascadeDelete: true)
                .ForeignKey("App.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.RoleID);
            
            CreateTable(
                "App.User",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 256),
                        FullName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(maxLength: 256),
                        IsDeleted = c.Boolean(nullable: false),
                        CreateBy = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateBy = c.Int(),
                        UpdateTime = c.DateTime(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserID)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "App.UserClaim",
                c => new
                    {
                        UserClaimID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.UserClaimID)
                .ForeignKey("App.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "App.UserLogin",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserID })
                .ForeignKey("App.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("App.UserRole", "UserID", "App.User");
            DropForeignKey("App.UserLogin", "UserID", "App.User");
            DropForeignKey("App.UserClaim", "UserID", "App.User");
            DropForeignKey("App.UserRole", "RoleID", "App.Role");
            DropIndex("App.UserLogin", new[] { "UserID" });
            DropIndex("App.UserClaim", new[] { "UserID" });
            DropIndex("App.User", "UserNameIndex");
            DropIndex("App.UserRole", new[] { "RoleID" });
            DropIndex("App.UserRole", new[] { "UserID" });
            DropIndex("App.Role", "RoleNameIndex");
            DropTable("App.UserLogin");
            DropTable("App.UserClaim");
            DropTable("App.User");
            DropTable("App.UserRole");
            DropTable("App.Role");
        }
    }
}
