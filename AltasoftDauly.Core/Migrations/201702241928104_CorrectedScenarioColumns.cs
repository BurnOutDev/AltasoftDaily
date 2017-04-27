namespace AltasoftDaily.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrectedScenarioColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DailyPayments", "ScenarioFixedPayment", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.DailyPayments", "ScenarioPayment", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.DailyPayments", "ScenarioWholeDebt", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.DailyPayments", "ScenarioPenalty", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.DailyPayments", "ScenarioPrincipalInGel");
            DropColumn("dbo.DailyPayments", "ScenarioBalanceInGel");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DailyPayments", "ScenarioBalanceInGel", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.DailyPayments", "ScenarioPrincipalInGel", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.DailyPayments", "ScenarioPenalty");
            DropColumn("dbo.DailyPayments", "ScenarioWholeDebt");
            DropColumn("dbo.DailyPayments", "ScenarioPayment");
            DropColumn("dbo.DailyPayments", "ScenarioFixedPayment");
        }
    }
}
