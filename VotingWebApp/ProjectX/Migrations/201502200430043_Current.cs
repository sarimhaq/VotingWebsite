namespace ProjectX.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Current : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserPosts", "UserProfileId", "dbo.UserProfiles");
            DropIndex("dbo.UserPosts", new[] { "UserProfileId" });
            AddColumn("dbo.UserPosts", "UserProfile_Id", c => c.Int());
            AlterColumn("dbo.UserPosts", "UserProfileId", c => c.String(nullable: false));
            CreateIndex("dbo.UserPosts", "UserProfile_Id");
            AddForeignKey("dbo.UserPosts", "UserProfile_Id", "dbo.UserProfiles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserPosts", "UserProfile_Id", "dbo.UserProfiles");
            DropIndex("dbo.UserPosts", new[] { "UserProfile_Id" });
            AlterColumn("dbo.UserPosts", "UserProfileId", c => c.Int(nullable: false));
            DropColumn("dbo.UserPosts", "UserProfile_Id");
            CreateIndex("dbo.UserPosts", "UserProfileId");
            AddForeignKey("dbo.UserPosts", "UserProfileId", "dbo.UserProfiles", "Id", cascadeDelete: true);
        }
    }
}
