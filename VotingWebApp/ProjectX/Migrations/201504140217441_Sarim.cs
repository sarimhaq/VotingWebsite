namespace ProjectX.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sarim : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VotingDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Clarity = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        Delivery = c.String(nullable: false),
                        Comment = c.String(nullable: false),
                        UserProfileId = c.String(nullable: false),
                        CompanyName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.VotingDatas");
        }
    }
}
