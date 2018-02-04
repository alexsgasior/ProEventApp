namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Person_Profiledrop : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.People", "ProfileId", "dbo.Profiles");
            DropIndex("dbo.People", new[] { "ProfileId" });
            DropColumn("dbo.People", "ProfileId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "ProfileId", c => c.Int(nullable: false));
            CreateIndex("dbo.People", "ProfileId");
            AddForeignKey("dbo.People", "ProfileId", "dbo.Profiles", "Id", cascadeDelete: true);
        }
    }
}
