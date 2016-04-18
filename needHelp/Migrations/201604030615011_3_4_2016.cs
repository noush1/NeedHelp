namespace needHelp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3_4_2016 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VolunteerModelsActivityModels",
                c => new
                    {
                        VolunteerModels_id = c.Int(nullable: false),
                        ActivityModels_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.VolunteerModels_id, t.ActivityModels_id })
                .ForeignKey("dbo.VolunteerModels", t => t.VolunteerModels_id, cascadeDelete: true)
                .ForeignKey("dbo.ActivityModels", t => t.ActivityModels_id, cascadeDelete: true)
                .Index(t => t.VolunteerModels_id)
                .Index(t => t.ActivityModels_id);
            
            AddColumn("dbo.ActivityModels", "date", c => c.DateTime(nullable: false));
            AddColumn("dbo.ActivityModels", "description", c => c.String(nullable: false));
            AddColumn("dbo.ActivityModels", "org_id", c => c.Int());
            CreateIndex("dbo.ActivityModels", "cityId");
            CreateIndex("dbo.ActivityModels", "org_id");
            AddForeignKey("dbo.ActivityModels", "cityId", "dbo.CityModels", "id", cascadeDelete: true);
            AddForeignKey("dbo.ActivityModels", "org_id", "dbo.OrganizationModels", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VolunteerModelsActivityModels", "ActivityModels_id", "dbo.ActivityModels");
            DropForeignKey("dbo.VolunteerModelsActivityModels", "VolunteerModels_id", "dbo.VolunteerModels");
            DropForeignKey("dbo.ActivityModels", "org_id", "dbo.OrganizationModels");
            DropForeignKey("dbo.ActivityModels", "cityId", "dbo.CityModels");
            DropIndex("dbo.VolunteerModelsActivityModels", new[] { "ActivityModels_id" });
            DropIndex("dbo.VolunteerModelsActivityModels", new[] { "VolunteerModels_id" });
            DropIndex("dbo.ActivityModels", new[] { "org_id" });
            DropIndex("dbo.ActivityModels", new[] { "cityId" });
            DropColumn("dbo.ActivityModels", "org_id");
            DropColumn("dbo.ActivityModels", "description");
            DropColumn("dbo.ActivityModels", "date");
            DropTable("dbo.VolunteerModelsActivityModels");
        }
    }
}
