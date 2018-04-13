namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class guid_added_to_AppEvent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppEvents", "Findid", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppEvents", "Findid");
        }
    }
}
