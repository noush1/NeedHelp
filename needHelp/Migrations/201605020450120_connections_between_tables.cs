namespace needHelp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class connections_between_tables : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.UserRequestModels", "volunteerId");
            CreateIndex("dbo.UserRequestModels", "activityId");
            AddForeignKey("dbo.UserRequestModels", "activityId", "dbo.ActivityModels", "id", cascadeDelete: true);
            AddForeignKey("dbo.UserRequestModels", "volunteerId", "dbo.VolunteerModels", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRequestModels", "volunteerId", "dbo.VolunteerModels");
            DropForeignKey("dbo.UserRequestModels", "activityId", "dbo.ActivityModels");
            DropIndex("dbo.UserRequestModels", new[] { "activityId" });
            DropIndex("dbo.UserRequestModels", new[] { "volunteerId" });
        }
    }
}
