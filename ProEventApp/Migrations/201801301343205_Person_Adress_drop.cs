namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Person_Adress_drop : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.People", "AddressId", "dbo.Addresses");
            DropIndex("dbo.People", new[] { "AddressId" });
            DropColumn("dbo.People", "AddressId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "AddressId", c => c.Int(nullable: false));
            CreateIndex("dbo.People", "AddressId");
            AddForeignKey("dbo.People", "AddressId", "dbo.Addresses", "Id", cascadeDelete: true);
        }
    }
}
