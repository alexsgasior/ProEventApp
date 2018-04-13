namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventProfessional_Fk_added_to_AppEvent : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EventProfessionals", "AppEvent_Id", "dbo.AppEvents");
            DropIndex("dbo.EventProfessionals", new[] { "AppEvent_Id" });
            RenameColumn(table: "dbo.EventProfessionals", name: "AppEvent_Id", newName: "AppEventId");
            AlterColumn("dbo.EventProfessionals", "AppEventId", c => c.Int(nullable: false));
            CreateIndex("dbo.EventProfessionals", "AppEventId");
            AddForeignKey("dbo.EventProfessionals", "AppEventId", "dbo.AppEvents", "Id", cascadeDelete: true);
            DropColumn("dbo.EventProfessionals", "EventId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EventProfessionals", "EventId", c => c.Int(nullable: false));
            DropForeignKey("dbo.EventProfessionals", "AppEventId", "dbo.AppEvents");
            DropIndex("dbo.EventProfessionals", new[] { "AppEventId" });
            AlterColumn("dbo.EventProfessionals", "AppEventId", c => c.Int());
            RenameColumn(table: "dbo.EventProfessionals", name: "AppEventId", newName: "AppEvent_Id");
            CreateIndex("dbo.EventProfessionals", "AppEvent_Id");
            AddForeignKey("dbo.EventProfessionals", "AppEvent_Id", "dbo.AppEvents", "Id");
        }
    }
}
