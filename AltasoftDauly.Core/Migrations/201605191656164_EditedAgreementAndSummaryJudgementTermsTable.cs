namespace AltasoftDaily.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditedAgreementAndSummaryJudgementTermsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AgreementAndSummaryJudgementTerms", "Payment", c => c.String());
            AddColumn("dbo.AgreementAndSummaryJudgementTerms", "PaymentDay", c => c.Int(nullable: false));
            DropColumn("dbo.AgreementAndSummaryJudgementTerms", "MonthlyPayment");
            DropColumn("dbo.AgreementAndSummaryJudgementTerms", "PaymentDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AgreementAndSummaryJudgementTerms", "PaymentDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AgreementAndSummaryJudgementTerms", "MonthlyPayment", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.AgreementAndSummaryJudgementTerms", "PaymentDay");
            DropColumn("dbo.AgreementAndSummaryJudgementTerms", "Payment");
        }
    }
}
