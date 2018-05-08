namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ads_added_to_Models : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Advertisements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        AdText = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProAdvertisements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProfessionalId = c.Int(nullable: false),
                        AdvertisementId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Advertisements", t => t.AdvertisementId, cascadeDelete: true)
                .ForeignKey("dbo.Professional", t => t.ProfessionalId)
                .Index(t => t.ProfessionalId)
                .Index(t => t.AdvertisementId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProAdvertisements", "ProfessionalId", "dbo.Professional");
            DropForeignKey("dbo.ProAdvertisements", "AdvertisementId", "dbo.Advertisements");
            DropIndex("dbo.ProAdvertisements", new[] { "AdvertisementId" });
            DropIndex("dbo.ProAdvertisements", new[] { "ProfessionalId" });
            DropTable("dbo.ProAdvertisements");
            DropTable("dbo.Advertisements");
        }
    }
}
