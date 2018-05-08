namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class advertisement_findingId_added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Advertisements", "FindId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Advertisements", "FindId");
        }
    }
}
