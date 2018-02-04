namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Event_changeName_TO_AppEvent : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Events", newName: "AppEvents");
            DropForeignKey("dbo.EventImages", "EventId", "dbo.Events");
            DropForeignKey("dbo.EventProfessionals", "EventId", "dbo.Events");
            DropIndex("dbo.EventImages", new[] { "EventId" });
            DropIndex("dbo.EventProfessionals", new[] { "EventId" });
            AddColumn("dbo.EventImages", "AppEvent_Id", c => c.Int());
            AddColumn("dbo.EventProfessionals", "AppEvent_Id", c => c.Int());
            CreateIndex("dbo.EventImages", "AppEvent_Id");
            CreateIndex("dbo.EventProfessionals", "AppEvent_Id");
            AddForeignKey("dbo.EventImages", "AppEvent_Id", "dbo.AppEvents", "Id");
            AddForeignKey("dbo.EventProfessionals", "AppEvent_Id", "dbo.AppEvents", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventProfessionals", "AppEvent_Id", "dbo.AppEvents");
            DropForeignKey("dbo.EventImages", "AppEvent_Id", "dbo.AppEvents");
            DropIndex("dbo.EventProfessionals", new[] { "AppEvent_Id" });
            DropIndex("dbo.EventImages", new[] { "AppEvent_Id" });
            DropColumn("dbo.EventProfessionals", "AppEvent_Id");
            DropColumn("dbo.EventImages", "AppEvent_Id");
            CreateIndex("dbo.EventProfessionals", "EventId");
            CreateIndex("dbo.EventImages", "EventId");
            AddForeignKey("dbo.EventProfessionals", "EventId", "dbo.Events", "Id", cascadeDelete: true);
            AddForeignKey("dbo.EventImages", "EventId", "dbo.Events", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.AppEvents", newName: "Events");
        }
    }
}
