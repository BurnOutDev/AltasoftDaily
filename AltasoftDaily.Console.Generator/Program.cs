using AltasoftAPI.CustomersAPI;
using AltasoftAPI.LoansAPI;
using AltasoftDaily.Core;
using AltasoftDaily.Domain;
using AltasoftDaily.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltasoftDaily.Console.Generator
{
    class Program
    {
        static void Main()
        {
            #region Initialize Services
            #region OrdersService
            AltasoftAPI.OrdersAPI.OrdersService o = new AltasoftAPI.OrdersAPI.OrdersService();
            o.RequestHeadersValue = new AltasoftAPI.OrdersAPI.RequestHeaders() { ApplicationKey = "BusinessCreditClient", RequestId = Guid.NewGuid().ToString() };
            #endregion

            #region CustomersService
            AltasoftAPI.CustomersAPI.CustomersService c = new AltasoftAPI.CustomersAPI.CustomersService();
            c.RequestHeadersValue = new AltasoftAPI.CustomersAPI.RequestHeaders() { ApplicationKey = "BusinessCreditClient", RequestId = Guid.NewGuid().ToString() };
            #endregion

            #region AccountsService
            AltasoftAPI.AccountsAPI.AccountsService a = new AltasoftAPI.AccountsAPI.AccountsService();
            a.RequestHeadersValue = new AltasoftAPI.AccountsAPI.RequestHeaders() { ApplicationKey = "BusinessCreditClient", RequestId = Guid.NewGuid().ToString() };
            #endregion

            #region LoansService
            AltasoftAPI.LoansAPI.LoansService l = new AltasoftAPI.LoansAPI.LoansService();
            l.RequestHeadersValue = new AltasoftAPI.LoansAPI.RequestHeaders() { ApplicationKey = "BusinessCreditClient", RequestId = Guid.NewGuid().ToString() };
            #endregion
            #endregion

            List<DailyPaymentAndLoan> data = new List<DailyPaymentAndLoan>();



            var loans = new HashSet<Loan>(l.ListLoans(new ListLoansQuery()
            {
                ControlFlags = LoanControlFlags.Basic | LoanControlFlags.Authorities | LoanControlFlags.Debts,
                Status = new LoanStatus[]
                {
                    LoanStatus.Overdue,
                    LoanStatus.Current,
                    LoanStatus.Late
                }
            }));

            foreach (var loan in loans)
            {
                var customer = c.GetCustomer( CustomerControlFlags.Basic, 
                                              true, 
                                              loan.BorrowerId.Value, 
                                              true );


            }











            return;

            (from x in l.ListLoans(new AltasoftAPI.LoansAPI.ListLoansQuery() { ControlFlags = AltasoftAPI.LoansAPI.LoanControlFlags.Basic, Status = new AltasoftAPI.LoansAPI.LoanStatus[] { AltasoftAPI.LoansAPI.LoanStatus.Overdue, AltasoftAPI.LoansAPI.LoanStatus.Current, AltasoftAPI.LoansAPI.LoanStatus.Late } })
             select x.Id.Value).ToList().OrderBy(x => x).Where(x => x != 2331 /*&& x != 6*/).ToList().ForEach(x =>

             data.Add(DailyManagement.GetLoanAndDailyModel(x)));

            using (var db = new AltasoftDailyContext())
            {
                foreach (var p in data)
                {

                }
            }
        }
    }
}
