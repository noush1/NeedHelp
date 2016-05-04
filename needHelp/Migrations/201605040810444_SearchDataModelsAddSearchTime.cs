namespace needHelp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SearchDataModelsAddSearchTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserSearchDataModels", "searchDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserSearchDataModels", "searchDate");
        }
    }
}
