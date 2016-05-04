namespace needHelp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserSearchFieldsAdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserSearchDataModels", "organizationId", c => c.Int(nullable: false));
            AddColumn("dbo.UserSearchDataModels", "typeId", c => c.Int(nullable: false));
            AddColumn("dbo.UserSearchDataModels", "org_id", c => c.Int());
            CreateIndex("dbo.UserSearchDataModels", "VolunteerId");
            CreateIndex("dbo.UserSearchDataModels", "cityId");
            CreateIndex("dbo.UserSearchDataModels", "typeId");
            CreateIndex("dbo.UserSearchDataModels", "org_id");
            AddForeignKey("dbo.UserSearchDataModels", "cityId", "dbo.CityModels", "id", cascadeDelete: true);
            AddForeignKey("dbo.UserSearchDataModels", "org_id", "dbo.OrganizationModels", "id");
            AddForeignKey("dbo.UserSearchDataModels", "typeId", "dbo.HelpTypeModels", "id", cascadeDelete: true);
            AddForeignKey("dbo.UserSearchDataModels", "VolunteerId", "dbo.VolunteerModels", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSearchDataModels", "VolunteerId", "dbo.VolunteerModels");
            DropForeignKey("dbo.UserSearchDataModels", "typeId", "dbo.HelpTypeModels");
            DropForeignKey("dbo.UserSearchDataModels", "org_id", "dbo.OrganizationModels");
            DropForeignKey("dbo.UserSearchDataModels", "cityId", "dbo.CityModels");
            DropIndex("dbo.UserSearchDataModels", new[] { "org_id" });
            DropIndex("dbo.UserSearchDataModels", new[] { "typeId" });
            DropIndex("dbo.UserSearchDataModels", new[] { "cityId" });
            DropIndex("dbo.UserSearchDataModels", new[] { "VolunteerId" });
            DropColumn("dbo.UserSearchDataModels", "org_id");
            DropColumn("dbo.UserSearchDataModels", "typeId");
            DropColumn("dbo.UserSearchDataModels", "organizationId");
        }
    }
}
