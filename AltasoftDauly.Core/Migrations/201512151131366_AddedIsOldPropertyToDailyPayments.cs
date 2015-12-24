namespace AltasoftDaily.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsOldPropertyToDailyPayments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DailyPayments", "IsOld", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DailyPayments", "IsOld");
        }
    }
}
