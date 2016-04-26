namespace needHelp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _21_45 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActivityModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        organizationId = c.Int(nullable: false),
                        address = c.String(nullable: false),
                        cityId = c.Int(nullable: false),
                        typeActivity_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.HelpTypeModels", t => t.typeActivity_id, cascadeDelete: true)
                .Index(t => t.typeActivity_id);
            
            CreateTable(
                "dbo.HelpTypeModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        typeName = c.String(nullable: false),
                        imagePath = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.CityModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.OrganizationModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        userId = c.Int(nullable: false),
                        name = c.String(nullable: false),
                        contactName = c.String(nullable: false),
                        contactPhone = c.String(nullable: false),
                        email = c.String(),
                        website = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.TrustedUserModels",
                c => new
                    {
                        organizationId = c.Int(nullable: false),
                        volunteerId = c.Int(nullable: false),
                        OrganizationModels_id = c.Int(),
                    })
                .PrimaryKey(t => new { t.organizationId, t.volunteerId })
                .ForeignKey("dbo.OrganizationModels", t => t.OrganizationModels_id)
                .Index(t => t.OrganizationModels_id);
            
            CreateTable(
                "dbo.UserSearchDataModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        VolunteerId = c.Int(nullable: false),
                        cityId = c.Int(nullable: false),
                        date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.UserRequestModels",
                c => new
                    {
                        volunteerId = c.Int(nullable: false),
                        activityId = c.Int(nullable: false),
                        isAccepted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.volunteerId, t.activityId });
            
            CreateTable(
                "dbo.VolunteerModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        userId = c.Int(nullable: false),
                        firstName = c.String(nullable: false),
                        lastName = c.String(nullable: false),
                        email = c.String(nullable: false),
                        phone = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.AspNetUsers", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrustedUserModels", "OrganizationModels_id", "dbo.OrganizationModels");
            DropForeignKey("dbo.ActivityModels", "typeActivity_id", "dbo.HelpTypeModels");
            DropIndex("dbo.TrustedUserModels", new[] { "OrganizationModels_id" });
            DropIndex("dbo.ActivityModels", new[] { "typeActivity_id" });
            DropColumn("dbo.AspNetUsers", "Discriminator");
            DropTable("dbo.VolunteerModels");
            DropTable("dbo.UserRequestModels");
            DropTable("dbo.UserSearchDataModels");
            DropTable("dbo.TrustedUserModels");
            DropTable("dbo.OrganizationModels");
            DropTable("dbo.CityModels");
            DropTable("dbo.HelpTypeModels");
            DropTable("dbo.ActivityModels");
        }
    }
}
