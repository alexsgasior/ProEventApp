namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validation_added_to_City : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cities", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Cities", "PostalCode", c => c.String(nullable: false, maxLength: 6));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cities", "PostalCode", c => c.String(nullable: false));
            AlterColumn("dbo.Cities", "Name", c => c.String(nullable: false));
        }
    }
}
