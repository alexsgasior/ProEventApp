namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validation_added_to_Profession : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Professions", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Professions", "Description", c => c.String(nullable: false, maxLength: 800));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Professions", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Professions", "Name", c => c.String(nullable: false));
        }
    }
}
