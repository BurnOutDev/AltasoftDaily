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
        [DisplayName("აქტიური")]
        public bool IsActive { get; set; }

        [DisplayName("სტატუსი")]
        public EnforcementLoanStatus Status { get; set; }

        [DisplayName("საქმის სტატუსი")]
        public EnforcementCaseStatus CaseStatus { get; set; }

        [DisplayName("სესხის #")]
        public int LoanID { get; set; }

        [DisplayName("სესხის ხელშ. #")]
        public string LoanAgreementNumber { get; set; }

        [DisplayName("სახელი")]
        public string BorrowerName { get; set; }

        [DisplayName("პირადი #")]
        public string BorrowerPrivateNumber { get; set; }

        [DisplayName("ტელეფონი")]
        public string BorrowerPhone { get; set; }

        [DisplayName("მისამართი")]
        public string BorrowerAddress { get; set; }
        //[DisplayName("აქტიური")]
        //public string GuarantorName { get; set; }
        //[DisplayName("აქტიური")]
        //public string GuarantorPrivateNumber { get; set; }
        //[DisplayName("აქტიური")]
        //public string GuarantorPhone { get; set; }
        //[DisplayName("აქტიური")]
        //public string GuarantorAddress { get; set; }
        [DisplayName("კომენტარი")]
        public string Comment { get; set; }

        [DisplayName("საქმის #")]
        public string CaseNo { get; set; }

        [DisplayName("იდენტიფიკატორი")]
        public string Identificator { get; set; }

        [DisplayName("შეტყობინებების რეესტრი")]
        public string NotificationRegistry { get; set; }

        [DisplayName("საკონტ. პირი")]
        public string ContactPerson { get; set; }

        [DisplayName("ყადაღა სააპლიკაციო/აღსრულება")]
        public string IncumbranceApplicationOrEnforcement { get; set; }

        [DisplayName("მორიგების პირობები")]
        public virtual AgreementAndSummaryJudgementTerms AgreementAndSummaryJudgementTerms { get; set; }

        [DisplayName("ფილიალი")]
        public string Branch { get; set; }

        [DisplayName("ექსპერტი")]
        public string CreditExpert { get; set; }

        [DisplayName("გაცემის თარიღი")]
        public DateTime LoanStartDate { get; set; }

        [DisplayName("აპლიკაციის შეტანის თარიღი")]
        public DateTime ApplicationSubmitDate { get; set; }

        [DisplayName("PLD გადაცემის თარიღი")]
        public DateTime GivePLD { get; set; }

        [DisplayName("ძირი")]
        public decimal LoanPrincipal { get; set; }

        [DisplayName("პროცენტი")]
        public decimal LoanInterest { get; set; }

        [DisplayName("ჯარიმა")]
        public decimal LoanPenalty { get; set; }

        [DisplayName("აპლ. ძირი")]
        public decimal AppPrincipal { get; set; }

        [DisplayName("აპლ. პროცენტი")]
        public decimal AppInterest { get; set; }

        [DisplayName("აპლ. ჯარიმა")]
        public decimal AppPenalty { get; set; }

        [DisplayName("სულ დავალიანება")]
        public decimal TotalLoanDebt { get; set; }

        [DisplayName("აპლიკაციის საფასური")]
        public decimal ApplicationCost { get; set; }

        [DisplayName("ყადაღის საფასური")]
        public decimal IncumbranceCost { get; set; }

        [DisplayName("საგარანტიოს საფასური")]
        public decimal InsuranceCost { get; set; }

        [DisplayName("ჯამი")]
        public decimal LoanSum
        {
            get
            {
                return LoanPrincipal + LoanInterest + LoanPenalty + ApplicationCost + IncumbranceCost + InsuranceCost;
            }
        }

        //[DisplayName("აღსრულების ხარჯი სულ")]
        //public decimal EnforcementCostTotal
        //{
        //    get
        //    {
        //        return ApplicationCost + IncumbranceCost + InsuranceCost;
        //    }
        //}

        [DisplayName("პრობ. მენეჯერის #")]
        public int ProblemManagerID { get; set; }

        [DisplayName("პრობ. მენეჯერის სახელი")]
        public string ProblemManagerName { get; set; }

    }

    public enum EnforcementLoanStatus
    {
        [Description("დასრულებული")]
        Passive = 0,
        [Description("აქტიური")]
        Active = 1
    }

    public enum EnforcementCaseStatus
    {
        [Description("გამარტივებული წარმოება")]
        SummaryJudgement = 0,
        [Description("მორიგება")]
        Agreement = 1,
        [Description("იძულებითი აღსრულება")]
        EnforcementExecution = 2,
        [Description("განწილვადება")]
        EnforcementReschedule = 3,
        [Description("სასამართლო მიმდინარე")]
        CourtCurrent = 4,
        [Description("სასამართლო დასრულებული")]
        CourtFinished = 5,
        [Description("დასრულებული")]
        Finished = 6,
        [Description("ახალი")]
        NewCase = 7
    }
}