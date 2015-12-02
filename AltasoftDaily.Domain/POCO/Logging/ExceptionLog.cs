using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltasoftDaily.Domain.POCO.Logging
{
    public class ExceptionLog
    {
        [Key]
        public int ExceptionLogID { get; set; }
        public DateTime Date { get; set; }
        public User User { get; set; }

        public bool IsInner { get; set; }

        public string Message { get; set; }
        public string Source { get; set; }
        public string StackTrace { get; set; }
        public ExceptionLog InnerException { get; set; }
    }
}
