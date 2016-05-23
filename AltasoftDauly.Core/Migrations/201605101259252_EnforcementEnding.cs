namespace AltasoftDaily.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnforcementEnding : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EnforcementLoans", "TotalLoanDebt", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.EnforcementLoans", "ProblemManagerName", c => c.String());
            DropColumn("dbo.EnforcementLoans", "ProblemManagerID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EnforcementLoans", "ProblemManagerID", c => c.Int(nullable: false));
            DropColumn("dbo.EnforcementLoans", "ProblemManagerName");
            DropColumn("dbo.EnforcementLoans", "TotalLoanDebt");
        }
    }
}
