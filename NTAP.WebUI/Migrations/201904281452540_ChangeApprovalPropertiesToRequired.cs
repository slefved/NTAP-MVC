namespace NTAP.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeApprovalPropertiesToRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("App.Approval", "ObjectType", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("App.Approval", "ActionType", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("App.Approval", "Data", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("App.Approval", "Data", c => c.String());
            AlterColumn("App.Approval", "ActionType", c => c.String(maxLength: 50));
            AlterColumn("App.Approval", "ObjectType", c => c.String(maxLength: 50));
        }
    }
}
