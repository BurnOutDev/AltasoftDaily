using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltasoftDaily.Domain.POCO
{
    public class LoanAccountNumber
    {
        [Key]
        public int ID { get; set; }
        public int LoanID { get; set; }
        public string AccountNumber { get; set; }
        public string Description { get; set; }
    }
}
