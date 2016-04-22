using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltasoftDaily.Domain.POCO
{
    public class EnforcementLoan
    {
        [Key]
        public int EnforcementID { get; set; }
        public bool IsActive { get; set; }
        public EnforcementLoanStatus Status { get; set; }
        public EnforcementCaseStatus CaseStatus { get; set; }
        public int LoanID { get; set; }
        public string LoanAgreementNumber { get; set; }
        public string BorrowerName { get; set; }
        public string BorrowerPrivateNumber { get; set; }
        public string BorrowerPhone { get; set; }
        public string BorrowerAddress { get; set; }
        public string GuarantorName { get; set; }
        public string GuarantorPrivateNumber { get; set; }
        public string GuarantorPhone { get; set; }
        public string GuarantorAddress { get; set; }
        public string ContactPerson { get; set; }
        public string CaseNo { get; set; }
        public string ID { get; set; }
        public string NotificationRegistry { get; set; }
        public string Comment { get; set; }

        [Description("ყადაღა სააპლიკაციოს შეტანისას/იძულებითი აღსრულებისას")]
        public string IncumbranceApplicationOrEnforcement { get; set; }
        public virtual AgreementAndSummaryJudgementTerms AgreementAndSummaryJudgementTerms { get; set; }
        public string Branch { get; set; }
        public string CreditExpert { get; set; }
        public DateTime LoanStartDate { get; set; }
        public DateTime ApplicationSubmitDate { get; set; }
        public DateTime GivePLD { get; set; }
        public decimal LoanPrincipal { get; set; }
        public decimal LoanInterest { get; set; }
        public decimal LoanPenalty { get; set; }
        public decimal ApplicationCost { get; set; }
        public decimal IncumbranceCost { get; set; }
        public decimal InsuranceCost { get; set; }
        public decimal EnforcementCostTotal { get; set; }
        public decimal TotalDebtInApplication { get; set; }

    }

    public enum EnforcementLoanStatus
    {
        Active = 1,
        Passive = 0
    }

    public enum EnforcementCaseStatus
    {
        [Description("გამარტივებული წარმოება")]
        SummaryJudgement,
        [Description("მორიგება")]
        Agreement,
        [Description("იძულებითი აღსრულება")]
        EnforcementExecution,
        [Description("განწილვადება")]
        EnforcementReschedule,
        [Description("სასამართლო მიმდინარე")]
        CourtCurrent,
        [Description("სასამართლო დასრულებული")]
        CourtFinished,
    }
}