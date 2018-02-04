namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryAddedToAppEvent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppEvents", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.AppEvents", "CategoryId");
            AddForeignKey("dbo.AppEvents", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AppEvents", "CategoryId", "dbo.Categories");
            DropIndex("dbo.AppEvents", new[] { "CategoryId" });
            DropColumn("dbo.AppEvents", "CategoryId");
        }
    }
}
