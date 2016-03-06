﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AltasoftDaily.Domain;
using AltasoftDaily.Domain.POCO;
using System.Reflection;
using AltasoftDaily.Helpers;
using System.Net;
using BusinessCredit.Core;
using BusinessCredit.Domain;
using System.Configuration;
using AltasoftAPI.AccountsAPI;

namespace AltasoftDaily.Core
{
    public class DailyManagement
    {
        #region Databases
        private BusinessCreditContext _centralDb;

        public BusinessCreditContext CentralDb
        {
            get
            {
                if (_centralDb == null)
                    _centralDb = new BusinessCreditContext(ConfigurationManager.ConnectionStrings["Head_BusinessCreditDbConnectionString"].ConnectionString);
                return _centralDb;
            }
        }

        private BusinessCreditContext _isaniDb;

        public BusinessCreditContext IsaniDb
        {
            get
            {
                if (_isaniDb == null)
                    _isaniDb = new BusinessCreditContext(ConfigurationManager.ConnectionStrings["Isani_BusinessCreditDbConnectionString"].ConnectionString);
                return _isaniDb;
            }
        }

        private BusinessCreditContext _okribaDb;

        public BusinessCreditContext OkribaDb
        {
            get
            {
                if (_okribaDb == null)
                    _okribaDb = new BusinessCreditContext(ConfigurationManager.ConnectionStrings["Okriba_BusinessCreditDbConnectionString"].ConnectionString);
                return _okribaDb;
            }
        }

        private BusinessCreditContext _liloDb;

        public BusinessCreditContext LiloDb
        {
            get
            {
                if (_liloDb == null)
                    _liloDb = new BusinessCreditContext(ConfigurationManager.ConnectionStrings["Lilo_BusinessCreditDbConnectionString"].ConnectionString);
                return _liloDb;
            }
        }

        private BusinessCreditContext _eliavaDb;

        public BusinessCreditContext EliavaDb
        {
            get
            {
                if (_eliavaDb == null)
                    _eliavaDb = new BusinessCreditContext(ConfigurationManager.ConnectionStrings["Eliava_BusinessCreditDbConnectionString"].ConnectionString);
                return _eliavaDb;
            }
        }

        private BusinessCreditContext _vagzaliDb;

        public BusinessCreditContext VagzaliDb
        {
            get
            {
                if (_vagzaliDb == null)
                    _vagzaliDb = new BusinessCreditContext(ConfigurationManager.ConnectionStrings["Vagzali_BusinessCreditDbConnectionString"].ConnectionString);
                return _vagzaliDb;
            }
        }

        private BusinessCreditContext _gugaDb;

        public BusinessCreditContext GugaDb
        {
            get
            {
                if (_gugaDb == null)
                    _gugaDb = new BusinessCreditContext(ConfigurationManager.ConnectionStrings["Central_Guga_BusinessCreditDbConnectionString"].ConnectionString);
                return _gugaDb;
            }
        }

        private BusinessCreditContext _sandroDb;

        public BusinessCreditContext SandroDb
        {
            get
            {
                if (_sandroDb == null)
                    _sandroDb = new BusinessCreditContext(ConfigurationManager.ConnectionStrings["Sandro_Head_BusinessCreditDbConnectionString"].ConnectionString);
                return _sandroDb;
            }
        }

        public ICollection<BusinessCreditContext> Databases
        {
            get
            {
                return new List<BusinessCreditContext>() { CentralDb, IsaniDb, OkribaDb, LiloDb, EliavaDb, VagzaliDb, GugaDb, SandroDb };
            }
        }

        #endregion

        public static int InsertPaymentsInBusinessCreditDb(User user)
        {
            using (var db = new BusinessCreditContext(user.ConnectionString))
            {
                using (var localdb = new AltasoftDailyContext())
                {
                    var data = localdb.DailyPayments.Where(x => x.CalculationDate == DateTime.Today && x.IsOld);
                    foreach (var pmt in data)
                    {
                        if (!pmt.IsOld)
                            continue;

                        Payment payment = null;
                        try
                        {
                            payment = db.Payments.FirstOrDefault(p => p.PaymentDate == pmt.CalculationDate && p.Loan.LoanID == pmt.LoanID);
                        }
                        catch
                        {
                            continue;
                        }
                        var loanId = payment.Loan.LoanID;

                        db.Payments.Remove(payment);
                        db.SaveChanges();

                        var paymentNew = db.Payments.Create();
                        paymentNew.Loan = db.Loans.FirstOrDefault(loan => loan.LoanID == loanId);
                        paymentNew.CurrentPayment = double.Parse(pmt.Payment.ToString());
                        paymentNew.PaymentDate = pmt.CalculationDate;
                        paymentNew.TaxOrderID = pmt.TaxOrderNumber.ToString();
                        paymentNew.CreditExpert = db.CreditExperts.FirstOrDefault();
                        paymentNew.CashCollectionAgent = db.CashCollectionAgents.FirstOrDefault();

                        db.Payments.Add(paymentNew);
                        db.SaveChanges();

                        if (paymentNew.WholeDebt.Value <= 0)
                        {
                            paymentNew.Loan.LoanStatus = LoanStatus.Closed;
                            db.SaveChanges();
                        }
                    }
                    db.SaveChanges();
                }
            }

            return 1;
        }

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

        public static List<DailyPayment> GetDailyByBusinesscreditUser(User user)
        {
            var altasoftdailydb = new AltasoftDailyContext();

            if (string.IsNullOrWhiteSpace(user.ConnectionString))
            {
                return new List<DailyPayment>();
            }

            using (var db = new BusinessCreditContext(user.ConnectionString))
            {
                var result = (from x in db.Payments.ToList()
                              where x.PaymentDate == DateTime.Today
                              select new DailyPayment()
                              {
                                  ClientName = x.Loan.Account.Name + " " + x.Loan.Account.LastName,
                                  FirstName = x.Loan.Account.Name,
                                  LastName = x.Loan.Account.LastName,
                                  ClientNo = x.Loan.Account.AccountID,
                                  StartDate = x.Loan.LoanStartDate.ToShortDateString(),
                                  EndDate = x.Loan.LoanEndDate.ToShortDateString(),
                                  Phone = x.Loan.Account.NumberMobile,
                                  AccruedInterestInGel = decimal.Parse(x.PayableInterest.Value.ToString()),
                                  AgreementNumber = x.Loan.Agreement,
                                  BusinessAddress = x.Loan.Account.BusinessPhysicalAddress,
                                  CalculationDate = x.PaymentDate,
                                  TotalDebtInGel = decimal.Parse(x.WholeDebt.Value.ToString()),
                                  ResponsibleUser = x.Loan.CreditExpert.Name + " " + x.Loan.CreditExpert.LastName,
                                  ProblemManager = x.Loan.ProblemManager,
                                  ProblemManageDate = x.Loan.ProblemManagerDate.HasValue ? x.Loan.ProblemManagerDate.Value.ToShortDateString() : null,
                                  CurrentDebtInGel = decimal.Parse(x.CurrentDebt.Value.ToString()),
                                  Payment = decimal.Parse(x.CurrentPayment.ToString()),
                                  ClientAddressFact = x.Loan.Account.PhysicalAddress,
                                  OverduePrincipalInGel = decimal.Parse(x.CurrentOverduePrincipal.Value.ToString()),
                                  CourtAndEnforcementFee = decimal.Parse(x.EnforcementAndCourtFee.ToString()),
                                  DateOfEnforcement = x.Loan.DateOfEnforcement.HasValue ? x.Loan.DateOfEnforcement.Value.ToShortDateString() : null,
                                  DateOfTheNotificationLetter = x.Loan.LoanNotificationLetter.HasValue ? x.Loan.LoanNotificationLetter.Value.ToString() : null,
                                  PrincipalInGel = decimal.Parse(x.PayablePrincipal.ToString()),
                                  OverdueInterestInGel = decimal.Parse(x.AccruingOverdueInterest.ToString()),
                                  LoanAmountInGel = decimal.Parse(x.Loan.LoanAmount.ToString()),
                                  LoanID = x.Loan.LoanID,
                                  PersonalID = x.Loan.Account.PrivateNumber,
                                  CurrentPrincipalInGel = decimal.Parse((x.LoanBalance == x.AccruingOverduePrincipal ? 0 : (x.LoanBalance - x.AccruingOverduePrincipal > x.PayablePrincipal ? x.PayablePrincipal : x.LoanBalance - x.AccruingOverduePrincipal)).Value.ToString()),
                                  PrincipalPenaltyInGel = decimal.Parse(x.AccruingPenalty.Value.ToString()),
                                  IsOld = true
                              }).ToList();

                foreach (var item in result)
                {
                    item.ClientAccountIban = altasoftdailydb.AccountNumbers.FirstOrDefault(x => x.LoanID == item.LoanID).AccountNumber;
                }

                return result;
            }
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


                loansIds.Where(x => x != 2331).ToList().ForEach(x => data.Add(GetLoanAndDailyModel(x)));

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

            #region Create Item
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

            result.OperatorID = loan.Authorities.FirstOrDefault(x => x.Role == AltasoftAPI.LoansAPI.AuthorityRole.Operator).UserId.Value;
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
                    result.Add(new DailyPaymentIDOrderID() { OrderID = SubmitOrder(item.TaxOrderNumber, "GEL", item.CalculationDate.Date, item.ClientAccountIban, item.Payment, item.AgreementNumber, "09", user.AltasoftUserID, user.DeptID), PaymentID = item.DailyPaymentID });
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

        public static int GetUpdatesByBusinesscreditUser(User user)
        {
            var calcDate = GetCalculationDate();

            List<DailyPayment> result = new List<DailyPayment>();

            using (var db = new AltasoftDailyContext())
            {
                var localPayments = db.DailyPayments.Where(x => x.CalculationDate == calcDate && x.LocalUserID == user.UserID && x.IsOld).ToList();
                var localPaymentsIds = from x in localPayments
                                       select x.LoanID;

                var bcPayments = GetDailyByBusinesscreditUser(user);
                var bcPaymentsIds = from x in bcPayments
                                    select x.LoanID;

                var newPaymentsIds = bcPaymentsIds.Except(localPaymentsIds).ToList();
                var oldPaymentsIds = localPaymentsIds.Except(bcPaymentsIds).ToList();

                var newPayments = bcPayments.Where(x => newPaymentsIds.Contains(x.LoanID));
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


        public static SortableBindingList<Kunchik> GetAccountsTest(DateTime startDate, DateTime endDate, decimal? balCode, int? deptId)
        {
            var list = new Dictionary<List<AccountStatementRecord>, AltasoftAPI.AccountsAPI.Account>();
            var list2 = new List<Kunchik>();

            #region AccountsService
            AltasoftAPI.AccountsAPI.AccountsService a = new AltasoftAPI.AccountsAPI.AccountsService();
            a.RequestHeadersValue = new AltasoftAPI.AccountsAPI.RequestHeaders() { ApplicationKey = "BusinessCreditClient", RequestId = Guid.NewGuid().ToString() };
            #endregion

            var lll = new Dictionary<AccountStatement, AltasoftAPI.AccountsAPI.Account>();

            var result = a.ListAccounts(new ListAccountsQuery()
            {
                ControlFlags = AccountControlFlags.Basic | AccountControlFlags.Balances,
                BalAcc = balCode,
                BalAccSpecified = balCode.HasValue,
                DeptId = deptId,
                DeptIdSpecified = deptId.HasValue
            });

            foreach (var item in result)
            {
                AccountStatement bal = a.GetStatement(new InternalAccountIdentification() { IBAN = item.IBAN, Ccy = item.Ccy }, new Period() { Start = startDate, End = endDate }, true, true, TransactionStatus.Green, true, 0, false);

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
                        AccountNumber = itt.PartnerAccountId.AccountNumber,
                        AccountNumberSpecified = itt.PartnerAccountId.AccountNumberSpecified,
                        Balance = itt.Balance,
                        BalanceEqu = itt.BalanceEqu,
                        BalanceEquSpecified = itt.BalanceEquSpecified,
                        BranchId = itt.PartnerAccountId.BranchId,
                        BranchIdSpecified = itt.PartnerAccountId.BranchIdSpecified,
                        Ccy = itt.PartnerAccountId.Ccy,
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
                        IBAN = itt.PartnerAccountId.IBAN,
                        Id = itt.PartnerAccountId.Id,
                        IdSpecified = itt.PartnerAccountId.IdSpecified,
                        OpCode = itt.OpCode,
                        OrderDate = itt.OrderDate,
                        OrderDateSpecified = itt.OrderDateSpecified,
                        OrderId = itt.OrderId,
                        Purpose = itt.Purpose,
                        Status = itt.Status,
                        TransactionType = itt.TransactionType,
                        AccountName = item.Value.DisplayName.ValueGeo
                    });
                }
            }

            return new SortableBindingList<Kunchik>(list2);

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