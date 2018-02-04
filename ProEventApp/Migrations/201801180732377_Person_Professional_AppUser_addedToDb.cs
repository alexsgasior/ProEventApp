namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Person_Professional_AppUser_addedToDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Surname = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Pesel = c.Long(nullable: false),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AppUser",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Nickname = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Professional",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        CompanyName = c.String(nullable: false),
                        NIP = c.String(),
                        CompanyWWW = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Professional", "Id", "dbo.People");
            DropForeignKey("dbo.AppUser", "Id", "dbo.People");
            DropIndex("dbo.Professional", new[] { "Id" });
            DropIndex("dbo.AppUser", new[] { "Id" });
            DropTable("dbo.Professional");
            DropTable("dbo.AppUser");
            DropTable("dbo.People");
        }
    }
}
