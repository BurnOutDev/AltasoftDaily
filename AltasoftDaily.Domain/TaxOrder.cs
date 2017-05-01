using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltasoftDaily.Domain
{
    public class TaxOrder
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
                //return Date.ToShortDateString() + " " + Date.ToShortTimeString();
                return Date.ToString("dd  MMMM  yyyy", CultureInfo.GetCultureInfo("ka-ge"));
            }
        }
        public string Amount { get; set; }
        public string Currency { get; set; }
        public string AmountInWords { get; set; }
        public string ResponsibleUser { get; set; }

        public DateTime Date { get; set; }
    }
}
