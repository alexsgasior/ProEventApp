namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validation_added_to_Person : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.People", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.People", "Surname", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.People", "Surname", c => c.String(nullable: false));
            AlterColumn("dbo.People", "Name", c => c.String(nullable: false));
        }
    }
}
