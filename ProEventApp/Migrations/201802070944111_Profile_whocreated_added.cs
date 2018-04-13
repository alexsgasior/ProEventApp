namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Profile_whocreated_added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Profiles", "WhoCreated", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Profiles", "WhoCreated");
        }
    }
}
