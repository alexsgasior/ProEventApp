namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Address_deletedFrom_AppEvent : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AppEvents", "AddressId", "dbo.Addresses");
            DropIndex("dbo.AppEvents", new[] { "AddressId" });
            DropColumn("dbo.AppEvents", "AddressId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AppEvents", "AddressId", c => c.Int(nullable: false));
            CreateIndex("dbo.AppEvents", "AddressId");
            AddForeignKey("dbo.AppEvents", "AddressId", "dbo.Addresses", "Id", cascadeDelete: true);
        }
    }
}
