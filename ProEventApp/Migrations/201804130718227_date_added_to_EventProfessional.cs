namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class date_added_to_EventProfessional : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EventProfessionals", "DateOfJob", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EventProfessionals", "DateOfJob");
        }
    }
}
