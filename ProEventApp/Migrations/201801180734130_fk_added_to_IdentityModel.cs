namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fk_added_to_IdentityModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ProfessionalId", c => c.Int());
            AddColumn("dbo.AspNetUsers", "AppUserId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "ProfessionalId");
            CreateIndex("dbo.AspNetUsers", "AppUserId");
            AddForeignKey("dbo.AspNetUsers", "AppUserId", "dbo.AppUser", "Id");
            AddForeignKey("dbo.AspNetUsers", "ProfessionalId", "dbo.Professional", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "ProfessionalId", "dbo.Professional");
            DropForeignKey("dbo.AspNetUsers", "AppUserId", "dbo.AppUser");
            DropIndex("dbo.AspNetUsers", new[] { "AppUserId" });
            DropIndex("dbo.AspNetUsers", new[] { "ProfessionalId" });
            DropColumn("dbo.AspNetUsers", "AppUserId");
            DropColumn("dbo.AspNetUsers", "ProfessionalId");
        }
    }
}
