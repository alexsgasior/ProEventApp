namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class image_byteArray : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "Bytes", c => c.Binary());
            DropColumn("dbo.Images", "Title");
            DropColumn("dbo.Images", "ImagePath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "ImagePath", c => c.String());
            AddColumn("dbo.Images", "Title", c => c.String());
            DropColumn("dbo.Images", "Bytes");
        }
    }
}
