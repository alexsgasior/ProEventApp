namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Image_CHANGE : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "ImageUrl", c => c.String());
            DropColumn("dbo.Images", "DateAdded");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "DateAdded", c => c.DateTime(nullable: false));
            DropColumn("dbo.Images", "ImageUrl");
        }
    }
}
