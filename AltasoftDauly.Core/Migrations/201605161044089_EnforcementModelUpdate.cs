namespace AltasoftDaily.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnforcementModelUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EnforcementLoans", "ProblemManagerID", c => c.Int(nullable: false));
            DropColumn("dbo.EnforcementLoans", "EnforcementCostTotal");
            DropColumn("dbo.EnforcementLoans", "TotalDebtInApplication");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EnforcementLoans", "TotalDebtInApplication", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.EnforcementLoans", "EnforcementCostTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.EnforcementLoans", "ProblemManagerID");
        }
    }
}
