namespace ProjectX.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class votingsys : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserCorrections", "UserPostsId", "dbo.UserPosts");
            DropForeignKey("dbo.UserPosts", "UserProfile_Id", "dbo.UserProfiles");
            DropIndex("dbo.UserCorrections", new[] { "UserPostsId" });
            DropIndex("dbo.UserPosts", new[] { "UserProfile_Id" });
            DropTable("dbo.UserCorrections");
            DropTable("dbo.UserPosts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserProfileId = c.String(nullable: false),
                        Heading = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        DatePosted = c.DateTime(nullable: false),
                        UserProfile_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserCorrections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserPostsId = c.Int(nullable: false),
                        Revision = c.String(),
                        Clarity = c.String(),
                        Grammar = c.String(),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.UserPosts", "UserProfile_Id");
            CreateIndex("dbo.UserCorrections", "UserPostsId");
            AddForeignKey("dbo.UserPosts", "UserProfile_Id", "dbo.UserProfiles", "Id");
            AddForeignKey("dbo.UserCorrections", "UserPostsId", "dbo.UserPosts", "Id", cascadeDelete: true);
        }
    }
}
