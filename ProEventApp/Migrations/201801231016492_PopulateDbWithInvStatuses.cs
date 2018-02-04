namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDbWithInvStatuses : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO InvitationStatus(Name) VALUES ('Pending')");
            Sql("INSERT INTO InvitationStatus(Name) VALUES ('Accepted')");
            Sql("INSERT INTO InvitationStatus(Name) VALUES ('Decline')");
        }
        
        public override void Down()
        {
        }
    }
}
