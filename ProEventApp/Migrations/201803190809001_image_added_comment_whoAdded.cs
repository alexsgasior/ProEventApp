namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class image_added_comment_whoAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "Comment", c => c.String());
            AddColumn("dbo.Images", "WhoAdded", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Images", "WhoAdded");
            DropColumn("dbo.Images", "Comment");
        }
    }
}
