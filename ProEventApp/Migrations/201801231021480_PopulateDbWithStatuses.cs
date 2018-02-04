namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDbWithStatuses : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Status(Name) VALUES ('Pending')");
            Sql("INSERT INTO Status(Name) VALUES ('Active')");
            Sql("INSERT INTO Status(Name) VALUES ('Done')");
        }
        
        public override void Down()
        {
        }
    }
}
