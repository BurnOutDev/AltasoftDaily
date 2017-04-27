namespace AltasoftDaily.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedScenario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DailyPayments", "ScenarioPrincipalInGel", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.DailyPayments", "ScenarioInterestInGel", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.DailyPayments", "ScenarioBalanceInGel", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DailyPayments", "ScenarioBalanceInGel");
            DropColumn("dbo.DailyPayments", "ScenarioInterestInGel");
            DropColumn("dbo.DailyPayments", "ScenarioPrincipalInGel");
        }
    }
}
