namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validation_added_to_Professional : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Professional", "CompanyName", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Professional", "NIP", c => c.String(nullable: false, maxLength: 13));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Professional", "NIP", c => c.String());
            AlterColumn("dbo.Professional", "CompanyName", c => c.String(nullable: false));
        }
    }
}
