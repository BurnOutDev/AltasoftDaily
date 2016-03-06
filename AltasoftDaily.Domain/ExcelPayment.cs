using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AltasoftDaily.Domain.POCO;

namespace AltasoftDaily.Domain
{
    public class ExcelPayment
    {
        public int N { get; set; }
        public int ClientNo { get; set; }
        public int LoanID { get; set; }
        public string ClientName { get; set; }
        public string PersonalID { get; set; }
        public string BusinessAddress { get; set; }
        public string Phone { get; set; }
        public decimal NextPayment { get; set; }
        public decimal CurrentDebt { get; set; }
        public decimal TotalDebt { get; set; }
        public decimal Payment { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public static explicit operator ExcelPayment(DailyPayment v)
        {
            return new ExcelPayment()
            {
                BusinessAddress = v.BusinessAddress,
                StartDate = DateTime.Parse(v.StartDate),
                ClientName = v.ClientName,
                ClientNo = v.ClientNo,
                CurrentDebt = v.CurrentDebtInGel,
                LoanID = v.LoanID,
                NextPayment = v.NextScheduledPaymentInGel,
                Payment = v.Payment,
                PersonalID = v.PersonalID,
                Phone = v.Phone,
                TotalDebt = v.TotalDebtInGel,
                EndDate = DateTime.Parse(v.EndDate)
            };
        }
    }
}
