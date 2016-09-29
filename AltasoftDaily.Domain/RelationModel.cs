using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltasoftDaily.Domain
{
    public class RelationModel
    {
        public RelationStatus Status { get; set; }
        public string AgreementNo { get; set; }
        public DateTime RelationDate { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public decimal LoanAmount { get; set; }
        public string LoanCurrency { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string LoanInterest { get; set; }
        public string Borrower { get; set; }
        public string Product { get; set; }
    }

    public enum RelationStatus
    {
        მიმდინარე,
        დახურული
    }
}
