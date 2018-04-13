namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validation_added_to_State : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.States", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.States", "Name", c => c.String(nullable: false));
        }
    }
}
