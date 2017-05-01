namespace AltasoftDaily.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPLU : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DailyPayments", "PLU", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DailyPayments", "PLU");
        }
    }
}
