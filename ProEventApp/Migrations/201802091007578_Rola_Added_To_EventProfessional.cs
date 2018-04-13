namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rola_Added_To_EventProfessional : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EventProfessionals", "Rola", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EventProfessionals", "Rola");
        }
    }
}
