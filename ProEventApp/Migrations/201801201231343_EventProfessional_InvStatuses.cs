namespace ProEventApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventProfessional_InvStatuses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventProfessionals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        Importance = c.Double(nullable: false),
                        ProfessionalId = c.Int(nullable: false),
                        InvitationStatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.InvitationStatus", t => t.InvitationStatusId, cascadeDelete: true)
                .ForeignKey("dbo.Professional", t => t.ProfessionalId)
                .Index(t => t.ProfessionalId)
                .Index(t => t.InvitationStatusId);
            
            CreateTable(
                "dbo.InvitationStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventProfessionals", "ProfessionalId", "dbo.Professional");
            DropForeignKey("dbo.EventProfessionals", "InvitationStatusId", "dbo.InvitationStatus");
            DropIndex("dbo.EventProfessionals", new[] { "InvitationStatusId" });
            DropIndex("dbo.EventProfessionals", new[] { "ProfessionalId" });
            DropTable("dbo.InvitationStatus");
            DropTable("dbo.EventProfessionals");
        }
    }
}
