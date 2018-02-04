namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class image_base64_added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "ContentBase64", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Images", "ContentBase64");
        }
    }
}
