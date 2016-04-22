namespace AltasoftDaily.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEnforcementTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EnforcementLoans",
                c => new
                    {
                        EnforcementID = c.Int(nullable: false, identity: true),
                        IsActive = c.Boolean(nullable: false),
                        Status = c.Int(nullable: false),
                        CaseStatus = c.Int(nullable: false),
                        LoanID = c.Int(nullable: false),
                        BorrowerName = c.String(),
                        BorrowerPrivateNumber = c.String(),
                        BorrowerPhone = c.String(),
                        BorrowerAddress = c.String(),
                        GuarantorName = c.String(),
                        GuarantorPrivateNumber = c.String(),
                        GuarantorPhone = c.String(),
                        GuarantorAddress = c.String(),
                        ContactPerson = c.String(),
                        CaseNo = c.String(),
                        ID = c.String(),
                        NotificationRegistry = c.String(),
                        Comment = c.String(),
                        IncumbranceApplicationOrEnforcement = c.String(),
                        AgreementAndSummaryJudgementTerms = c.String(),
                        Branch = c.String(),
                        CreditExpert = c.String(),
                        LoanStartDate = c.DateTime(nullable: false),
                        ApplicationSubmitDate = c.DateTime(nullable: false),
                        GivePLD = c.DateTime(nullable: false),
                        LoanPrincipal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LoanInterest = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LoanPenalty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ApplicationCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IncumbranceCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InsuranceCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EnforcementCostTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalDebtInApplication = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.EnforcementID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EnforcementLoans");
        }
    }
}
