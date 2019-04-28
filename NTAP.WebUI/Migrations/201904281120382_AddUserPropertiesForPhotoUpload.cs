namespace NTAP.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserPropertiesForPhotoUpload : DbMigration
    {
        public override void Up()
        {
            AddColumn("App.User", "Photo", c => c.Binary());
            AddColumn("App.User", "MimeType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("App.User", "MimeType");
            DropColumn("App.User", "Photo");
        }
    }
}
