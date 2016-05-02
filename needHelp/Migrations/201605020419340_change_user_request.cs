namespace needHelp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_user_request : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserRequestModels", "isAnswered", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserRequestModels", "replyMessage", c => c.String());
            AddColumn("dbo.UserRequestModels", "isDeletedByUser", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserRequestModels", "isDeletedByOrganization", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserRequestModels", "isDeletedByOrganization");
            DropColumn("dbo.UserRequestModels", "isDeletedByUser");
            DropColumn("dbo.UserRequestModels", "replyMessage");
            DropColumn("dbo.UserRequestModels", "isAnswered");
        }
    }
}
