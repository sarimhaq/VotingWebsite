namespace ProjectX.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserProfileId = c.Int(nullable: false),
                        Heading = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        DatePosted = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfiles", t => t.UserProfileId, cascadeDelete: true)
                .Index(t => t.UserProfileId);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserPosts", t => t.UserPostsId, cascadeDelete: true)
                .Index(t => t.UserPostsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserPosts", "UserProfileId", "dbo.UserProfiles");
            DropForeignKey("dbo.UserCorrections", "UserPostsId", "dbo.UserPosts");
            DropIndex("dbo.UserCorrections", new[] { "UserPostsId" });
            DropIndex("dbo.UserPosts", new[] { "UserProfileId" });
            DropTable("dbo.UserCorrections");
            DropTable("dbo.UserPosts");
        }
    }
}
