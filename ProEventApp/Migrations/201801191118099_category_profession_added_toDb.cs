namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class category_profession_added_toDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Professions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            AddColumn("dbo.Professional", "Profession_Id", c => c.Int());
            CreateIndex("dbo.Professional", "Profession_Id");
            AddForeignKey("dbo.Professional", "Profession_Id", "dbo.Professions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Professional", "Profession_Id", "dbo.Professions");
            DropForeignKey("dbo.Professions", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Professional", new[] { "Profession_Id" });
            DropIndex("dbo.Professions", new[] { "CategoryId" });
            DropColumn("dbo.Professional", "Profession_Id");
            DropTable("dbo.Professions");
            DropTable("dbo.Categories");
        }
    }
}
