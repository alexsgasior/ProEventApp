namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eventComment_drop : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EventComments", "AppEventId", "dbo.AppEvents");
            DropForeignKey("dbo.EventComments", "CommentId", "dbo.Comments");
            DropIndex("dbo.EventComments", new[] { "AppEventId" });
            DropIndex("dbo.EventComments", new[] { "CommentId" });
            DropTable("dbo.EventComments");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EventComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AppEventId = c.Int(nullable: false),
                        CommentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.EventComments", "CommentId");
            CreateIndex("dbo.EventComments", "AppEventId");
            AddForeignKey("dbo.EventComments", "CommentId", "dbo.Comments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.EventComments", "AppEventId", "dbo.AppEvents", "Id", cascadeDelete: true);
        }
    }
}
