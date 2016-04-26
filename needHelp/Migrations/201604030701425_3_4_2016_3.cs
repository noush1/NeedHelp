namespace needHelp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3_4_2016_3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ActivityModels", "typeActivity_id", "dbo.HelpTypeModels");
            DropIndex("dbo.ActivityModels", new[] { "typeActivity_id" });
            AddColumn("dbo.ActivityModels", "typeId", c => c.Int(nullable: false));
            DropColumn("dbo.ActivityModels", "typeActivity_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ActivityModels", "typeActivity_id", c => c.Int(nullable: false));
            DropColumn("dbo.ActivityModels", "typeId");
            CreateIndex("dbo.ActivityModels", "typeActivity_id");
            AddForeignKey("dbo.ActivityModels", "typeActivity_id", "dbo.HelpTypeModels", "id", cascadeDelete: true);
        }
    }
}
