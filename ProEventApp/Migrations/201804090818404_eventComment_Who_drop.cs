namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eventComment_Who_drop : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.EventComments", "Who");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EventComments", "Who", c => c.String());
        }
    }
}
