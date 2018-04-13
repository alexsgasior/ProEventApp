namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validation_added_to_Address : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Addresses", "Street", c => c.String(nullable: false));
            AlterColumn("dbo.Addresses", "HouseNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Addresses", "HouseNumber", c => c.String());
            AlterColumn("dbo.Addresses", "Street", c => c.String());
        }
    }
}
