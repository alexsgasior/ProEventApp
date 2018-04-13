namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class date_added_to_AppEvent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppEvents", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppEvents", "Date");
        }
    }
}
