namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppEventComment_OneToMany : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "AppEventId", c => c.Int(nullable: false));
            AddColumn("dbo.Comments", "DateCreated", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Comments", "AppEventId");
            AddForeignKey("dbo.Comments", "AppEventId", "dbo.AppEvents", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "AppEventId", "dbo.AppEvents");
            DropIndex("dbo.Comments", new[] { "AppEventId" });
            DropColumn("dbo.Comments", "DateCreated");
            DropColumn("dbo.Comments", "AppEventId");
        }
    }
}
