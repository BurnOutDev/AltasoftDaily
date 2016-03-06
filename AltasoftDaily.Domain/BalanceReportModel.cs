using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltasoftDaily.Domain
{
    public class BalanceReportModel
    {
        public DateTime Date { get; set; }
        public decimal Balance { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public long BalanceCode { get; set; }
        public int BranchID { get; set; }
    }
}
