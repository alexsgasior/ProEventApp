namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eventFk_to_eventPro : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EventProfessionals", "EventId", c => c.Int(nullable: false));
            CreateIndex("dbo.EventProfessionals", "EventId");
            AddForeignKey("dbo.EventProfessionals", "EventId", "dbo.Events", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventProfessionals", "EventId", "dbo.Events");
            DropIndex("dbo.EventProfessionals", new[] { "EventId" });
            DropColumn("dbo.EventProfessionals", "EventId");
        }
    }
}
