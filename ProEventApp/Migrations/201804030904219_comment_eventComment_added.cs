namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class comment_eventComment_added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        Who = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EventComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AppEventId = c.Int(nullable: false),
                        CommentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppEvents", t => t.AppEventId, cascadeDelete: true)
                .ForeignKey("dbo.Comments", t => t.CommentId, cascadeDelete: true)
                .Index(t => t.AppEventId)
                .Index(t => t.CommentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventComments", "CommentId", "dbo.Comments");
            DropForeignKey("dbo.EventComments", "AppEventId", "dbo.AppEvents");
            DropIndex("dbo.EventComments", new[] { "CommentId" });
            DropIndex("dbo.EventComments", new[] { "AppEventId" });
            DropTable("dbo.EventComments");
            DropTable("dbo.Comments");
        }
    }
}
