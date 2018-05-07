namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Who_to_Comment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "Who", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "Who");
        }
    }
}
