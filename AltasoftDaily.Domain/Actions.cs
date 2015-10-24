using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltasoftDaily.Domain
{
    public class Action
    {
        [Key]
        public int ActionID { get; set; }
        public string RequestID { get; set; }
        public decimal OrderID { get; set; }
        public string DocNum { get; set; }
        public int DeptID { get; set; }
        public DateTime Date { get; set; }
        public object ExternalData { get; set; }
        public ActionStatus Status { get; set; }

        public User User { get; set; }
    }

    public enum ActionStatus
    {
        Canceled,
        Completed,

    }
}
