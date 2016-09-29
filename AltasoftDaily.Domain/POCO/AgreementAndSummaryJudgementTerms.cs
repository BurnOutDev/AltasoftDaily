using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltasoftDaily.Domain.POCO
{
    public class AgreementAndSummaryJudgementTerms
    {
        [Key]
        public int ID { get; set; }
        public decimal AdmittedAmount { get; set; }
        public string Payment { get; set; }
        public int PaymentDay { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public override string ToString()
        {
            return string.Format("თანხა: {0}, თარიღი: {1}, გადახდა: {2}", AdmittedAmount.ToString(), Start.ToShortDateString(), Payment);
        }
    }
}
