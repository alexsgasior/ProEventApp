namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddressFk_added_to_AppEvent_Person : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.People", "CityId", "dbo.Cities");
            DropIndex("dbo.People", new[] { "CityId" });
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Street = c.String(),
                        HouseNumber = c.String(),
                        PostalCode = c.String(),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
            AddColumn("dbo.People", "AddressId", c => c.Int(nullable: false));
            AddColumn("dbo.AppEvents", "AddressId", c => c.Int(nullable: false));
            CreateIndex("dbo.People", "AddressId");
            CreateIndex("dbo.AppEvents", "AddressId");
            AddForeignKey("dbo.AppEvents", "AddressId", "dbo.Addresses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.People", "AddressId", "dbo.Addresses", "Id", cascadeDelete: true);
            DropColumn("dbo.People", "CityId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "CityId", c => c.Int(nullable: false));
            DropForeignKey("dbo.People", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.AppEvents", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Addresses", "CityId", "dbo.Cities");
            DropIndex("dbo.AppEvents", new[] { "AddressId" });
            DropIndex("dbo.People", new[] { "AddressId" });
            DropIndex("dbo.Addresses", new[] { "CityId" });
            DropColumn("dbo.AppEvents", "AddressId");
            DropColumn("dbo.People", "AddressId");
            DropTable("dbo.Addresses");
            CreateIndex("dbo.People", "CityId");
            AddForeignKey("dbo.People", "CityId", "dbo.Cities", "Id", cascadeDelete: true);
        }
    }
}
