namespace AltasoftDaily.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamedDailyModelToDailyPaymentAddedToDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DailyPayments",
                c => new
                    {
                        DailyPaymentID = c.Int(nullable: false, identity: true),
                        ClientNo = c.Int(nullable: false),
                        LoanID = c.Int(nullable: false),
                        ClientName = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PersonalID = c.String(),
                        BusinessAddress = c.String(),
                        Phone = c.String(),
                        NextScheduledPaymentInGel = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrentDebtInGel = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalDebtInGel = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Payment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CalculationDate = c.String(),
                        AgreementNumber = c.String(),
                        LoanCCY = c.String(),
                        StartDate = c.String(),
                        EndDate = c.String(),
                        DateOfTheNotificationLetter = c.String(),
                        ProblemManageDate = c.String(),
                        ProblemManager = c.String(),
                        DateOfEnforcement = c.String(),
                        CourtAndEnforcementFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ClientAccountIban = c.String(),
                        ClientAccountDescrip = c.String(),
                        ClientAccountBranchCode = c.String(),
                        ClientAddressFact = c.String(),
                        InterestPenaltyInGel = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrincipalPenaltyInGel = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OverdueInterestInGel = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AccruedInterestInGel = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OverduePrincipalInGel = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrentPrincipalInGel = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrincipalInGel = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LoanAmountInGel = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ResponsibleUser = c.String(),
                    })
                .PrimaryKey(t => t.DailyPaymentID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DailyPayments");
        }
    }
}
