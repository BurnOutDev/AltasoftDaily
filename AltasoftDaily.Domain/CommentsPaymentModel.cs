using AltasoftDaily.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltasoftDaily.Domain
{
    public class CommentsPaymentModel
    {
        public string კლიენტის_ნომერი { get; set; }
        public string სესხის_ნომერი { get; set; }
        public string სახელი { get; set; }
        public string გვარი { get; set; }
        public string პირადი_ნომერი { get; set; }
        public string ბიზნესის_ფაქტ_მის { get; set; }
        public string ტელ { get; set; }
        public string PMT { get; set; }
        public string მიმდინარე_დავალიანება { get; set; }
        public string სულ_განულება { get; set; }
        public string გადახდა { get; set; }
        public string თარიღი { get; set; }
        public string ხელშ_ნომერი { get; set; }
        public string ექსპერტი { get; set; }

        public static explicit operator CommentsPaymentModel(DailyPayment v)
        {
            return new CommentsPaymentModel()
            {
                ბიზნესის_ფაქტ_მის = v.BusinessAddress,
                თარიღი = v.CalculationDate.ToShortDateString(),
                სახელი = v.FirstName,
                კლიენტის_ნომერი = v.ClientNo.ToString(),
                მიმდინარე_დავალიანება = v.CurrentDebtInGel.ToString(),
                სესხის_ნომერი = v.LoanID.ToString(),
                PMT = v.NextScheduledPaymentInGel.ToString(),
                გადახდა = v.Payment.ToString(),
                პირადი_ნომერი = v.PersonalID,
                ტელ = v.Phone,
                სულ_განულება = v.TotalDebtInGel.ToString(),
                გვარი = v.LastName,
                ხელშ_ნომერი = v.AgreementNumber,
                ექსპერტი = v.ResponsibleUser
            };
        }
    }
}
