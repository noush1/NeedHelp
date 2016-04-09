namespace needHelp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingType : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.ActivityModels", "typeId");
            AddForeignKey("dbo.ActivityModels", "typeId", "dbo.HelpTypeModels", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ActivityModels", "typeId", "dbo.HelpTypeModels");
            DropIndex("dbo.ActivityModels", new[] { "typeId" });
        }
    }
}
