namespace AltasoftDaily.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLoanAgreementNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EnforcementLoans", "LoanAgreementNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EnforcementLoans", "LoanAgreementNumber");
        }
    }
}
