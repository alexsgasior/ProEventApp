namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tags_added_to_appevent1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppEvents", "Tags", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppEvents", "Tags");
        }
    }
}
