namespace needHelp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteBullshit : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.BullshitModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BullshitModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
    }
}
