namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validation_added_to_Category : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Categories", "Description", c => c.String(nullable: false, maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false));
        }
    }
}
