namespace AltasoftDaily.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAgreementTermsToEnforcementLoan : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AgreementAndSummaryJudgementTerms",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AdmittedAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MonthlyPayment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaymentDate = c.DateTime(nullable: false),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.EnforcementLoans", "AgreementAndSummaryJudgementTerms_ID", c => c.Int());
            CreateIndex("dbo.EnforcementLoans", "AgreementAndSummaryJudgementTerms_ID");
            AddForeignKey("dbo.EnforcementLoans", "AgreementAndSummaryJudgementTerms_ID", "dbo.AgreementAndSummaryJudgementTerms", "ID");
            DropColumn("dbo.EnforcementLoans", "AgreementAndSummaryJudgementTerms");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EnforcementLoans", "AgreementAndSummaryJudgementTerms", c => c.String());
            DropForeignKey("dbo.EnforcementLoans", "AgreementAndSummaryJudgementTerms_ID", "dbo.AgreementAndSummaryJudgementTerms");
            DropIndex("dbo.EnforcementLoans", new[] { "AgreementAndSummaryJudgementTerms_ID" });
            DropColumn("dbo.EnforcementLoans", "AgreementAndSummaryJudgementTerms_ID");
            DropTable("dbo.AgreementAndSummaryJudgementTerms");
        }
    }
}
