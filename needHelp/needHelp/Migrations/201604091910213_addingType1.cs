namespace needHelp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingType1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OrganizationModels", "userId", c => c.String(nullable: false));
            AlterColumn("dbo.VolunteerModels", "userId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VolunteerModels", "userId", c => c.Int(nullable: false));
            AlterColumn("dbo.OrganizationModels", "userId", c => c.Int(nullable: false));
        }
    }
}
