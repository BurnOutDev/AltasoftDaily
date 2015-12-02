using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltasoftDaily.Domain
{
    public class CollateralModel
    {
        public int Code { get; set; }
        public int? CollateralID { get; set; }
        public string Name { get; set; }
        public decimal? Discount { get; set; }
        public string CCy { get; set; }
        public decimal? MarketPrice { get; set; }
        public decimal LiquidationAmount { get; set; }
        public string AgreementNumber { get; set; }
        public DateTime? CloseDate { get; set; }
    }
}
