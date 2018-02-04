namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CityFk_to_Person : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "CityId", c => c.Int(nullable: false));
            CreateIndex("dbo.People", "CityId");
            AddForeignKey("dbo.People", "CityId", "dbo.Cities", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.People", "CityId", "dbo.Cities");
            DropIndex("dbo.People", new[] { "CityId" });
            DropColumn("dbo.People", "CityId");
        }
    }
}
