namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class done_added_to_AppEvent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppEvents", "Done", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppEvents", "Done");
        }
    }
}
