using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AltasoftDaily.Domain;
using AltasoftDaily.Domain.POCO;
using System.Reflection;
using AltasoftDaily.Helpers;
using System.Net;
using System.Configuration;
using AltasoftAPI.AccountsAPI;

namespace AltasoftDaily.Core
{
    public class DailyManagement
    {
        public static List<DailyPayment> GetDailyByDeptId(int deptId)
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

            List<DailyPayment> list = new List<DailyPayment>();

            var loansIds = (from x in l.ListLoans(new AltasoftAPI.LoansAPI.ListLoansQuery() { ControlFlags = AltasoftAPI.LoansAPI.LoanControlFlags.Basic, Status = new AltasoftAPI.LoansAPI.LoanStatus[] { AltasoftAPI.LoansAPI.LoanStatus.Overdue, AltasoftAPI.LoansAPI.LoanStatus.Current, AltasoftAPI.LoansAPI.LoanStatus.Late } })
                            select x.Id.Value).ToList().OrderBy(x => x);


            foreach (var loanId in loansIds)
            {
                var loan = l.GetLoan(AltasoftAPI.LoansAPI.LoanControlFlags.Authorities | AltasoftAPI.LoansAPI.LoanControlFlags.Debts | AltasoftAPI.LoansAPI.LoanControlFlags.Basic, true, loanId, true);

                if (loan.BranchId == deptId)
                {
                    var item = new DailyPayment()
                    {
                        LoanID = loanId,
                        CurrentDebtInGel = loan.Debts.Where(x => !x.Name.Contains("undue_principal")).Sum(x => x.Amount),
                        TotalDebtInGel = loan.Debts.Sum(x => x.Amount),
                        InterestPenaltyInGel = loan.Debts.Where(x => x.Name.Contains("overdue_interest_penalty")).Sum(x => x.Amount),
                        PrincipalPenaltyInGel = loan.Debts.Where(x => x.Name.Contains("overdue_principal_penalty")).Sum(x => x.Amount),
                        OverdueInterestInGel = loan.Debts.Where(x => x.Name.Contains("overdue_interest#") || x.Name.Contains("overdue_principal_interest")).Sum(x => x.Amount),
                        AccruedInterestInGel = loan.Debts.Where(x => x.Name == ("interest")).Sum(x => x.Amount),
                        OverduePrincipalInGel = loan.Debts.Where(x => x.Name.Contains("overdue_principal#")).Sum(x => x.Amount),
                        PrincipalInGel = loan.Debts.Where(x => x.Name == ("undue_principal")).Sum(x => x.Amount),
                        CurrentPrincipalInGel = loan.Debts.Where(x => x.Name == ("principal")).Sum(x => x.Amount)
                    };

                    loan = l.GetLoan(AltasoftAPI.LoansAPI.LoanControlFlags.Basic, true, loanId, true);

                    item.CalculationDate = loan.CalcDate.Value.Date;
                    item.LoanAmountInGel = loan.Amount.Amount;
                    item.LoanCCY = loan.Amount.Ccy;
                    item.ClientNo = loan.BorrowerId.Value;
                    item.AgreementNumber = loan.AgreementNo;
                    item.StartDate = loan.Term.Start.ToShortDateString();
                    item.EndDate = loan.Term.End.ToShortDateString();

                    var customer = c.GetCustomer(AltasoftAPI.CustomersAPI.CustomerControlFlags.Basic, true, loan.BorrowerId.Value, true);
                    AltasoftAPI.CustomersAPI.Customer customer2;
                    var account = a.GetAccount(AltasoftAPI.AccountsAPI.AccountControlFlags.Basic, true, new AltasoftAPI.AccountsAPI.InternalAccountIdentification() { Id = loan.AccountIdentifier, IdSpecified = true }, item.LoanCCY);

                    customer2 = c.GetCustomer(AltasoftAPI.CustomersAPI.CustomerControlFlags.Extensions, true, loan.BorrowerId.Value, true);
                    item.Phone = customer2.ContactInfo.MobilePhone;
                    item.ClientAccountDescrip = account.DisplayName.ValueGeo;
                    item.ClientName = customer.Name.ValueGeo;
                    item.FirstName = (customer.Entity as AltasoftAPI.CustomersAPI.IndividualEntity).Name.FirstName.ValueGeo;
                    item.LastName = (customer.Entity as AltasoftAPI.CustomersAPI.IndividualEntity).Name.LastName.ValueGeo;
                    item.PersonalID = (customer.Entity as AltasoftAPI.CustomersAPI.IndividualEntity).PIN;
                    item.ClientAccountBranchCode = customer.BranchId.Value.ToString();
                    item.ClientAccountIban = account.IBAN;

                    AltasoftAPI.CustomersAPI.Customer customer3;

                    customer3 = c.GetCustomer(AltasoftAPI.CustomersAPI.CustomerControlFlags.Addresses, true, loan.BorrowerId.Value, true);
                    item.ClientAddressFact = customer3.AddressActual.Value.ValueGeo;


                    item.NextScheduledPaymentInGel = l.GetLoanSchedule(AltasoftAPI.LoansAPI.DebtComponentDetalization.Detailed, true, AltasoftAPI.LoansAPI.GetLoanScheduleControlFlags.Full, true, item.LoanID, true).FirstOrDefault(x => x.Date == DateTime.Today) != null ?
                        l.GetLoanSchedule(AltasoftAPI.LoansAPI.DebtComponentDetalization.Detailed, true, AltasoftAPI.LoansAPI.GetLoanScheduleControlFlags.Full, true, item.LoanID, true).FirstOrDefault(x => x.Date == DateTime.Today).Elements.Where(x => x.Name != "balance").Sum(x => x.Amount) : 0;

                    AltasoftAPI.LoansAPI.Application app;
                    bool? notm;
                    bool notms;
                    l.GetApplication(AltasoftAPI.LoansAPI.ApplicationControlFlags.ExtraFields, true, loan.Id.Value, true, out notm, out notms, out app);

                    item.BusinessAddress = app.Businesses.FirstOrDefault().Address;

                    list.Add(item);
                }
            }

            //list.AddRange(GetDailyByDeptId() as IEnumerable<DailyPayment>);

            return list.OrderBy(x => x.ClientNo).ToList();


        }

       

        public static List<DailyPayment> UpdateCommentsInDaily(List<DailyPayment> list)
        {
            var result = new List<DailyPayment>();
            using (var db = new AltasoftDailyContext())
            {
                //Collection was modified; enumeration operation may not execute.
                foreach (var comment in list)
                {
                    var payment = db.DailyPayments.FirstOrDefault(x => x.DailyPaymentID == comment.DailyPaymentID);

                    if (payment.Comment != comment.Comment)
                    {
                        payment.Comment = comment.Comment;
                        result.Add(comment);
                    }

                }
                db.SaveChanges();
            }
            return result;
        }

        public static List<DailyPayment> GetDailyByUser(int altasoftUserId)
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

            List<DailyPayment> list = new List<DailyPayment>();
            List<DailyPaymentAndLoan> data = new List<DailyPaymentAndLoan>();

            var loansIds = (from x in l.ListLoans(new AltasoftAPI.LoansAPI.ListLoansQuery() { ControlFlags = AltasoftAPI.LoansAPI.LoanControlFlags.Basic, Status = new AltasoftAPI.LoansAPI.LoanStatus[] { AltasoftAPI.LoansAPI.LoanStatus.Overdue, AltasoftAPI.LoansAPI.LoanStatus.Current, AltasoftAPI.LoansAPI.LoanStatus.Late } })
                            select x.Id.Value).ToList().OrderBy(x => x);

            using (var db = new AltasoftDailyContext())
            {
                var user = db.Users.FirstOrDefault(x => x.AltasoftUserID == altasoftUserId);


                loansIds.Where(x => x != 2331 /*&& x != 6*/).ToList().ForEach(x => data.Add(GetLoanAndDailyModel(x)));

                try
                {
                    if (user.Filter.IsDeptFilterEnabled && data != null)
                        data = data.Where(x => x != null && user.Filter.FilterData.Any(y => y.DeptID == x.DeptID)).ToList();

                    if (user.Filter.IsCustomerFilterEnabled && data != null)
                        data = data.Where(x => x != null && user.Filter.FilterData.Any(y => y.ClientID == x.ClientID)).ToList();

                    if (user.Filter.IsOperatorFilterEnabled && data != null)
                        data = data.Where(x => x != null && user.Filter.FilterData.Any(y => y.OperatorID == x.OperatorID)).ToList();
                }
                catch (NullReferenceException)
                {
                    return new List<DailyPayment>();
                }

                if (data != null)
                    data.ForEach(x => { if (x != null) list.Add(x.DailyPayment); });
            }
            return list.OrderBy(x => x.LoanID).ToList();
        }

        public static DailyPaymentAndLoan GetLoanAndDailyModel(int loanId)
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

            var result = new DailyPaymentAndLoan();
            var loan = l.GetLoan(AltasoftAPI.LoansAPI.LoanControlFlags.Authorities | AltasoftAPI.LoansAPI.LoanControlFlags.Debts | AltasoftAPI.LoansAPI.LoanControlFlags.Basic, true, loanId, true);

            var bbbb = from x in loan.Debts
                       select x.Name;

            #region Create Item
            var pm = loan.Authorities.LastOrDefault(x => x.Role == AltasoftAPI.LoansAPI.AuthorityRole.ProblemManager);

            var item = new DailyPayment()
            {
                LoanID = loanId,
                CurrentDebtInGel = loan.Debts.Where(x => !x.Name.Contains("undue_principal")).Sum(x => x.Amount),
                TotalDebtInGel = loan.Debts.Sum(x => x.Amount),
                InterestPenaltyInGel = loan.Debts.Where(x => x.Name.Contains("overdue_interest_penalty")).Sum(x => x.Amount),
                PrincipalPenaltyInGel = loan.Debts.Where(x => x.Name.Contains("overdue_principal_penalty")).Sum(x => x.Amount),
                OverdueInterestInGel = loan.Debts.Where(x => x.Name.Contains("overdue_interest#") || x.Name.Contains("overdue_principal_interest")).Sum(x => x.Amount),
                AccruedInterestInGel = loan.Debts.Where(x => x.Name == ("interest")).Sum(x => x.Amount),
                OverduePrincipalInGel = loan.Debts.Where(x => x.Name.Contains("overdue_principal#")).Sum(x => x.Amount),
                PrincipalInGel = loan.Debts.Where(x => x.Name == ("undue_principal")).Sum(x => x.Amount),
                CurrentPrincipalInGel = loan.Debts.Where(x => x.Name == ("principal")).Sum(x => x.Amount),
                LatePrincipalInGel = loan.Debts.Where(x => x.Name.ToLower().Contains("late") && x.Name.ToLower().Contains("principal")).Sum(x => x.Amount),
                ProblemManager = pm != null ? pm.Name : ""
            };

            result.OperatorID = loan.Authorities.LastOrDefault(x => x.Role == AltasoftAPI.LoansAPI.AuthorityRole.PrimaryResponsible).UserId.Value;
            item.ResponsibleUser = loan.Authorities.FirstOrDefault(x => x.Role == AltasoftAPI.LoansAPI.AuthorityRole.PrimaryResponsible).Name;

            loan = l.GetLoan(AltasoftAPI.LoansAPI.LoanControlFlags.Basic, true, loanId, true);

            item.DeptID = loan.BranchId.Value;
            item.CalculationDate = loan.CalcDate.Value.Date;
            item.LoanAmountInGel = loan.Amount.Amount;
            item.LoanCCY = loan.Amount.Ccy;
            item.ClientNo = loan.BorrowerId.Value;
            item.AgreementNumber = loan.AgreementNo;
            item.StartDate = loan.Term.Start.ToShortDateString();
            item.EndDate = loan.Term.End.ToShortDateString();

            result.ClientID = loan.BorrowerId.Value;
            result.DeptID = loan.BranchId.Value;

            var customer = c.GetCustomer(AltasoftAPI.CustomersAPI.CustomerControlFlags.Basic, true, loan.BorrowerId.Value, true);
            AltasoftAPI.CustomersAPI.Customer customer2;
            var account = a.GetAccount(AltasoftAPI.AccountsAPI.AccountControlFlags.Basic, true, new AltasoftAPI.AccountsAPI.InternalAccountIdentification() { Id = loan.AccountIdentifier, IdSpecified = true }, item.LoanCCY);

            customer2 = c.GetCustomer(AltasoftAPI.CustomersAPI.CustomerControlFlags.Extensions, true, loan.BorrowerId.Value, true);
            if (customer2.ContactInfo != null)
                item.Phone = customer2.ContactInfo.MobilePhone;

            item.ClientAccountDescrip = account.DisplayName.ValueGeo;
            item.ClientName = customer.Name.ValueGeo;//


            item.FirstName = (customer.Entity as AltasoftAPI.CustomersAPI.IndividualEntity).Name.FirstName.ValueGeo;
            item.LastName = (customer.Entity as AltasoftAPI.CustomersAPI.IndividualEntity).Name.LastName.ValueGeo;
            item.PersonalID = (customer.Entity as AltasoftAPI.CustomersAPI.IndividualEntity).PIN;


            item.ClientAccountBranchCode = customer.BranchId.Value.ToString();
            item.ClientAccountIban = account.IBAN;

            AltasoftAPI.CustomersAPI.Customer customer3;

            customer3 = c.GetCustomer(AltasoftAPI.CustomersAPI.CustomerControlFlags.Addresses, true, loan.BorrowerId.Value, true);
            if (customer3.AddressActual != null && customer3.AddressActual.Value != null)
            {
                item.ClientAddressFact = customer3.AddressActual.Value.ValueGeo;
            }

            item.NextScheduledPaymentInGel = l.GetLoanSchedule(AltasoftAPI.LoansAPI.DebtComponentDetalization.Detailed, true, AltasoftAPI.LoansAPI.GetLoanScheduleControlFlags.Full, true, item.LoanID, true).FirstOrDefault(x => x.Date == DateTime.Today) != null ?
                l.GetLoanSchedule(AltasoftAPI.LoansAPI.DebtComponentDetalization.Detailed, true, AltasoftAPI.LoansAPI.GetLoanScheduleControlFlags.Full, true, item.LoanID, true).FirstOrDefault(x => x.Date == DateTime.Today).Elements.Where(x => x.Name != "balance").Sum(x => x.Amount) : 0;

            AltasoftAPI.LoansAPI.Application app;
            bool? notm;
            bool notms;
            l.GetApplication(AltasoftAPI.LoansAPI.ApplicationControlFlags.ExtraFields, true, loan.Id.Value, true, out notm, out notms, out app);

            if (app.Businesses != null)
                item.BusinessAddress = app.Businesses.FirstOrDefault().Address;

            result.DailyPayment = item;
            #endregion

            return result;
        }

        public static long? SubmitOrder(int docNum, string ccy, DateTime date, string accountIBAN, decimal amount, string agreementNumber, string cashDeskSymbol, int userId, int deptId)
        {
            #region OrdersService
            AltasoftAPI.OrdersAPI.OrdersService o = new AltasoftAPI.OrdersAPI.OrdersService();
            o.RequestHeadersValue = new AltasoftAPI.OrdersAPI.RequestHeaders() { ApplicationKey = "BusinessCreditClient", RequestId = Guid.NewGuid().ToString() };
            #endregion

            #region CustomersService
            AltasoftAPI.CustomersAPI.CustomersService c = new AltasoftAPI.CustomersAPI.CustomersService();
            c.RequestHeadersValue = new AltasoftAPI.CustomersAPI.RequestHeaders() { ApplicationKey = "BusinessCreditClient", RequestId = Guid.NewGuid().ToString() };
            #endregion

            #region CustomersService
            AltasoftAPI.AccountsAPI.AccountsService a = new AltasoftAPI.AccountsAPI.AccountsService();
            a.RequestHeadersValue = new AltasoftAPI.AccountsAPI.RequestHeaders() { ApplicationKey = "BusinessCreditClient", RequestId = Guid.NewGuid().ToString() };
            #endregion

            #region LoansService
            AltasoftAPI.LoansAPI.LoansService l = new AltasoftAPI.LoansAPI.LoansService();
            l.RequestHeadersValue = new AltasoftAPI.LoansAPI.RequestHeaders() { ApplicationKey = "BusinessCreditClient", RequestId = Guid.NewGuid().ToString() };
            #endregion

            var acc = a.GetAccount(AltasoftAPI.AccountsAPI.AccountControlFlags.Basic, true, new AltasoftAPI.AccountsAPI.InternalAccountIdentification() { IBAN = accountIBAN }, ccy);
            var cus = c.GetCustomer(AltasoftAPI.CustomersAPI.CustomerControlFlags.Basic | AltasoftAPI.CustomersAPI.CustomerControlFlags.Addresses, true, acc.CustomerId.Value, true);
            var cusEntity = cus.Entity as AltasoftAPI.CustomersAPI.IndividualEntity;

            #region Variables
            long id;
            bool specified;
            #endregion

            #region Order Init
            var Order = new AltasoftAPI.OrdersAPI.CashOrderData
            {
                Amount = new AltasoftAPI.OrdersAPI.AmountAndCurrency { Amount = amount, Ccy = ccy },
                Date = date,
                Status = AltasoftAPI.OrdersAPI.TransactionStatus.Green,
                StatusSpecified = true,
                TransactionCode = "qwe34242342", //09 
                OpCode = "09",
                Purpose = "სესხის დაფარვა სესხის ხელშ. " + agreementNumber + "-ის საფუძველზე",
                //ExtraAccount = 0,
                //ExtraAccountSpecified = false,
                CustomerAccount = new AltasoftAPI.OrdersAPI.InternalAccountIdentification { IBAN = accountIBAN },
                OrderDate = date,
                OrderDateSpecified = true,
                Type = AltasoftAPI.OrdersAPI.CashOrderType.Deposit,
                Customer = new AltasoftAPI.OrdersAPI.CustomerData { Name = (AltasoftAPI.OrdersAPI.PersonName)cusEntity.Name },
                //DeptId = 5,
                //DeptIdSpecified = true,
                DocNum = docNum,
                DocNumSpecified = docNum == 0 ? false : true
            };

            cusEntity = (c.GetCustomer(AltasoftAPI.CustomersAPI.CustomerControlFlags.IdentityDocuments | AltasoftAPI.CustomersAPI.CustomerControlFlags.Basic, true, acc.CustomerId.Value, true).Entity as AltasoftAPI.CustomersAPI.IndividualEntity);

            Order.Customer.IdentityDocument = (AltasoftAPI.OrdersAPI.IdentityDocument)cusEntity.IdentityDocuments.FirstOrDefault();
            try
            {
                Order.Customer.Address = (AltasoftAPI.OrdersAPI.TextBilingual)cus.AddressActual.Value;
                Order.Customer.BirthPlaceDateAndCountry = (AltasoftAPI.OrdersAPI.BirthPlaceDateAndCountry)cusEntity.BirthPlaceDateAndCountry;

            }
            catch
            {
            }
            #endregion

            #region Put Order
            o.PutOrder(new AltasoftAPI.OrdersAPI.UserAndDeptId()
                        {
                            DeptId = deptId,
                            DeptIdSpecified = true,
                            UserIdentification = new AltasoftAPI.OrdersAPI.UserIdentification() { Id = userId, IdSpecified = true }
                        }, 0, false,
                               new Guid().ToString(),
                               true, true, false, true, Order, out id, out specified);
            #endregion



            return id;
        }
        public static List<DailyPaymentIDOrderID> SubmitOrdersFromDatabase(User user)
        {
            List<DailyPaymentIDOrderID> result = new List<DailyPaymentIDOrderID>();
            var calcDate = GetCalculationDate();

            using (var db = new AltasoftDailyContext())
            {
                var localPayments = db.DailyPayments.Where(x => x.CalculationDate == calcDate && x.LocalUserID == user.UserID && x.Payment > 0 && x.OrderID == null).ToList();

                foreach (var item in localPayments)
                {
                    DeleteOrder(item);
                    result.Add(new DailyPaymentIDOrderID() { OrderID = SubmitOrder(item.TaxOrderNumber, "GEL", item.CalculationDate.Date, item.ClientAccountIban, item.Payment, item.AgreementNumber, "09", user.AltasoftUserID, user.DeptID), PaymentID = item.DailyPaymentID, CoverLoanID = !item.IsOld ? CoverLoan(item.LoanID, item.Payment) : "Not Covered" });
                }
            }

            return result;
        }

        public static bool DeleteOrder(DailyPayment payment)
        {
            #region OrdersService
            AltasoftAPI.OrdersAPI.OrdersService o = new AltasoftAPI.OrdersAPI.OrdersService();
            o.RequestHeadersValue = new AltasoftAPI.OrdersAPI.RequestHeaders() { ApplicationKey = "BusinessCreditClient", RequestId = Guid.NewGuid().ToString() };
            #endregion

            if (!payment.OrderID.HasValue)
                return false;

            using (var db = new AltasoftDailyContext())
            {
                var user = db.Users.FirstOrDefault(x => x.UserID == payment.LocalUserID);
                o.DeleteOrder(new AltasoftAPI.OrdersAPI.UserAndDeptId() { DeptId = user.DeptID, DeptIdSpecified = true, UserIdentification = new AltasoftAPI.OrdersAPI.UserIdentification() { Id = user.AltasoftUserID, IdSpecified = true } }, payment.OrderID, payment.OrderID.HasValue, Guid.NewGuid().ToString(), true, true);
                db.DailyPayments.FirstOrDefault(x => x.DailyPaymentID == payment.DailyPaymentID).OrderID = null;
                db.SaveChanges();
            }

            return true;
        }

        public static bool UpdatePaymentsInDaily(List<DailyPayment> payments)
        {
            using (var db = new AltasoftDailyContext())
            {
                foreach (var payment in payments)
                {
                    db.DailyPayments.FirstOrDefault(x => x.DailyPaymentID == payment.DailyPaymentID).Payment = payment.Payment;
                }
                db.SaveChanges();
            }
            return true;
        }
        public static int GetUpdatesByAltasoftUser(User user)
        {
            var calcDate = GetCalculationDate();

            List<DailyPayment> result = new List<DailyPayment>();

            using (var db = new AltasoftDailyContext())
            {
                var localPayments = db.DailyPayments.Where(x => x.CalculationDate == calcDate && x.LocalUserID == user.UserID && !x.IsOld).ToList();
                var localPaymentsIds = from x in localPayments
                                       select x.LoanID;

                var lmsPayments = GetDailyByUser(user.AltasoftUserID).Where(x => x.CalculationDate == calcDate && x.LoanID != 2331);
                var lmsPaymentsIds = from x in lmsPayments
                                     select x.LoanID;

                var newPaymentsIds = lmsPaymentsIds.Except(localPaymentsIds).ToList();
                var oldPaymentsIds = localPaymentsIds.Except(lmsPaymentsIds).ToList();

                var newPayments = lmsPayments.Where(x => newPaymentsIds.Contains(x.LoanID));
                var oldPayments = localPayments.Where(x => oldPaymentsIds.Contains(x.LoanID));

                int count = 0;
                if (localPayments.Count > 0)
                    count = localPayments.Max(x => x.TaxOrderNumber);

                foreach (var item in newPayments)
                {
                    count++;
                    item.LocalUserID = user.UserID;
                    item.TaxOrderNumber = int.Parse(user.DeptID + user.AltasoftUserID.ToString() + count);
                }

                if (newPaymentsIds.Count > 0)
                    db.DailyPayments.AddRange(newPayments);

                if (oldPaymentsIds.Count > 0)
                    db.DailyPayments.RemoveRange(oldPayments);

                db.SaveChanges();
                return newPaymentsIds.Count;
            }
        }

        public static DateTime GetCalculationDate()
        {
            //#region Initialize Loans Service
            //AltasoftAPI.LoansAPI.LoansService l = new AltasoftAPI.LoansAPI.LoansService();
            //l.RequestHeadersValue = new AltasoftAPI.LoansAPI.RequestHeaders() { ApplicationKey = "BusinessCreditClient", RequestId = Guid.NewGuid().ToString() };
            //#endregion

            //return l.ListLoans(new AltasoftAPI.LoansAPI.ListLoansQuery() { ControlFlags = AltasoftAPI.LoansAPI.LoanControlFlags.Basic, Status = new AltasoftAPI.LoansAPI.LoanStatus[] { AltasoftAPI.LoansAPI.LoanStatus.Current } }).LastOrDefault().CalcDate.Value;
            ////return new DateTime(2015, 9, 27);
            return DateTime.Today;
        }

        public static string GetAccountIbanByDept(int deptId)
        {
            #region AccountsService
            AltasoftAPI.AccountsAPI.AccountsService a = new AltasoftAPI.AccountsAPI.AccountsService();
            a.RequestHeadersValue = new AltasoftAPI.AccountsAPI.RequestHeaders() { ApplicationKey = "BusinessCreditClient", RequestId = Guid.NewGuid().ToString() };
            #endregion

            var acc = a.GetAccount(AltasoftAPI.AccountsAPI.AccountControlFlags.Basic | AltasoftAPI.AccountsAPI.AccountControlFlags.Classifiers, true, new AltasoftAPI.AccountsAPI.InternalAccountIdentification() { AccountNumber = ulong.Parse(1001000.ToString() + deptId.ToString()), AccountNumberSpecified = true, Ccy = "GET", BranchId = deptId, BranchIdSpecified = true }, "GEL");

            return acc.IBAN;
        }

        public static int GetCollateralIdByLoanId(int loanId)
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


            AltasoftAPI.LoansAPI.Application app;
            bool? n;
            bool n2;

            l.GetApplication(AltasoftAPI.LoansAPI.ApplicationControlFlags.Basic | AltasoftAPI.LoansAPI.ApplicationControlFlags.Extensions, true, loanId, true, out n, out n2, out app);

            var collaterals = l.ListLinkedCollaterals(new AltasoftAPI.LoansAPI.ListLinkedCollateralsQuery()
                {
                    ControlFlags = AltasoftAPI.LoansAPI.LinkedCollateralControlFlags.Basic | AltasoftAPI.LoansAPI.LinkedCollateralControlFlags.Attributes,
                    ApplicationId = app.Id.Value,
                    ApplicationIdSpecified = app.IdSpecified
                });

            //var collateral = l.ListCollaterals(new AltasoftAPI.LoansAPI.ListCollateralsQuery()
            //    {
            //        ControlFlags = AltasoftAPI.LoansAPI.CollateralControlFlags.Basic | AltasoftAPI.LoansAPI.CollateralControlFlags.Attributes,
            //        AccountId = loan.AccountIdentifier,
            //        AccountIdSpecified = loan.AccountIdentifierSpecified,
            //        CollateralType = "03"
            //    });

            return collaterals.FirstOrDefault().CollateralId.Value;
        }

        public static SortableBindingList<BalanceReportModel> GetAccounts(DateTime startDate, DateTime endDate, decimal? balCode, int? deptId)
        {
            var dates = new List<DateTime>();

            for (var date = startDate; date <= endDate; date = date.AddDays(1))
                dates.Add(startDate);

            var list = new List<BalanceReportModel>();

            AltasoftAPI.AccountsAPI.Account[] result = null;

            foreach (var date in dates)
            {
                #region AccountsService
                AltasoftAPI.AccountsAPI.AccountsService a = new AltasoftAPI.AccountsAPI.AccountsService();
                a.RequestHeadersValue = new AltasoftAPI.AccountsAPI.RequestHeaders() { ApplicationKey = "BusinessCreditClient", RequestId = Guid.NewGuid().ToString() };
                #endregion

                var lll = new List<AccountStatement>();

                result = a.ListAccounts(new ListAccountsQuery()
                {
                    ControlFlags = AccountControlFlags.Basic | AccountControlFlags.Balances,
                    BalAcc = balCode,
                    BalAccSpecified = balCode.HasValue,
                    DeptId = deptId,
                    DeptIdSpecified = deptId.HasValue
                });

                foreach (var item in result)
                {
                    AccountStatement bal;

                    if (item.Ccy == "GEL")
                        bal = a.GetStatement(new InternalAccountIdentification() { IBAN = item.IBAN, Ccy = item.Ccy }, new Period() { Start = date, End = date }, false, false, TransactionStatus.Green, true, 0, false);
                    else
                        bal = a.GetStatement(new InternalAccountIdentification() { IBAN = item.IBAN, Ccy = item.Ccy }, new Period() { Start = date, End = date }, true, true, TransactionStatus.Green, true, 0, false);
                    lll.Add(bal);
                }

                foreach (var item in lll)
                {
                    list.Add(new BalanceReportModel()
                        {
                            Date = date,
                            Balance = item.EndingBalanceEqu.HasValue ? item.EndingBalanceEqu.Value : item.EndingBalance,
                            CreditAmount = item.Records.Sum(x => x.CreditAmountEqu.HasValue ? x.CreditAmountEqu.Value : x.CreditAmount.Value),
                            DebitAmount = item.Records.Sum(x => x.DebitAmountEqu.HasValue ? x.DebitAmountEqu.Value : x.DebitAmount.Value)
                        });
                }
            }

            var list2 = list.GroupBy(x => x.Date);

            var list3 = new List<BalanceReportModel>();

            foreach (var item in list2)
            {
                list3.Add(new BalanceReportModel()
                    {
                        Date = item.FirstOrDefault().Date,
                        Balance = item.Sum(x => x.Balance)
                    });
            }

            #region AccountsService
            AltasoftAPI.AccountsAPI.AccountsService g = new AltasoftAPI.AccountsAPI.AccountsService();
            g.RequestHeadersValue = new AltasoftAPI.AccountsAPI.RequestHeaders() { ApplicationKey = "BusinessCreditClient", RequestId = Guid.NewGuid().ToString() };
            #endregion

            List<AccountStatementRecord> sts = new List<AccountStatementRecord>();

            foreach (var item in result)
                sts.AddRange(g.GetStatement(new InternalAccountIdentification() { IBAN = item.IBAN, Ccy = item.Ccy }, new Period() { Start = startDate, End = endDate }, true, true, TransactionStatus.Green, true, 0, false).Records);//.Sum(x => x.DebitAmountEqu.HasValue ? x.DebitAmountEqu.Value : x.DebitAmount.Value);

            var grouppedsts = sts.GroupBy(x => x.Date);

            foreach (var item in grouppedsts)
            {
                list3.FirstOrDefault(x => x.Date == item.Key).DebitAmount = item.Sum(x => x.DebitAmountEqu.HasValue ? x.DebitAmountEqu.Value : x.DebitAmount.HasValue ? x.DebitAmount.Value : 0);
                list3.FirstOrDefault(x => x.Date == item.Key).CreditAmount = item.Sum(x => x.CreditAmountEqu.HasValue ? x.CreditAmountEqu.Value : x.CreditAmount.HasValue ? x.CreditAmount.Value : 0);
            }

            return new SortableBindingList<BalanceReportModel>(list3);
        }

        public static SortableBindingList<BalanceReportModel> GetAccounts2(DateTime startDate, DateTime endDate, decimal? balCode, int? deptId)
        {
            var list = new List<AccountStatementRecord>();
            var list2 = new List<object>();

            #region AccountsService
            AltasoftAPI.AccountsAPI.AccountsService a = new AltasoftAPI.AccountsAPI.AccountsService();
            a.RequestHeadersValue = new AltasoftAPI.AccountsAPI.RequestHeaders() { ApplicationKey = "BusinessCreditClient", RequestId = Guid.NewGuid().ToString() };
            #endregion

            var lll = new List<AccountStatement>();

            var result = new List<string>();

            result.Add("GE10AL0500000450100514");
            result.Add("GE59AL0300000450100514");
            result.Add("GE84AL0000000450100514");
            result.Add("GE15AL0100000045010012");
            result.Add("GE38AL0000000045010621");
            result.Add("GE11AL0000000045010622");
            result.Add("GE12AL0700000045010621");
            result.Add("GE34AL0600000450100514");
            result.Add("GE81AL0000000045010623");
            result.Add("GE54AL0000000045010624");
            result.Add("GE26AL0400000045010625");


            foreach (var item in result)
            {
                AccountStatement bal = a.GetStatement(new InternalAccountIdentification() { IBAN = item, Ccy = "GEL" }, new Period() { Start = startDate, End = endDate }, false, false, TransactionStatus.Green, true, 0, false);

                lll.Add(bal);
            }

            foreach (var item in lll)
                list.AddRange(item.Records.ToList());

            foreach (var item in list)
            {
                list2.Add(new Kunchik()
                {
                    AccountNumber = item.PartnerAccountId.AccountNumber,
                    AccountNumberSpecified = item.PartnerAccountId.AccountNumberSpecified,
                    Balance = item.Balance,
                    BalanceEqu = item.BalanceEqu,
                    BalanceEquSpecified = item.BalanceEquSpecified,
                    BranchId = item.PartnerAccountId.BranchId,
                    BranchIdSpecified = item.PartnerAccountId.BranchIdSpecified,
                    Ccy = item.PartnerAccountId.Ccy,
                    CreditAmount = item.CreditAmount,
                    CreditAmountEqu = item.CreditAmountEqu,
                    CreditAmountEquSpecified = item.CreditAmountEquSpecified,
                    CreditAmountSpecified = item.CreditAmountSpecified,
                    Date = item.Date,
                    DebitAmount = item.DebitAmount,
                    DebitAmountEqu = item.DebitAmountEqu,
                    DebitAmountEquSpecified = item.DebitAmountEquSpecified,
                    DebitAmountSpecified = item.DebitAmountSpecified,
                    DocNum = item.DocNum,
                    DocNumSpecified = item.DocNumSpecified,
                    ExtraDescription = item.ExtraDescription,
                    IBAN = item.PartnerAccountId.IBAN,
                    Id = item.PartnerAccountId.Id,
                    IdSpecified = item.PartnerAccountId.IdSpecified,
                    OpCode = item.OpCode,
                    OrderDate = item.OrderDate,
                    OrderDateSpecified = item.OrderDateSpecified,
                    OrderId = item.OrderId,
                    Purpose = item.Purpose,
                    Status = item.Status,
                    TransactionType = item.TransactionType
                });
            }

            TaxOrderGenerator.ExportToExcel(new SortableBindingList<object>(list2), typeof(Kunchik));

            //var list2 = list.GroupBy(x => x.Date);

            //var list3 = new List<BalanceReportModel>();

            //foreach (var item in list2)
            //{
            //    list3.Add(new BalanceReportModel()
            //    {
            //        Date = item.FirstOrDefault().Date,
            //        Balance = item.Sum(x => x.Balance)
            //    });
            //}

            //return new SortableBindingList<BalanceReportModel>(list3);

            return null;
        }
        public static SortableBindingList<BalanceReportModel> GetAccounts3(DateTime startDate, DateTime endDate, decimal? balCode, int? deptId)
        {

            var list3 = new List<BalanceReportModel>();

            #region AccountsService
            AltasoftAPI.AccountsAPI.AccountsService g = new AltasoftAPI.AccountsAPI.AccountsService();
            g.RequestHeadersValue = new AltasoftAPI.AccountsAPI.RequestHeaders() { ApplicationKey = "BusinessCreditClient", RequestId = Guid.NewGuid().ToString() };
            #endregion

            var result = g.ListAccounts(new ListAccountsQuery()
            {
                ControlFlags = AccountControlFlags.Basic | AccountControlFlags.Balances,
                BalAcc = balCode,
                BalAccSpecified = balCode.HasValue,
                DeptId = deptId,
                DeptIdSpecified = deptId.HasValue
            });

            List<AccountStatementRecord> sts = new List<AccountStatementRecord>();

            foreach (var item in result)
                sts.AddRange(g.GetStatement(new InternalAccountIdentification() { IBAN = item.IBAN, Ccy = item.Ccy }, new Period() { Start = startDate, End = endDate }, true, true, TransactionStatus.Green, true, 0, false).Records);//.Sum(x => x.DebitAmountEqu.HasValue ? x.DebitAmountEqu.Value : x.DebitAmount.Value);

            var grouppedsts = sts.GroupBy(x => x.Date);

            foreach (var item in grouppedsts)
            {
                list3.Add(new BalanceReportModel() { DebitAmount = item.Sum(x => x.DebitAmountEqu.HasValue ? x.DebitAmountEqu.Value : x.DebitAmount.HasValue ? x.DebitAmount.Value : 0), CreditAmount = item.Sum(x => x.CreditAmountEqu.HasValue ? x.CreditAmountEqu.Value : x.CreditAmount.HasValue ? x.CreditAmount.Value : 0), Date = item.Key });
            }

            return new SortableBindingList<BalanceReportModel>(list3);

        }


        public static SortableBindingList<Kunchik> GetAccountsTest(DateTime startDate, DateTime endDate, decimal[] balCodes, int? deptId)
        {
            var list = new Dictionary<List<AccountStatementRecord>, AltasoftAPI.AccountsAPI.Account>();
            var list2 = new List<Kunchik>();

            #region AccountsService
            AltasoftAPI.AccountsAPI.AccountsService a = new AltasoftAPI.AccountsAPI.AccountsService();
            a.RequestHeadersValue = new AltasoftAPI.AccountsAPI.RequestHeaders() { ApplicationKey = "BusinessCreditClient", RequestId = Guid.NewGuid().ToString() };
            #endregion

            var lll = new Dictionary<AccountStatement, AltasoftAPI.AccountsAPI.Account>();

            var result = new List<AltasoftAPI.AccountsAPI.Account>();

            foreach (var balCode in balCodes)
            {
                result.AddRange(a.ListAccounts(new ListAccountsQuery()
                {
                    ControlFlags = AccountControlFlags.Basic | AccountControlFlags.Balances,
                    BalAcc = balCode,
                    BalAccSpecified = true,
                    DeptId = deptId,
                    DeptIdSpecified = deptId.HasValue
                }));
            }

            foreach (var item in result)
            {
                AccountStatement bal = a.GetStatement(new InternalAccountIdentification() { IBAN = item.IBAN, Ccy = item.Ccy }, new Period() { Start = startDate.Date, End = endDate.Date }, true, true, TransactionStatus.Green, true, 0, false);

                lll.Add(bal, item);
            }

            foreach (var item in lll)
                foreach (var item2 in item.Key.Records)
                    list.Add(item.Key.Records.ToList(), item.Value);

            foreach (var item in list)
            {
                foreach (var itt in item.Key)
                {

                    list2.Add(new Kunchik()
                    {
                        AccountNumber = item.Value.AccountNumber,
                        AccountNumberSpecified = item.Value.AccountNumberSpecified,
                        Balance = itt.Balance,
                        BalanceEqu = itt.BalanceEqu,
                        BalanceEquSpecified = itt.BalanceEquSpecified,
                        BranchId = item.Value.BranchId,
                        BranchIdSpecified = item.Value.BranchIdSpecified,
                        Ccy = item.Value.Ccy,
                        CreditAmount = itt.CreditAmount,
                        CreditAmountEqu = itt.CreditAmountEqu,
                        CreditAmountEquSpecified = itt.CreditAmountEquSpecified,
                        CreditAmountSpecified = itt.CreditAmountSpecified,
                        Date = itt.Date,
                        DebitAmount = itt.DebitAmount,
                        DebitAmountEqu = itt.DebitAmountEqu,
                        DebitAmountEquSpecified = itt.DebitAmountEquSpecified,
                        DebitAmountSpecified = itt.DebitAmountSpecified,
                        DocNum = itt.DocNum,
                        DocNumSpecified = itt.DocNumSpecified,
                        ExtraDescription = itt.ExtraDescription,
                        IBAN = item.Value.IBAN,
                        Id = item.Value.Id,
                        IdSpecified = item.Value.IdSpecified,
                        OpCode = itt.OpCode,
                        OrderDate = itt.OrderDate,
                        OrderDateSpecified = itt.OrderDateSpecified,
                        OrderId = itt.OrderId,
                        Purpose = itt.Purpose,
                        Status = itt.Status,
                        TransactionType = itt.TransactionType,
                        AccountName = item.Value.DisplayName.ValueGeo,
                        BC_IBAN = itt.PartnerAccountId.IBAN
                    });
                }
            }

            return new SortableBindingList<Kunchik>(list2.GroupBy(x => x.OrderId).Select(x => x.First()).ToList());

            //TaxOrderGenerator.ExportToExcel(new SortableBindingList<object>(list2), typeof(Kunchik));

            //var list2 = list.GroupBy(x => x.Date);

            //var list3 = new List<BalanceReportModel>();

            //foreach (var item in list2)
            //{
            //    list3.Add(new BalanceReportModel()
            //    {
            //        Date = item.FirstOrDefault().Date,
            //        Balance = item.Sum(x => x.Balance)
            //    });
            //}

            //return new SortableBindingList<BalanceReportModel>(list3);
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
            //var money = amount;
            //var priorities = "fee,overdue_interest_penalty,overdue_nu_interest_penalty,overdue_principal_penalty,overdue_interest,overdue_principal_interest,overdue_nu_interest,late_interest,late_nu_interest,interest,nu_interest,overdue_principal,late_principal,principal,writeoff_penalty,writeoff_interest,writeoff_nu_interest,writeoff_principal,undue_principal".Split(',');
            //var substractedDebts = new List<Tuple<string, string, decimal>>();
            //var loanAmounts = new List<AltasoftAPI.LoansAPI.NameAmountCollectionItem>();
            var user = l.ListUsers(new AltasoftAPI.LoansAPI.ListUsersQuery() { Id = 12, IdSpecified = true });
            string opid;
            var customer = c.GetCustomer(AltasoftAPI.CustomersAPI.CustomerControlFlags.Basic | AltasoftAPI.CustomersAPI.CustomerControlFlags.IdentityDocuments, true, loan.BorrowerId.Value, true);
            string b = "";
            string etag = "";

            #region Comment
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
            #endregion

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
                        true, AltasoftAPI.LoansAPI.PrepaymentRescheduleStrategy.ByPMT,
                        false, AltasoftAPI.LoansAPI.PaymentSource.ClientResource,
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



        /////////////////////////////////////////////////

        public static List<DailyPayment> GetDailyByUserTest(int altasoftUserId)
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

            List<DailyPayment> list = new List<DailyPayment>();
            List<DailyPaymentAndLoan> data = new List<DailyPaymentAndLoan>();

            var loanss = l.ListLoans(new AltasoftAPI.LoansAPI.ListLoansQuery() { ControlFlags = AltasoftAPI.LoansAPI.LoanControlFlags.Authorities | AltasoftAPI.LoansAPI.LoanControlFlags.Debts | AltasoftAPI.LoansAPI.LoanControlFlags.Basic, Status = new AltasoftAPI.LoansAPI.LoanStatus[] { AltasoftAPI.LoansAPI.LoanStatus.Overdue, AltasoftAPI.LoansAPI.LoanStatus.Current, AltasoftAPI.LoansAPI.LoanStatus.Late } });

            foreach (var loan in loanss)
            {
                var result = new DailyPaymentAndLoan();

                #region Create Item
                var item = new DailyPayment()
                {
                    LoanID = loan.Id.Value,
                    CurrentDebtInGel = loan.Debts.Where(x => !x.Name.Contains("undue_principal")).Sum(x => x.Amount),
                    TotalDebtInGel = loan.Debts.Sum(x => x.Amount),
                    InterestPenaltyInGel = loan.Debts.Where(x => x.Name.Contains("overdue_interest_penalty")).Sum(x => x.Amount),
                    PrincipalPenaltyInGel = loan.Debts.Where(x => x.Name.Contains("overdue_principal_penalty")).Sum(x => x.Amount),
                    OverdueInterestInGel = loan.Debts.Where(x => x.Name.Contains("overdue_interest#") || x.Name.Contains("overdue_principal_interest")).Sum(x => x.Amount),
                    AccruedInterestInGel = loan.Debts.Where(x => x.Name == ("interest")).Sum(x => x.Amount),
                    OverduePrincipalInGel = loan.Debts.Where(x => x.Name.Contains("overdue_principal#")).Sum(x => x.Amount),
                    PrincipalInGel = loan.Debts.Where(x => x.Name == ("undue_principal")).Sum(x => x.Amount),
                    CurrentPrincipalInGel = loan.Debts.Where(x => x.Name == ("principal")).Sum(x => x.Amount)
                };

                result.OperatorID = loan.Authorities.FirstOrDefault(x => x.Role == AltasoftAPI.LoansAPI.AuthorityRole.Operator).UserId.Value;
                item.ResponsibleUser = loan.Authorities.FirstOrDefault(x => x.Role == AltasoftAPI.LoansAPI.AuthorityRole.PrimaryResponsible).Name;

                item.DeptID = loan.BranchId.Value;
                item.CalculationDate = loan.CalcDate.Value.Date;
                item.LoanAmountInGel = loan.Amount.Amount;
                item.LoanCCY = loan.Amount.Ccy;
                item.ClientNo = loan.BorrowerId.Value;
                item.AgreementNumber = loan.AgreementNo;
                item.StartDate = loan.Term.Start.ToShortDateString();
                item.EndDate = loan.Term.End.ToShortDateString();

                result.ClientID = loan.BorrowerId.Value;
                result.DeptID = loan.BranchId.Value;

                var customer = c.GetCustomer(AltasoftAPI.CustomersAPI.CustomerControlFlags.Basic, true, loan.BorrowerId.Value, true);
                AltasoftAPI.CustomersAPI.Customer customer2;
                var account = a.GetAccount(AltasoftAPI.AccountsAPI.AccountControlFlags.Basic, true, new AltasoftAPI.AccountsAPI.InternalAccountIdentification() { Id = loan.AccountIdentifier, IdSpecified = true }, item.LoanCCY);

                customer2 = c.GetCustomer(AltasoftAPI.CustomersAPI.CustomerControlFlags.Extensions, true, loan.BorrowerId.Value, true);
                item.Phone = customer2.ContactInfo.MobilePhone;
                item.ClientAccountDescrip = account.DisplayName.ValueGeo;
                item.ClientName = customer.Name.ValueGeo;
                item.FirstName = (customer.Entity as AltasoftAPI.CustomersAPI.IndividualEntity).Name.FirstName.ValueGeo;
                item.LastName = (customer.Entity as AltasoftAPI.CustomersAPI.IndividualEntity).Name.LastName.ValueGeo;
                item.PersonalID = (customer.Entity as AltasoftAPI.CustomersAPI.IndividualEntity).PIN;
                item.ClientAccountBranchCode = customer.BranchId.Value.ToString();
                item.ClientAccountIban = account.IBAN;

                AltasoftAPI.CustomersAPI.Customer customer3;

                customer3 = c.GetCustomer(AltasoftAPI.CustomersAPI.CustomerControlFlags.Addresses, true, loan.BorrowerId.Value, true);
                if (customer3.AddressActual != null)
                {
                    item.ClientAddressFact = customer3.AddressActual.Value.ValueGeo;
                }

                item.NextScheduledPaymentInGel = l.GetLoanSchedule(AltasoftAPI.LoansAPI.DebtComponentDetalization.Detailed, true, AltasoftAPI.LoansAPI.GetLoanScheduleControlFlags.Full, true, item.LoanID, true).FirstOrDefault(x => x.Date == DateTime.Today) != null ?
                    l.GetLoanSchedule(AltasoftAPI.LoansAPI.DebtComponentDetalization.Detailed, true, AltasoftAPI.LoansAPI.GetLoanScheduleControlFlags.Full, true, item.LoanID, true).FirstOrDefault(x => x.Date == DateTime.Today).Elements.Where(x => x.Name != "balance").Sum(x => x.Amount) : 0;

                AltasoftAPI.LoansAPI.Application app;
                bool? notm;
                bool notms;
                l.GetApplication(AltasoftAPI.LoansAPI.ApplicationControlFlags.ExtraFields, true, loan.Id.Value, true, out notm, out notms, out app);

                item.BusinessAddress = app.Businesses.FirstOrDefault().Address;

                result.DailyPayment = item;
                data.Add(result);
                #endregion
            }


            using (var db = new AltasoftDailyContext())
            {
                var user = db.Users.FirstOrDefault(x => x.AltasoftUserID == altasoftUserId);

                try
                {
                    if (user.Filter.IsDeptFilterEnabled && data != null)
                        data = data.Where(x => x != null && user.Filter.FilterData.Any(y => y.DeptID == x.DeptID)).ToList();

                    if (user.Filter.IsCustomerFilterEnabled && data != null)
                        data = data.Where(x => x != null && user.Filter.FilterData.Any(y => y.ClientID == x.ClientID)).ToList();

                    if (user.Filter.IsOperatorFilterEnabled && data != null)
                        data = data.Where(x => x != null && user.Filter.FilterData.Any(y => y.OperatorID == x.OperatorID)).ToList();
                }
                catch (NullReferenceException)
                {
                    return new List<DailyPayment>();
                }

                if (data != null)
                    data.ForEach(x => { if (x != null) list.Add(x.DailyPayment); });
            }
            return list.OrderBy(x => x.LoanID).ToList();
        }

        /////////////////////////////////////////////////

        public static List<EnforcementLoan> ListEnForcementLoans(User problemManager, params int[] localIds)
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

            var loansIds = (from x in l.ListLoans
                            (new AltasoftAPI.LoansAPI.ListLoansQuery()
                                {
                                    ControlFlags = AltasoftAPI.LoansAPI.LoanControlFlags.Basic,
                                    Status = new AltasoftAPI.LoansAPI.LoanStatus[] { AltasoftAPI.LoansAPI.LoanStatus.Overdue, AltasoftAPI.LoansAPI.LoanStatus.Current }
                                }) select x.Id.Value).Except(localIds).ToArray();

            var list1 = new List<EnforcementLoan>();

            foreach (var i in loansIds)
            {
                var loan = l.GetLoan(AltasoftAPI.LoansAPI.LoanControlFlags.Basic | AltasoftAPI.LoansAPI.LoanControlFlags.Authorities | AltasoftAPI.LoansAPI.LoanControlFlags.Debts, true, i, true);

                if (loan.Authorities.FirstOrDefault(x => x.Role == AltasoftAPI.LoansAPI.AuthorityRole.ProblemManager && x.UserId.Value == problemManager.AltasoftUserID) == null)
                    continue;

                var cus1 = c.GetCustomer(AltasoftAPI.CustomersAPI.CustomerControlFlags.Basic | AltasoftAPI.CustomersAPI.CustomerControlFlags.Addresses | AltasoftAPI.CustomersAPI.CustomerControlFlags.ContactPersons | AltasoftAPI.CustomersAPI.CustomerControlFlags.Extensions, true, loan.BorrowerId.Value, true);

                /////////////////////////////////
                AltasoftAPI.LoansAPI.Application app;
                bool? n;
                bool n2;
                
                l.GetApplication(AltasoftAPI.LoansAPI.ApplicationControlFlags.Basic | AltasoftAPI.LoansAPI.ApplicationControlFlags.Extensions, true, i, true, out n, out n2, out app);

                var collaterals = l.ListLinkedCollaterals(new AltasoftAPI.LoansAPI.ListLinkedCollateralsQuery()
                {
                    ControlFlags = AltasoftAPI.LoansAPI.LinkedCollateralControlFlags.Basic | AltasoftAPI.LoansAPI.LinkedCollateralControlFlags.Attributes,
                    ApplicationId = app.Id.Value,
                    ApplicationIdSpecified = app.IdSpecified
                });

                /////////////////////////////////

                var enf = new EnforcementLoan();

                enf.LoanID = loan.Id.Value;
                enf.AgreementAndSummaryJudgementTerms = new AgreementAndSummaryJudgementTerms() { End = DateTime.Now, Start = DateTime.Now, PaymentDay = 0 };
                try
                {
                    enf.BorrowerAddress = cus1.AddressActual != null ? cus1.AddressActual.Value.ValueGeo : "";
                }
                catch { }
                enf.BorrowerName = cus1.Name != null ? cus1.Name.ValueGeo : "";
                enf.BorrowerPhone = cus1.ContactInfo != null ? cus1.ContactInfo.MobilePhone : "";
                enf.CreditExpert = loan.Authorities.FirstOrDefault(x => x.Role == AltasoftAPI.LoansAPI.AuthorityRole.Operator).Name;
                enf.LoanAgreementNumber = loan.AgreementNo;
                enf.Status = EnforcementLoanStatus.Active;
                enf.GivePLD = DateTime.Now;
                enf.ProblemManagerName = loan.Authorities.FirstOrDefault(x => x.Role == AltasoftAPI.LoansAPI.AuthorityRole.ProblemManager).Name;
                enf.ProblemManagerID = loan.Authorities.FirstOrDefault(x => x.Role == AltasoftAPI.LoansAPI.AuthorityRole.ProblemManager).UserId.Value;
                enf.BorrowerPrivateNumber = (cus1.Entity as AltasoftAPI.CustomersAPI.IndividualEntity).PIN;

                enf.CaseStatus = EnforcementCaseStatus.NewCase;

                ///////////////////////////////////////////
                enf.LoanPrincipal = loan.Debts.Where(x => x.Name.Contains("overdue_principal#")).Sum(x => x.Amount)                                         //OverduePrincipalInGel
                                  + loan.Debts.Where(x => x.Name == ("undue_principal")).Sum(x => x.Amount)                                                 //PrincipalInGel
                                  + loan.Debts.Where(x => x.Name == ("principal")).Sum(x => x.Amount)                                                       //CurrentPrincipalInGel
                                  + loan.Debts.Where(x => x.Name.ToLower().Contains("late") && x.Name.ToLower().Contains("principal")).Sum(x => x.Amount);  //LatePrincipalInGel

                enf.LoanInterest = loan.Debts.Where(x => x.Name.Contains("overdue_interest#") || x.Name.Contains("overdue_principal_interest")).Sum(x => x.Amount) //OverdueInterestInGel
                                 + loan.Debts.Where(x => x.Name == ("interest")).Sum(x => x.Amount);                                                               //AccruedInterestInGel

                enf.LoanPenalty = loan.Debts.Where(x => x.Name.Contains("overdue_interest_penalty")).Sum(x => x.Amount)          //InterestPenaltyInGel 
                                + loan.Debts.Where(x => x.Name.Contains("overdue_principal_penalty")).Sum(x => x.Amount);        //PrincipalPenaltyInGel
                enf.TotalLoanDebt = loan.Debts.Sum(x => x.Amount);
                ///////////////////////////////////////////

                enf.ApplicationSubmitDate = DateTime.Now;
                enf.LoanStartDate = loan.Term.Start;

                if (collaterals.Count() > 0)
                {
                    var coll = l.ListCollaterals(new AltasoftAPI.LoansAPI.ListCollateralsQuery()
                    {
                        Id = collaterals.FirstOrDefault().CollateralId,
                        IdSpecified = collaterals.FirstOrDefault().CollateralIdSpecified,
                        ControlFlags = AltasoftAPI.LoansAPI.CollateralControlFlags.Basic | AltasoftAPI.LoansAPI.CollateralControlFlags.Attributes
                    });

                    var col1 = c.GetCustomer(AltasoftAPI.CustomersAPI.CustomerControlFlags.Basic | AltasoftAPI.CustomersAPI.CustomerControlFlags.Addresses, true, coll[0].OwnerId.Value, true);
                }

                if (loan.Authorities.FirstOrDefault(x => x.Role == AltasoftAPI.LoansAPI.AuthorityRole.ProblemManager) != null)
                    list1.Add(enf);
            }

            return list1;
        }

        public static List<LoanDebts> ListLoanDebts(int[] loanIds)
        {
            #region LoansService
            AltasoftAPI.LoansAPI.LoansService l = new AltasoftAPI.LoansAPI.LoansService();
            l.RequestHeadersValue = new AltasoftAPI.LoansAPI.RequestHeaders() { ApplicationKey = "BusinessCreditClient", RequestId = Guid.NewGuid().ToString() };
            #endregion

            var result = new List<LoanDebts>();

            foreach (var i in loanIds)
            {
                var loan = l.GetLoan(AltasoftAPI.LoansAPI.LoanControlFlags.Basic | AltasoftAPI.LoansAPI.LoanControlFlags.Authorities | AltasoftAPI.LoansAPI.LoanControlFlags.Debts, true, i, true);

                if (loan.Debts == null)
                    continue;

                var enf = new LoanDebts() { LoanID = i };

                ///////////////////////////////////////////
                enf.Principal = loan.Debts.Where(x => x.Name.Contains("overdue_principal#")).Sum(x => x.Amount)                                         //OverduePrincipalInGel
                                  + loan.Debts.Where(x => x.Name == ("undue_principal")).Sum(x => x.Amount)                                                 //PrincipalInGel
                                  + loan.Debts.Where(x => x.Name == ("principal")).Sum(x => x.Amount)                                                       //CurrentPrincipalInGel
                                  + loan.Debts.Where(x => x.Name.ToLower().Contains("late") && x.Name.ToLower().Contains("principal")).Sum(x => x.Amount);  //LatePrincipalInGel

                enf.Interest = loan.Debts.Where(x => x.Name.Contains("overdue_interest#") || x.Name.Contains("overdue_principal_interest")).Sum(x => x.Amount) //OverdueInterestInGel
                                 + loan.Debts.Where(x => x.Name == ("interest")).Sum(x => x.Amount);                                                               //AccruedInterestInGel

                enf.Penalty = loan.Debts.Where(x => x.Name.Contains("overdue_interest_penalty")).Sum(x => x.Amount)          //InterestPenaltyInGel 
                                + loan.Debts.Where(x => x.Name.Contains("overdue_principal_penalty")).Sum(x => x.Amount);        //PrincipalPenaltyInGel
                enf.Other = loan.Debts.Sum(x => x.Amount) - (enf.Principal + enf.Interest + enf.Penalty);
                ///////////////////////////////////////////

                result.Add(enf);
            }

            return result;
        }
    }

    public class Kunchik
    {
        public string AccountName { get; set; }

        private System.Nullable<int> idField;

        private bool idFieldSpecified;

        private string iBANField;

        private System.Nullable<ulong> accountNumberField;

        private bool accountNumberFieldSpecified;

        private System.Nullable<int> branchIdField;

        private bool branchIdFieldSpecified;

        private string ccyField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public System.Nullable<int> Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool IdSpecified
        {
            get
            {
                return this.idFieldSpecified;
            }
            set
            {
                this.idFieldSpecified = value;
            }
        }


        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string IBAN
        {
            get
            {
                return this.iBANField;
            }
            set
            {
                this.iBANField = value;
            }
        }

        public string BC_IBAN { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public System.Nullable<ulong> AccountNumber
        {
            get
            {
                return this.accountNumberField;
            }
            set
            {
                this.accountNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AccountNumberSpecified
        {
            get
            {
                return this.accountNumberFieldSpecified;
            }
            set
            {
                this.accountNumberFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public System.Nullable<int> BranchId
        {
            get
            {
                return this.branchIdField;
            }
            set
            {
                this.branchIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool BranchIdSpecified
        {
            get
            {
                return this.branchIdFieldSpecified;
            }
            set
            {
                this.branchIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string Ccy
        {
            get
            {
                return this.ccyField;
            }
            set
            {
                this.ccyField = value;
            }
        }









        /////////////////////////////////////////////////

        private long orderIdField;

        private System.DateTime dateField;

        private System.Nullable<System.DateTime> orderDateField;

        private bool orderDateFieldSpecified;

        private TransactionStatus statusField;

        private byte transactionTypeField;

        private System.Nullable<decimal> debitAmountField;

        private bool debitAmountFieldSpecified;

        private System.Nullable<decimal> creditAmountField;

        private bool creditAmountFieldSpecified;

        private System.Nullable<decimal> debitAmountEquField;

        private bool debitAmountEquFieldSpecified;

        private System.Nullable<decimal> creditAmountEquField;

        private bool creditAmountEquFieldSpecified;



        private string purposeField;

        private string extraDescriptionField;

        private string opCodeField;

        private System.Nullable<int> docNumField;

        private bool docNumFieldSpecified;

        private decimal balanceField;

        private System.Nullable<decimal> balanceEquField;

        private bool balanceEquFieldSpecified;

        /// <remarks/>
        public long OrderId
        {
            get
            {
                return this.orderIdField;
            }
            set
            {
                this.orderIdField = value;
            }
        }

        /// <remarks/>
        public System.DateTime Date
        {
            get
            {
                return this.dateField;
            }
            set
            {
                this.dateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public System.Nullable<System.DateTime> OrderDate
        {
            get
            {
                return this.orderDateField;
            }
            set
            {
                this.orderDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool OrderDateSpecified
        {
            get
            {
                return this.orderDateFieldSpecified;
            }
            set
            {
                this.orderDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public TransactionStatus Status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }

        /// <remarks/>
        public byte TransactionType
        {
            get
            {
                return this.transactionTypeField;
            }
            set
            {
                this.transactionTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public System.Nullable<decimal> DebitAmount
        {
            get
            {
                return this.debitAmountField;
            }
            set
            {
                this.debitAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DebitAmountSpecified
        {
            get
            {
                return this.debitAmountFieldSpecified;
            }
            set
            {
                this.debitAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public System.Nullable<decimal> CreditAmount
        {
            get
            {
                return this.creditAmountField;
            }
            set
            {
                this.creditAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CreditAmountSpecified
        {
            get
            {
                return this.creditAmountFieldSpecified;
            }
            set
            {
                this.creditAmountFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public System.Nullable<decimal> DebitAmountEqu
        {
            get
            {
                return this.debitAmountEquField;
            }
            set
            {
                this.debitAmountEquField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DebitAmountEquSpecified
        {
            get
            {
                return this.debitAmountEquFieldSpecified;
            }
            set
            {
                this.debitAmountEquFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public System.Nullable<decimal> CreditAmountEqu
        {
            get
            {
                return this.creditAmountEquField;
            }
            set
            {
                this.creditAmountEquField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CreditAmountEquSpecified
        {
            get
            {
                return this.creditAmountEquFieldSpecified;
            }
            set
            {
                this.creditAmountEquFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string Purpose
        {
            get
            {
                return this.purposeField;
            }
            set
            {
                this.purposeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string ExtraDescription
        {
            get
            {
                return this.extraDescriptionField;
            }
            set
            {
                this.extraDescriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string OpCode
        {
            get
            {
                return this.opCodeField;
            }
            set
            {
                this.opCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public System.Nullable<int> DocNum
        {
            get
            {
                return this.docNumField;
            }
            set
            {
                this.docNumField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DocNumSpecified
        {
            get
            {
                return this.docNumFieldSpecified;
            }
            set
            {
                this.docNumFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal Balance
        {
            get
            {
                return this.balanceField;
            }
            set
            {
                this.balanceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public System.Nullable<decimal> BalanceEqu
        {
            get
            {
                return this.balanceEquField;
            }
            set
            {
                this.balanceEquField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool BalanceEquSpecified
        {
            get
            {
                return this.balanceEquFieldSpecified;
            }
            set
            {
                this.balanceEquFieldSpecified = value;
            }
        }
    }
}


//public static bool AddDaily(List<DailyPayment> payments)
//{
//    using (var db = new AltasoftDailyContext())
//    {
//        db.DailyPayments.AddRange(payments);
//        db.SaveChanges();
//    }
//    return true;
//}