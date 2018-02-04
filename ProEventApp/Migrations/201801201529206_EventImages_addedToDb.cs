namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventImages_addedToDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EventId = c.Int(nullable: false),
                        ImageId = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .ForeignKey("dbo.Images", t => t.ImageId, cascadeDelete: true)
                .Index(t => t.EventId)
                .Index(t => t.ImageId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventImages", "ImageId", "dbo.Images");
            DropForeignKey("dbo.EventImages", "EventId", "dbo.Events");
            DropIndex("dbo.EventImages", new[] { "ImageId" });
            DropIndex("dbo.EventImages", new[] { "EventId" });
            DropTable("dbo.EventImages");
        }
    }
}
