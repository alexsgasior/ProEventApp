namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Professional_CurrentIdUser_upgrade : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Professional", "CurrentUserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Professional", "CurrentUserId");
        }
    }
}
