using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltasoftDaily.Domain
{
    class IncomeOrder
    {
        public string OrderID { get; set; }
        public string ClientName { get; set; }
        public string ClientId { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverId { get; set; }
        public string Description { get; set; }
        public string ExactDate
        {
            get
            {
                return Date.ToShortDateString() + " " + Date.ToShortTimeString();
            }
        }
        public string Amount { get; set; }
        public string Currency { get; set; }
        public string AmountInWords { get; set; }
        public string ResponsibleUser { get; set; }

        public DateTime Date { get; set; }
    }
}
