namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Comment_UserProComment_added_1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        Unique = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserProComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentId = c.Int(nullable: false),
                        ProfessionalId = c.Int(nullable: false),
                        AppUserId = c.Int(nullable: false),
                        Who = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppUser", t => t.AppUserId)
                .ForeignKey("dbo.Comments", t => t.CommentId, cascadeDelete: true)
                .ForeignKey("dbo.Professional", t => t.ProfessionalId)
                .Index(t => t.CommentId)
                .Index(t => t.ProfessionalId)
                .Index(t => t.AppUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProComments", "ProfessionalId", "dbo.Professional");
            DropForeignKey("dbo.UserProComments", "CommentId", "dbo.Comments");
            DropForeignKey("dbo.UserProComments", "AppUserId", "dbo.AppUser");
            DropIndex("dbo.UserProComments", new[] { "AppUserId" });
            DropIndex("dbo.UserProComments", new[] { "ProfessionalId" });
            DropIndex("dbo.UserProComments", new[] { "CommentId" });
            DropTable("dbo.UserProComments");
            DropTable("dbo.Comments");
        }
    }
}
