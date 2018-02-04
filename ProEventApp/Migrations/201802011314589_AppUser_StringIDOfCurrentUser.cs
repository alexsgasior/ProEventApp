namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppUser_StringIDOfCurrentUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppUser", "CurrentUserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppUser", "CurrentUserId");
        }
    }
}
