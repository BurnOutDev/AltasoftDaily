namespace AltasoftDaily.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserIDToDailyPayments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DailyPayments", "LocalUserID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DailyPayments", "LocalUserID");
        }
    }
}
