namespace AltasoftDaily.Domain
{
    public class DailyModel
    {
        public int ClientNo { get; set; }
        public int LoanID { get; set; }
        public string ClientName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalID { get; set; }
        public string BusinessAddress { get; set; }
        public string Phone { get; set; }
        public decimal NextScheduledPaymentInGel { get; set; }
        public decimal CurrentDebtInGel { get; set; }
        public decimal TotalDebtInGel { get; set; }
        public decimal Payment { get; set; }
        public string CalculationDate { get; set; }
        public string AgreementNumber { get; set; }
        public string LoanCCY { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string DateOfTheNotificationLetter { get; set; }
        public string ProblemManageDate { get; set; }
        public string ProblemManager { get; set; }
        public string DateOfEnforcement { get; set; }
        public decimal CourtAndEnforcementFee { get; set; }
        public string ClientAccountIban { get; set; }
        public string ClientAccountDescrip { get; set; }
        public string ClientAccountBranchCode { get; set; }
        public string ClientAddressFact { get; set; }
        public decimal InterestPenaltyInGel { get; set; }
        public decimal PrincipalPenaltyInGel { get; set; }
        public decimal OverdueInterestInGel { get; set; }
        public decimal AccruedInterestInGel { get; set; }
        public decimal OverduePrincipalInGel { get; set; }
        public decimal CurrentPrincipalInGel { get; set; }
        public decimal PrincipalInGel { get; set; }
        public decimal LoanAmountInGel { get; set; }
        public string ResponsibleUser { get; set; }
    }
}
