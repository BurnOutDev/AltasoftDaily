using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltasoftDaily.Domain
{
    public class LoanDebts
    {
        public int LoanID { get; set; }
        public decimal Principal { get; set; }
        public decimal Interest { get; set; }
        public decimal Penalty { get; set; }
        public decimal Other { get; set; }


        public decimal GetSum()
        {
            return Principal + Interest + Penalty + Other;
        }
    }
}
