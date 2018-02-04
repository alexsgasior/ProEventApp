namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppEvent_added_City_street_and_houseNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppEvents", "CityId", c => c.Int(nullable: false));
            AddColumn("dbo.AppEvents", "Street", c => c.String());
            AddColumn("dbo.AppEvents", "HouseNumber", c => c.String());
            CreateIndex("dbo.AppEvents", "CityId");
            AddForeignKey("dbo.AppEvents", "CityId", "dbo.Cities", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AppEvents", "CityId", "dbo.Cities");
            DropIndex("dbo.AppEvents", new[] { "CityId" });
            DropColumn("dbo.AppEvents", "HouseNumber");
            DropColumn("dbo.AppEvents", "Street");
            DropColumn("dbo.AppEvents", "CityId");
        }
    }
}
