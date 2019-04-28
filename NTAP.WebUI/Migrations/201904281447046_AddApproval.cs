namespace NTAP.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApproval : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "App.Approval",
                c => new
                    {
                        ApprovalID = c.Int(nullable: false, identity: true),
                        ApprovalStatusID = c.Byte(nullable: false),
                        ObjectType = c.String(maxLength: 50),
                        ActionType = c.String(maxLength: 50),
                        Data = c.String(),
                        PreviousData = c.String(),
                        CreateBy = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        ApprovalBy = c.Int(),
                        ApprovalTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.ApprovalID)
                .ForeignKey("App.ApprovalStatus", t => t.ApprovalStatusID, cascadeDelete: true)
                .Index(t => t.ApprovalStatusID);
            
            CreateTable(
                "App.ApprovalStatus",
                c => new
                    {
                        ApprovalStatusID = c.Byte(nullable: false),
                        Name = c.String(maxLength: 50),
                        Description = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.ApprovalStatusID);
            
            AlterColumn("App.User", "MimeType", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropForeignKey("App.Approval", "ApprovalStatusID", "App.ApprovalStatus");
            DropIndex("App.Approval", new[] { "ApprovalStatusID" });
            AlterColumn("App.User", "MimeType", c => c.String());
            DropTable("App.ApprovalStatus");
            DropTable("App.Approval");
        }
    }
}
