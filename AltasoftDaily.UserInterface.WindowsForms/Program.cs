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
using AltasoftAPI;

namespace AltasoftDaily.UserInterface.WindowsForms
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            //DailyManagement.SubmitOrder(1133666, "GEL", DateTime.Today, "GE32AL0100000314450001", 29, "1.0000314.009", "09", 21, 1);
            //var scenario = ScenarioService.GetScenarioByLoan(13566).FirstOrDefault(x => x.date == DateTime.Today);
            //var scenariofull = ScenarioService.GetScenarioByLoan(13566);
            //var scenarioPW = ScenarioService.GetScenarioPreviewByLoan(13566);
            //var spwToday = scenarioPW.FirstOrDefault(x => x.date == DateTime.Today);

            //var ScenarioBalanceInGel = (decimal)spwToday.balance;      //ნაშთი
            //var ScenarioInterestInGel = (decimal)spwToday.interest;    //პროცენტი
            //var ScenarioPrincipalInGel = (decimal)spwToday.principal;  //ძირი
            //var ScenarioPenalty = (decimal)spwToday.penalty;           //ჯარიმა
            //var ScenarioFixedPayment = (decimal)scenario.interest + (decimal)scenario.penalty + (decimal)scenario.principal;
            //var ScenarioFixedPayment2 = (decimal)spwToday.interest + (decimal)spwToday.penalty + (decimal)spwToday.principal;
            //var sum = ScenarioFixedPayment2 + ScenarioBalanceInGel;
            //var prsum = ScenarioFixedPayment + (decimal)scenario.balance;

            //var chamorchena = sum - prsum; //

            //var paymentFixed = ScenarioFixedPayment;//

            //var grafikitGanuleba = prsum;

            //var realGanuleba = sum;

            //Console.Read();


            //var payments = new AltasoftDailyContext().DailyPayments.Where( x => x.CalculationDate == DateTime.Today && x.Payment > 0 && x.OrderID != null).ToList();
            //var results = new List<string>();

            //foreach (var pay in payments)
            //    results.Add(DailyManagement.CoverLoan(pay.LoanID, pay.Payment));

            //TaxOrderGenerator.ExportToExcel(new AltasoftDaily.Helpers.SortableBindingList<Domain.ExcelPayment>((from x in results select new Domain.ExcelPayment() { ClientName = x }).ToList()));

            //return;
            //using (var db = new AltasoftDaily.Core.AltasoftDailyContext())
            //{
            //    var users = db.Users.Where(x => x.Username == "shkheladze" ||
            //                                    x.Username == "skandelaki" ||
            //                                    x.Username == "belisashvili" ||
            //                                    x.Username == "dsakhokia" ||
            //                                    x.Username == "vmamulia" ||
            //                                    x.Username == "iiremashvili" ||
            //                                    x.Username == "lchachanidze" ||
            //                                    x.Username == "dtsiteladze" ||
            //                                    x.Username == "gshaverdashvili" ||
            //                                    x.Username == "gkoridze").ToList();

            //    foreach (var user in users)
            //    {
            //        DailyManagement.GetUpdatesByAltasoftUser(user);
            //    }

            //    MessageBox.Show("Finished");
            //}

            //return;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        private static string CoverLoan(int loanid, decimal amount)
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

            var loan = l.GetLoan(AltasoftAPI.LoansAPI.LoanControlFlags.Basic | AltasoftAPI.LoansAPI.LoanControlFlags.Authorities | AltasoftAPI.LoansAPI.LoanControlFlags.Debts, true, loanid, true);
            var money = amount;
            var priorities = "fee,overdue_interest_penalty,overdue_nu_interest_penalty,overdue_principal_penalty,overdue_interest,overdue_principal_interest,overdue_nu_interest,late_interest,late_nu_interest,interest,nu_interest,overdue_principal,late_principal,principal,writeoff_penalty,writeoff_interest,writeoff_nu_interest,writeoff_principal,undue_principal".Split(',');
            var substractedDebts = new List<Tuple<string, string, decimal>>();
            var loanAmounts = new List<AltasoftAPI.LoansAPI.NameAmountCollectionItem>();
            var user = l.ListUsers(new AltasoftAPI.LoansAPI.ListUsersQuery() { Id = 12, IdSpecified = true });
            string opid;
            var customer = c.GetCustomer(AltasoftAPI.CustomersAPI.CustomerControlFlags.Basic | AltasoftAPI.CustomersAPI.CustomerControlFlags.IdentityDocuments, true, loan.BorrowerId.Value, true);

            #region Fill Substracted Debts

            foreach (var debt in loan.Debts)
            {
                if (debt.Name.Contains('#'))
                    substractedDebts.Add(new Tuple<string, string, decimal>(debt.Name.Substring(0, debt.Name.IndexOf('#')), debt.Name, debt.Amount));
                else
                    substractedDebts.Add(new Tuple<string, string, decimal>(debt.Name, debt.Name, debt.Amount));
            }
            #endregion

            #region Fill Loan Amounts
            for (int i = 0; i < priorities.Length; i++)
            {
                if (money == 0) break;

                var ddd = substractedDebts.Where(x => x.Item1 == priorities[i]);

                if (priorities[i] == "fee")
                    ddd = substractedDebts.Where(x => x.Item1.Contains(priorities[i]));

                if (ddd.Count() == 0) continue;

                foreach (var r in ddd)
                {
                    if (r.Item3 <= money)
                    {
                        loanAmounts.Add(new AltasoftAPI.LoansAPI.NameAmountCollectionItem()
                        {
                            Name = r.Item2,
                            Amount = r.Item3
                        });

                        money -= r.Item3;
                    }
                    else
                    {
                        loanAmounts.Add(new AltasoftAPI.LoansAPI.NameAmountCollectionItem()
                        {
                            Name = r.Item2,
                            Amount = money
                        });

                        money -= money;
                    }
                }
            }
            #endregion

            var b = l.LoanPayment(new int[1] { 1 },
                new AltasoftAPI.LoansAPI.UserAndDeptId() { DeptId = 1, DeptIdSpecified = true, UserIdentification = new AltasoftAPI.LoansAPI.UserIdentification() { Id = 12, IdSpecified = true, Name = "ნინო საჩალელი" } },
                loanAmounts.ToArray(), "jahsdahdkjas",
                loan.Id.Value,
                true,
                new AltasoftAPI.LoansAPI.PayerDetails { Client = customer.Name.ValueGeo },
                loan.AccountIdentifier,
                loan.AccountIdentifier.HasValue,
                AltasoftAPI.LoansAPI.PaymentType.Payment,
                true, AltasoftAPI.LoansAPI.PrepaymentRescheduleStrategy.ByPMT,
                false, AltasoftAPI.LoansAPI.PaymentSource.ClientResource,
                true, true, true, loan.Version.Value, loan.VersionSpecified, out opid);

            var etag = l.GetLoanOperationDetails(ref opid);

            l.ExecuteOperationAction(AltasoftAPI.LoansAPI.LoanOperationAction.IncrementAuthorization, true, "ავტორიზაცია", ref etag, ref opid, new AltasoftAPI.LoansAPI.UserAndDeptId() { DeptId = user.FirstOrDefault().DeptId, DeptIdSpecified = true, UserIdentification = new AltasoftAPI.LoansAPI.UserIdentification() { Id = user.FirstOrDefault().Id, IdSpecified = true, Name = user.FirstOrDefault().DisplayName } });

            return opid;
        }

        private static string CoverLoan2(int loanid, decimal amount)
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

            var loan = l.GetLoan(AltasoftAPI.LoansAPI.LoanControlFlags.Basic | AltasoftAPI.LoansAPI.LoanControlFlags.Authorities | AltasoftAPI.LoansAPI.LoanControlFlags.Debts, true, loanid, true);
            //var money = amount;
            //var priorities = "fee,overdue_interest_penalty,overdue_nu_interest_penalty,overdue_principal_penalty,overdue_interest,overdue_principal_interest,overdue_nu_interest,late_interest,late_nu_interest,interest,nu_interest,overdue_principal,late_principal,principal,writeoff_penalty,writeoff_interest,writeoff_nu_interest,writeoff_principal,undue_principal".Split(',');
            //var substractedDebts = new List<Tuple<string, string, decimal>>();
            //var loanAmounts = new List<AltasoftAPI.LoansAPI.NameAmountCollectionItem>();
            var user = l.ListUsers(new AltasoftAPI.LoansAPI.ListUsersQuery() { Id = 12, IdSpecified = true });
            string opid;
            var customer = c.GetCustomer(AltasoftAPI.CustomersAPI.CustomerControlFlags.Basic | AltasoftAPI.CustomersAPI.CustomerControlFlags.IdentityDocuments, true, loan.BorrowerId.Value, true);
            string b = "";
            string etag = "";

            //#region Fill Substracted Debts

            //foreach (var debt in loan.Debts)
            //{
            //    if (debt.Name.Contains('#'))
            //        substractedDebts.Add(new Tuple<string, string, decimal>(debt.Name.Substring(0, debt.Name.IndexOf('#')), debt.Name, debt.Amount));
            //    else
            //        substractedDebts.Add(new Tuple<string, string, decimal>(debt.Name, debt.Name, debt.Amount));
            //}
            //#endregion

            //#region Fill Loan Amounts
            //for (int i = 0; i < priorities.Length; i++)
            //{
            //    if (money == 0) break;

            //    var ddd = substractedDebts.Where(x => x.Item1 == priorities[i]);

            //    if (priorities[i] == "fee")
            //        ddd = substractedDebts.Where(x => x.Item1.Contains(priorities[i]));

            //    if (ddd.Count() == 0) continue;

            //    foreach (var r in ddd)
            //    {
            //        if (r.Item3 <= money)
            //        {
            //            loanAmounts.Add(new AltasoftAPI.LoansAPI.NameAmountCollectionItem()
            //            {
            //                Name = r.Item2,
            //                Amount = r.Item3
            //            });

            //            money -= r.Item3;
            //        }
            //        else
            //        {
            //            loanAmounts.Add(new AltasoftAPI.LoansAPI.NameAmountCollectionItem()
            //            {
            //                Name = r.Item2,
            //                Amount = money
            //            });

            //            money -= money;
            //        }
            //    }
            //}
            //#endregion

            var first = loan.Debts.Except(loan.Debts.Where(x => x.Name.Contains("undue_principal"))).Sum(x => x.Amount);
            var second = loan.Debts.Sum(x => x.Amount);
            var third = loan.Debts.Sum(x => x.Amount) <= amount;

            if (loan.Debts.Except(loan.Debts.Where(x => x.Name.Contains("undue_principal"))).Sum(x => x.Amount) >= amount)
            {
                b = l.LoanPayment(new int[0],
                        new AltasoftAPI.LoansAPI.UserAndDeptId() { DeptId = 1, DeptIdSpecified = true, UserIdentification = new AltasoftAPI.LoansAPI.UserIdentification() { Id = 12, IdSpecified = true, Name = "ნინო საჩალელი" } },
                        new AltasoftAPI.LoansAPI.NameAmountCollectionItem[1] { new AltasoftAPI.LoansAPI.NameAmountCollectionItem() { Name = "payment", Amount = amount } }, "დავალიანების დაფარვა",
                        loan.Id.Value,
                        true,
                        new AltasoftAPI.LoansAPI.PayerDetails { Client = customer.Name.ValueGeo },
                        loan.AccountIdentifier,
                        loan.AccountIdentifier.HasValue,
                        AltasoftAPI.LoansAPI.PaymentType.Payment,
                        false, AltasoftAPI.LoansAPI.PrepaymentRescheduleStrategy.ByPMT,
                        true, AltasoftAPI.LoansAPI.PaymentSource.ClientResource,
                        true, true, true, loan.Version.Value, loan.VersionSpecified, out opid);
                etag = l.GetLoanOperationDetails(ref opid);
                l.ExecuteOperationAction(AltasoftAPI.LoansAPI.LoanOperationAction.IncrementAuthorization, true, "ავტორიზაცია", ref etag, ref opid, new AltasoftAPI.LoansAPI.UserAndDeptId() { DeptId = user.FirstOrDefault().DeptId, DeptIdSpecified = true, UserIdentification = new AltasoftAPI.LoansAPI.UserIdentification() { Id = user.FirstOrDefault().Id, IdSpecified = true, Name = user.FirstOrDefault().DisplayName } });
                return opid;
            }
            else if (loan.Debts.Sum(x => x.Amount) > amount)
            {
                b = l.LoanPayment(new int[0],
                       new AltasoftAPI.LoansAPI.UserAndDeptId() { DeptId = 1, DeptIdSpecified = true, UserIdentification = new AltasoftAPI.LoansAPI.UserIdentification() { Id = 12, IdSpecified = true, Name = "ნინო საჩალელი" } },
                       new AltasoftAPI.LoansAPI.NameAmountCollectionItem[1] { new AltasoftAPI.LoansAPI.NameAmountCollectionItem() { Name = "payment", Amount = amount } }, "დავალიანების დაფარვა",
                       loan.Id.Value,
                       true,
                       new AltasoftAPI.LoansAPI.PayerDetails { Client = customer.Name.ValueGeo },
                       loan.AccountIdentifier,
                       loan.AccountIdentifier.HasValue,
                       AltasoftAPI.LoansAPI.PaymentType.Prepayment,
                       true, AltasoftAPI.LoansAPI.PrepaymentRescheduleStrategy.ByPMT,
                       true, AltasoftAPI.LoansAPI.PaymentSource.ClientResource,
                       true, true, true, loan.Version.Value, loan.VersionSpecified, out opid);
                etag = l.GetLoanOperationDetails(ref opid);
                l.ExecuteOperationAction(AltasoftAPI.LoansAPI.LoanOperationAction.IncrementAuthorization, true, "ავტორიზაცია", ref etag, ref opid, new AltasoftAPI.LoansAPI.UserAndDeptId() { DeptId = user.FirstOrDefault().DeptId, DeptIdSpecified = true, UserIdentification = new AltasoftAPI.LoansAPI.UserIdentification() { Id = user.FirstOrDefault().Id, IdSpecified = true, Name = user.FirstOrDefault().DisplayName } });
                return opid;
            }
            else if (loan.Debts.Sum(x => x.Amount) <= amount)
            {
                b = l.LoanPayment(new int[0],
                       new AltasoftAPI.LoansAPI.UserAndDeptId() { DeptId = 1, DeptIdSpecified = true, UserIdentification = new AltasoftAPI.LoansAPI.UserIdentification() { Id = 12, IdSpecified = true, Name = "ნინო საჩალელი" } },
                       new AltasoftAPI.LoansAPI.NameAmountCollectionItem[1] { new AltasoftAPI.LoansAPI.NameAmountCollectionItem() { Name = "payment", Amount = loan.Debts.Sum(x => x.Amount) } }, "დავალიანების დაფარვა",
                       loan.Id.Value,
                       true,
                       new AltasoftAPI.LoansAPI.PayerDetails { Client = customer.Name.ValueGeo },
                       loan.AccountIdentifier,
                       loan.AccountIdentifier.HasValue,
                       AltasoftAPI.LoansAPI.PaymentType.PrepaymentAndClose,
                       true, AltasoftAPI.LoansAPI.PrepaymentRescheduleStrategy.ByPMT,
                       false, AltasoftAPI.LoansAPI.PaymentSource.ClientResource,
                       true, true, true, loan.Version.Value, loan.VersionSpecified, out opid);
                etag = l.GetLoanOperationDetails(ref opid);
                l.ExecuteOperationAction(AltasoftAPI.LoansAPI.LoanOperationAction.IncrementAuthorization, true, "ავტორიზაცია", ref etag, ref opid, new AltasoftAPI.LoansAPI.UserAndDeptId() { DeptId = user.FirstOrDefault().DeptId, DeptIdSpecified = true, UserIdentification = new AltasoftAPI.LoansAPI.UserIdentification() { Id = user.FirstOrDefault().Id, IdSpecified = true, Name = user.FirstOrDefault().DisplayName } });
                return opid;
            }

            //if (loan.Debts.Sum(x => x.Amount) <= amount)
            //{
            //    b = l.LoanPayment(new int[0],
            //            new AltasoftAPI.LoansAPI.UserAndDeptId() { DeptId = 1, DeptIdSpecified = true, UserIdentification = new AltasoftAPI.LoansAPI.UserIdentification() { Id = 12, IdSpecified = true, Name = "ნინო საჩალელი" } },
            //            new AltasoftAPI.LoansAPI.NameAmountCollectionItem[1] { new AltasoftAPI.LoansAPI.NameAmountCollectionItem() { Name = "payment", Amount = amount } }, "დავალიანების დაფარვა",
            //            loan.Id.Value,
            //            true,
            //            new AltasoftAPI.LoansAPI.PayerDetails { Client = customer.Name.ValueGeo },
            //            loan.AccountIdentifier,
            //            loan.AccountIdentifier.HasValue,
            //            AltasoftAPI.LoansAPI.PaymentType.PrepaymentAndClose,
            //            true, AltasoftAPI.LoansAPI.PrepaymentRescheduleStrategy.ByPMT,
            //            false, AltasoftAPI.LoansAPI.PaymentSource.ClientResource,
            //            true, true, true, loan.Version.Value, loan.VersionSpecified, out opid);
            //    etag = l.GetLoanOperationDetails(ref opid);
            //    l.ExecuteOperationAction(AltasoftAPI.LoansAPI.LoanOperationAction.IncrementAuthorization, true, "ავტორიზაცია", ref etag, ref opid, new AltasoftAPI.LoansAPI.UserAndDeptId() { DeptId = user.FirstOrDefault().DeptId, DeptIdSpecified = true, UserIdentification = new AltasoftAPI.LoansAPI.UserIdentification() { Id = user.FirstOrDefault().Id, IdSpecified = true, Name = user.FirstOrDefault().DisplayName } });
            //    return opid;
            //}
            //else if (loan.Debts.Where(x => x.Name.Contains("undue_principal")).Count() > 0)
            //{
            //    if (loan.Debts.FirstOrDefault(x => x.Name.Contains("undue_principal")).Amount == 0)
            //    {
            //        throw new Exception("In loanAmounts undue_principal is 0");
            //    }
            //    if (loan.Debts.Where(x => x.Name.Contains("undue_principal")).Sum(x => x.Amount) > 0)
            //    {
            //        b = l.LoanPayment(new int[1] { 1 },
            //                new AltasoftAPI.LoansAPI.UserAndDeptId() { DeptId = 1, DeptIdSpecified = true, UserIdentification = new AltasoftAPI.LoansAPI.UserIdentification() { Id = 12, IdSpecified = true, Name = "ნინო საჩალელი" } },
            //                loanAmounts.ToArray(), "დავალიანების დაფარვა",
            //                loan.Id.Value,
            //                true,
            //                new AltasoftAPI.LoansAPI.PayerDetails { Client = customer.Name.ValueGeo },
            //                loan.AccountIdentifier,
            //                loan.AccountIdentifier.HasValue,
            //                AltasoftAPI.LoansAPI.PaymentType.Prepayment,
            //                true, AltasoftAPI.LoansAPI.PrepaymentRescheduleStrategy.ByPMT,
            //                false, AltasoftAPI.LoansAPI.PaymentSource.ClientResource,
            //                true, true, true, loan.Version.Value, loan.VersionSpecified, out opid);
            //        etag = l.GetLoanOperationDetails(ref opid);
            //        l.ExecuteOperationAction(AltasoftAPI.LoansAPI.LoanOperationAction.IncrementAuthorization, true, "ავტორიზაცია", ref etag, ref opid, new AltasoftAPI.LoansAPI.UserAndDeptId() { DeptId = user.FirstOrDefault().DeptId, DeptIdSpecified = true, UserIdentification = new AltasoftAPI.LoansAPI.UserIdentification() { Id = user.FirstOrDefault().Id, IdSpecified = true, Name = user.FirstOrDefault().DisplayName } });
            //        return opid;
            //    }
            //}
            //else
            //{
            //    b = l.LoanPayment(new int[1] { 1 },
            //            new AltasoftAPI.LoansAPI.UserAndDeptId() { DeptId = 1, DeptIdSpecified = true, UserIdentification = new AltasoftAPI.LoansAPI.UserIdentification() { Id = 12, IdSpecified = true, Name = "ნინო საჩალელი" } },
            //            loanAmounts.ToArray(), "დავალიანების დაფარვა",
            //            loan.Id.Value,
            //            true,
            //            new AltasoftAPI.LoansAPI.PayerDetails { Client = customer.Name.ValueGeo },
            //            loan.AccountIdentifier,
            //            loan.AccountIdentifier.HasValue,
            //            AltasoftAPI.LoansAPI.PaymentType.Payment,
            //            true, AltasoftAPI.LoansAPI.PrepaymentRescheduleStrategy.ByPMT,
            //            false, AltasoftAPI.LoansAPI.PaymentSource.ClientResource,
            //            true, true, true, loan.Version.Value, loan.VersionSpecified, out opid);
            //    etag = l.GetLoanOperationDetails(ref opid);
            //    l.ExecuteOperationAction(AltasoftAPI.LoansAPI.LoanOperationAction.IncrementAuthorization, true, "ავტორიზაცია", ref etag, ref opid, new AltasoftAPI.LoansAPI.UserAndDeptId() { DeptId = user.FirstOrDefault().DeptId, DeptIdSpecified = true, UserIdentification = new AltasoftAPI.LoansAPI.UserIdentification() { Id = user.FirstOrDefault().Id, IdSpecified = true, Name = user.FirstOrDefault().DisplayName } });
            //    return opid;
            //}


            throw new Exception("Loan not covered. LoanID: " + loan.Id);
            //l.ExecuteOperationAction(AltasoftAPI.LoansAPI.LoanOperationAction.IncrementAuthorization, true, "ავტორიზაცია", ref etag, ref opid, new AltasoftAPI.LoansAPI.UserAndDeptId() { DeptId = user.FirstOrDefault().DeptId, DeptIdSpecified = true, UserIdentification = new AltasoftAPI.LoansAPI.UserIdentification() { Id = user.FirstOrDefault().Id, IdSpecified = true, Name = user.FirstOrDefault().DisplayName } });
        }

    }
}
