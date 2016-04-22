using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltasoftDaily.Domain
{
    public class DailyPaymentIDOrderID
    {
        public long? OrderID { get; set; }
        public int PaymentID { get; set; }
        public string CoverLoanID { get; set; }
    }
}
