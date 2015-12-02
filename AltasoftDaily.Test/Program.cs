using System;
using System.Windows.Forms;

namespace AltasoftDaily.Test
{
    static class Program
    {
        [STAThread]
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

            var loan = l.GetLoan(AltasoftAPI.LoansAPI.LoanControlFlags.Basic, true, 43, true);

            var list = l.ListCollaterals(new AltasoftAPI.LoansAPI.ListCollateralsQuery()
            {
                ControlFlags = AltasoftAPI.LoansAPI.CollateralControlFlags.Basic | AltasoftAPI.LoansAPI.CollateralControlFlags.Attributes
            });

            var list2 = l.ListLinkedCollaterals(new AltasoftAPI.LoansAPI.ListLinkedCollateralsQuery()
            {
                ControlFlags = AltasoftAPI.LoansAPI.LinkedCollateralControlFlags.Basic | AltasoftAPI.LoansAPI.LinkedCollateralControlFlags.Attributes,
                ApplicationId = 571,
                ApplicationIdSpecified = true
            });

            var t = l.ListLinkedCollaterals(new AltasoftAPI.LoansAPI.ListLinkedCollateralsQuery()
                {
                    ControlFlags = AltasoftAPI.LoansAPI.LinkedCollateralControlFlags.Basic | AltasoftAPI.LoansAPI.LinkedCollateralControlFlags.Attributes,
                    ApplicationId = loan.Id,
                    ApplicationIdSpecified = true
                });

            var tt = l.ListCollaterals(new AltasoftAPI.LoansAPI.ListCollateralsQuery()
            {
                ControlFlags = AltasoftAPI.LoansAPI.CollateralControlFlags.Basic | AltasoftAPI.LoansAPI.CollateralControlFlags.Attributes,
                Id = t[0].CollateralId,
                IdSpecified = true
            });

            //c.GetCustomer(AltasoftAPI.CustomersAPI.CustomerControlFlags.Basic | AltasoftAPI.CustomersAPI.CustomerControlFlags.);
        }
    }
}
