namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class profile_added_to_Person : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "ProfileId", c => c.Int(nullable: true));
            CreateIndex("dbo.People", "ProfileId");
            AddForeignKey("dbo.People", "ProfileId", "dbo.Profiles", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.People", "ProfileId", "dbo.Profiles");
            DropIndex("dbo.People", new[] { "ProfileId" });
            DropColumn("dbo.People", "ProfileId");
        }
    }
}
