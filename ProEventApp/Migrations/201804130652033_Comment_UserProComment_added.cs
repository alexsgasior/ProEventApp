namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Comment_UserProComment_added : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Comments", newName: "EventComments");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.EventComments", newName: "Comments");
        }
    }
}
