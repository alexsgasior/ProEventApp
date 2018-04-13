namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validation_upgradedAddress_AppEvent : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Addresses", "Street", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Addresses", "HouseNumber", c => c.String(nullable: false, maxLength: 25));
            AlterColumn("dbo.AppEvents", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.AppEvents", "Description", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.AppEvents", "Street", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.AppEvents", "HouseNumber", c => c.String(nullable: false, maxLength: 25));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AppEvents", "HouseNumber", c => c.String());
            AlterColumn("dbo.AppEvents", "Street", c => c.String());
            AlterColumn("dbo.AppEvents", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.AppEvents", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Addresses", "HouseNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Addresses", "Street", c => c.String(nullable: false));
        }
    }
}
