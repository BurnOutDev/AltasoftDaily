using AltasoftDaily.Core;
using AltasoftDaily.Domain.POCO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BusinessCredit.Core;
using BusinessCredit.Domain;

namespace AltasoftDaily.UserInterface.WindowsForms
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

            //var accs = from x in a.ListAccounts(new AltasoftAPI.AccountsAPI.ListAccountsQuery()
            //   {
            //       ControlFlags = AltasoftAPI.AccountsAPI.AccountControlFlags.Basic
            //   }).ToList()
            //           select new LoanAccountNumber() { AccountNumber = x.IBAN, Description = x.Name.ValueGeo };

            //using (var db = new AltasoftDailyContext())
            //{
            //    foreach (var item in accs.Where(x => x.Description.Contains("დაფარვის")).ToList())
            //    {
            //        item.LoanID = int.Parse(item.Description.Substring(item.Description.LastIndexOf("-") + 1));
            //        db.AccountNumbers.Add(item);
            //    }
            //    db.SaveChanges();
            //}

            //using (var db = new AltasoftDailyContext())
            //{
            //    foreach (var item in db.DailyPayments.ToList())
            //    {
            //        if (string.IsNullOrWhiteSpace(item.ClientAccountIban))
            //        {
            //            item.IsOld = true;

            //            item.ClientAccountIban = db.AccountNumbers.FirstOrDefault(x => x.LoanID == item.LoanID).AccountNumber;
            //        }

            //        item.LoanCCY = "GEL";
            //    }

            //    db.SaveChanges();
            //}
            //using (var db = new AltasoftDailyContext())
            //{
            //    var user = db.Users.FirstOrDefault(x => x.Username == "atumaniani");
            //    var pmts = DailyManagement.GetDailyByBusinesscreditUser(user);
            //    var olds = db.DailyPayments.Where(x => x.CalculationDate == DateTime.Today && x.LocalUserID == user.UserID && x.IsOld).ToList();

            //    foreach (var old in olds)
            //    {
            //        var newp = pmts.FirstOrDefault(x => x.LoanID == old.LoanID);

            //        old.AccruedInterestInGel = newp.AccruedInterestInGel;
            //        old.AgreementNumber = newp.AgreementNumber;
            //        old.BusinessAddress = newp.BusinessAddress;
            //        old.CalculationDate = newp.CalculationDate;
            //        old.ClientAccountBranchCode = newp.ClientAccountBranchCode;
            //        old.ClientAccountDescrip = newp.ClientAccountDescrip;
            //        old.ClientAccountIban = newp.ClientAccountIban;
            //        old.ClientAddressFact = newp.ClientAddressFact;
            //        old.ClientName = newp.ClientName;
            //        old.ClientNo = newp.ClientNo;
            //        old.Comment = newp.Comment;
            //        old.CourtAndEnforcementFee = newp.CourtAndEnforcementFee;
            //        old.CurrentDebtInGel = newp.CurrentDebtInGel;
            //        old.CurrentPrincipalInGel = newp.CurrentPrincipalInGel;
            //        old.DateOfEnforcement = newp.DateOfEnforcement;
            //        old.DateOfTheNotificationLetter = newp.DateOfTheNotificationLetter;
            //        old.DeptID = newp.DeptID;
            //        old.EndDate = newp.EndDate;
            //        old.FirstName = newp.FirstName;
            //        old.InterestPenaltyInGel = newp.InterestPenaltyInGel;
            //        old.LastName = newp.LastName;
            //        old.LoanAmountInGel = newp.LoanAmountInGel;
            //        old.LoanCCY = newp.LoanCCY;
            //        old.LocalUserID = newp.LocalUserID;
            //        old.NextScheduledPaymentInGel = newp.NextScheduledPaymentInGel;
            //        old.OverdueInterestInGel = newp.OverdueInterestInGel;
            //        old.OverduePrincipalInGel = newp.OverduePrincipalInGel;
            //        old.PersonalID = newp.PersonalID;
            //        old.Phone = newp.Phone;
            //        old.PrincipalInGel = newp.PrincipalInGel;
            //        old.PrincipalPenaltyInGel = newp.PrincipalPenaltyInGel;
            //        old.ProblemManageDate = newp.ProblemManageDate;
            //        old.ProblemManager = newp.ProblemManager;
            //        old.ResponsibleUser = newp.ResponsibleUser;
            //        old.StartDate = newp.StartDate;
            //        old.TaxOrderNumber = newp.TaxOrderNumber;
            //        old.TotalDebtInGel = newp.TotalDebtInGel;
            //    }

            //    db.SaveChanges();
            //}

            #region Old Comments
            //using (var db = new AltasoftDailyContext())
            //{
            //var artura = db.Users.FirstOrDefault(x => x.Username == "atumaniani");
            //var filter = new Filter();
            //filter.Enabled = true;
            //filter.IsDeptFilterEnabled = true;
            //var deptIds = new List<int>();
            //deptIds.Add(2);
            //filter.DeptIDs = deptIds;
            //artura.Filter = filter;

            //artura.Filter.DeptIDs = new List<int>();
            //artura.Filter.FilterData = new List<Domain.FilterData>();
            //artura.Filter.FilterData.Add(new Domain.FilterData() { DeptID = 3 });

            //db.SaveChanges();
            //} 
            //}
            #endregion

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
