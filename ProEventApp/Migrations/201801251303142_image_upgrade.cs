namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class image_upgrade : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "Title", c => c.String());
            AddColumn("dbo.Images", "ImagePath", c => c.String());
            DropColumn("dbo.Images", "Name");
            DropColumn("dbo.Images", "ImageUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "ImageUrl", c => c.String());
            AddColumn("dbo.Images", "Name", c => c.String());
            DropColumn("dbo.Images", "ImagePath");
            DropColumn("dbo.Images", "Title");
        }
    }
}
