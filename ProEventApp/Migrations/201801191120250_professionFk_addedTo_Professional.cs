namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class professionFk_addedTo_Professional : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Professional", "Profession_Id", "dbo.Professions");
            DropIndex("dbo.Professional", new[] { "Profession_Id" });
            RenameColumn(table: "dbo.Professional", name: "Profession_Id", newName: "ProfessionId");
            AlterColumn("dbo.Professional", "ProfessionId", c => c.Int(nullable: false));
            CreateIndex("dbo.Professional", "ProfessionId");
            AddForeignKey("dbo.Professional", "ProfessionId", "dbo.Professions", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Professional", "ProfessionId", "dbo.Professions");
            DropIndex("dbo.Professional", new[] { "ProfessionId" });
            AlterColumn("dbo.Professional", "ProfessionId", c => c.Int());
            RenameColumn(table: "dbo.Professional", name: "ProfessionId", newName: "Profession_Id");
            CreateIndex("dbo.Professional", "Profession_Id");
            AddForeignKey("dbo.Professional", "Profession_Id", "dbo.Professions", "Id");
        }
    }
}
