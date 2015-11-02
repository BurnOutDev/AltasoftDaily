using AltasoftDaily.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltasoftDaily.Domain
{
    public class DailyPaymentAndLoan
    {
        public DailyPayment DailyPayment { get; set; }
        public int ClientID { get; set; }
        public int DeptID { get; set; }
        public int OperatorID { get; set; }
    }
}
