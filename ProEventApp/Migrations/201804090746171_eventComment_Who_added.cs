namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eventComment_Who_added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EventComments", "Who", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EventComments", "Who");
        }
    }
}
