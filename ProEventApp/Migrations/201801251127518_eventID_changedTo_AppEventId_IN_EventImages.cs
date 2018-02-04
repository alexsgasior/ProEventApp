namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eventID_changedTo_AppEventId_IN_EventImages : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EventImages", "AppEvent_Id", "dbo.AppEvents");
            DropIndex("dbo.EventImages", new[] { "AppEvent_Id" });
            RenameColumn(table: "dbo.EventImages", name: "AppEvent_Id", newName: "AppEventId");
            AlterColumn("dbo.EventImages", "AppEventId", c => c.Int(nullable: false));
            CreateIndex("dbo.EventImages", "AppEventId");
            AddForeignKey("dbo.EventImages", "AppEventId", "dbo.AppEvents", "Id", cascadeDelete: true);
            DropColumn("dbo.EventImages", "EventId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EventImages", "EventId", c => c.Int(nullable: false));
            DropForeignKey("dbo.EventImages", "AppEventId", "dbo.AppEvents");
            DropIndex("dbo.EventImages", new[] { "AppEventId" });
            AlterColumn("dbo.EventImages", "AppEventId", c => c.Int());
            RenameColumn(table: "dbo.EventImages", name: "AppEventId", newName: "AppEvent_Id");
            CreateIndex("dbo.EventImages", "AppEvent_Id");
            AddForeignKey("dbo.EventImages", "AppEvent_Id", "dbo.AppEvents", "Id");
        }
    }
}
