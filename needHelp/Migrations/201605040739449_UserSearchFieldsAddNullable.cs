namespace needHelp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserSearchFieldsAddNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserSearchDataModels", "cityId", "dbo.CityModels");
            DropForeignKey("dbo.UserSearchDataModels", "typeId", "dbo.HelpTypeModels");
            DropIndex("dbo.UserSearchDataModels", new[] { "cityId" });
            DropIndex("dbo.UserSearchDataModels", new[] { "typeId" });
            AddColumn("dbo.UserSearchDataModels", "startDate", c => c.DateTime());
            AddColumn("dbo.UserSearchDataModels", "endDate", c => c.DateTime());
            AlterColumn("dbo.UserSearchDataModels", "cityId", c => c.Int());
            AlterColumn("dbo.UserSearchDataModels", "organizationId", c => c.Int());
            AlterColumn("dbo.UserSearchDataModels", "typeId", c => c.Int());
            CreateIndex("dbo.UserSearchDataModels", "cityId");
            CreateIndex("dbo.UserSearchDataModels", "typeId");
            AddForeignKey("dbo.UserSearchDataModels", "cityId", "dbo.CityModels", "id");
            AddForeignKey("dbo.UserSearchDataModels", "typeId", "dbo.HelpTypeModels", "id");
            DropColumn("dbo.UserSearchDataModels", "date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserSearchDataModels", "date", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.UserSearchDataModels", "typeId", "dbo.HelpTypeModels");
            DropForeignKey("dbo.UserSearchDataModels", "cityId", "dbo.CityModels");
            DropIndex("dbo.UserSearchDataModels", new[] { "typeId" });
            DropIndex("dbo.UserSearchDataModels", new[] { "cityId" });
            AlterColumn("dbo.UserSearchDataModels", "typeId", c => c.Int(nullable: false));
            AlterColumn("dbo.UserSearchDataModels", "organizationId", c => c.Int(nullable: false));
            AlterColumn("dbo.UserSearchDataModels", "cityId", c => c.Int(nullable: false));
            DropColumn("dbo.UserSearchDataModels", "endDate");
            DropColumn("dbo.UserSearchDataModels", "startDate");
            CreateIndex("dbo.UserSearchDataModels", "typeId");
            CreateIndex("dbo.UserSearchDataModels", "cityId");
            AddForeignKey("dbo.UserSearchDataModels", "typeId", "dbo.HelpTypeModels", "id", cascadeDelete: true);
            AddForeignKey("dbo.UserSearchDataModels", "cityId", "dbo.CityModels", "id", cascadeDelete: true);
        }
    }
}
