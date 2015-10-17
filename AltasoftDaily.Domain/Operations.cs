using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltasoftDaily.Domain
{
    public class Transaction
    {
        [Key]
        public int TransactionID { get; set; }
        public string RequestID { get; set; }
        public int UserID { get; set; }
        public int DeptID { get; set; }
        public decimal Amount { get; set; }
        public decimal OrderID { get; set; }
        public DateTime Date { get; set; }
        public byte TransactionStatus { get; set; }
        public string TransactionCode { get; set; }
        public string OpCode { get; set; }
        public string Purpose { get; set; }
        public string CustomerAccountIBAN { get; set; }
        public byte CashOrderType { get; set; }
        public string DocNum { get; set; }

        public User User { get; set; }
    }
}
