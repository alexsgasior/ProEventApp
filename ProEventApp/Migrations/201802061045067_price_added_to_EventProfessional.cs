namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class price_added_to_EventProfessional : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EventProfessionals", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EventProfessionals", "Price");
        }
    }
}
