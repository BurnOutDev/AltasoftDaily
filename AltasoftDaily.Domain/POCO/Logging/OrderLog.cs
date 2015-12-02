using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltasoftDaily.Domain.POCO.Logging
{
    public class OrderLog
    {
        [Key]
        public int OrderLogID { get; set; }
        public DateTime Date { get; set; }
        public User User { get; set; }

        public long OrderID { get; set; }
        public decimal Amount { get; set; }

        public DailyPayment LocalPayment { get; set; }
    }
}
