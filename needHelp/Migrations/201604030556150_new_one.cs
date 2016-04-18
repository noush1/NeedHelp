namespace needHelp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new_one : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ActivityModels", "cityId", "dbo.CityModels");
            DropForeignKey("dbo.ActivityModels", "org_id", "dbo.OrganizationModels");
            DropForeignKey("dbo.VolunteerModelsActivityModels", "VolunteerModels_id", "dbo.VolunteerModels");
            DropForeignKey("dbo.VolunteerModelsActivityModels", "ActivityModels_id", "dbo.ActivityModels");
            DropIndex("dbo.ActivityModels", new[] { "cityId" });
            DropIndex("dbo.ActivityModels", new[] { "org_id" });
            DropIndex("dbo.VolunteerModelsActivityModels", new[] { "VolunteerModels_id" });
            DropIndex("dbo.VolunteerModelsActivityModels", new[] { "ActivityModels_id" });
            DropColumn("dbo.ActivityModels", "date");
            DropColumn("dbo.ActivityModels", "description");
            DropColumn("dbo.ActivityModels", "org_id");
            DropTable("dbo.VolunteerModelsActivityModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.VolunteerModelsActivityModels",
                c => new
                    {
                        VolunteerModels_id = c.Int(nullable: false),
                        ActivityModels_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.VolunteerModels_id, t.ActivityModels_id });
            
            AddColumn("dbo.ActivityModels", "org_id", c => c.Int());
            AddColumn("dbo.ActivityModels", "description", c => c.String(nullable: false));
            AddColumn("dbo.ActivityModels", "date", c => c.DateTime(nullable: false));
            CreateIndex("dbo.VolunteerModelsActivityModels", "ActivityModels_id");
            CreateIndex("dbo.VolunteerModelsActivityModels", "VolunteerModels_id");
            CreateIndex("dbo.ActivityModels", "org_id");
            CreateIndex("dbo.ActivityModels", "cityId");
            AddForeignKey("dbo.VolunteerModelsActivityModels", "ActivityModels_id", "dbo.ActivityModels", "id", cascadeDelete: true);
            AddForeignKey("dbo.VolunteerModelsActivityModels", "VolunteerModels_id", "dbo.VolunteerModels", "id", cascadeDelete: true);
            AddForeignKey("dbo.ActivityModels", "org_id", "dbo.OrganizationModels", "id");
            AddForeignKey("dbo.ActivityModels", "cityId", "dbo.CityModels", "id", cascadeDelete: true);
        }
    }
}
