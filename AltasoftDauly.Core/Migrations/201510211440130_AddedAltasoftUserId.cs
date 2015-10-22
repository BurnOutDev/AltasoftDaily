namespace AltasoftDaily.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAltasoftUserId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "AltasoftUserID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "AltasoftUserID");
        }
    }
}
