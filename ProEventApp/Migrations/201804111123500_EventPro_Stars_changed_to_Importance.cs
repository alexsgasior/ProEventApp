namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventPro_Stars_changed_to_Importance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EventProfessionals", "Importance", c => c.Double(nullable: false));
            DropColumn("dbo.EventProfessionals", "Stars");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EventProfessionals", "Stars", c => c.Double(nullable: false));
            DropColumn("dbo.EventProfessionals", "Importance");
        }
    }
}
