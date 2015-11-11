using AltasoftDaily.Domain.POCO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltasoftDaily.Domain.POCO
{
    public class CommentLog
    {
        [Key]
        public int CommentLogID { get; set; }
        public DateTime Date { get; set; }
        public User User { get; set; }

        public string CommentValue { get; set; }

        public DailyPayment LocalPayment { get; set; }
    }
}
