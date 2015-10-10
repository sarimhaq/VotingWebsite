namespace ProjectX.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sarim1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.VotingDatas", "UserProfileId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VotingDatas", "UserProfileId", c => c.String(nullable: false));
        }
    }
}
