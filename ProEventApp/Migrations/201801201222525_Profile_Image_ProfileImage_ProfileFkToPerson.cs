namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Profile_Image_ProfileImage_ProfileFkToPerson : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PreferedName = c.String(nullable: false),
                        Motto = c.String(nullable: false),
                        About = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProfileImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        ProfileId = c.Int(nullable: false),
                        ImageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Images", t => t.ImageId, cascadeDelete: true)
                .ForeignKey("dbo.Profiles", t => t.ProfileId, cascadeDelete: true)
                .Index(t => t.ProfileId)
                .Index(t => t.ImageId);
            
            AddColumn("dbo.People", "ProfileId", c => c.Int(nullable: false));
            CreateIndex("dbo.People", "ProfileId");
            AddForeignKey("dbo.People", "ProfileId", "dbo.Profiles", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProfileImages", "ProfileId", "dbo.Profiles");
            DropForeignKey("dbo.ProfileImages", "ImageId", "dbo.Images");
            DropForeignKey("dbo.People", "ProfileId", "dbo.Profiles");
            DropIndex("dbo.ProfileImages", new[] { "ImageId" });
            DropIndex("dbo.ProfileImages", new[] { "ProfileId" });
            DropIndex("dbo.People", new[] { "ProfileId" });
            DropColumn("dbo.People", "ProfileId");
            DropTable("dbo.ProfileImages");
            DropTable("dbo.Images");
            DropTable("dbo.Profiles");
        }
    }
}
