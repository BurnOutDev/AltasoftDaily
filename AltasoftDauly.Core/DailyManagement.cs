using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AltasoftDaily.Domain;

namespace AltasoftDaily.Core
{
    public class DailyManagement
    {
        public static List<DailyModel> GetDailyByDeptId(int deptId)
        {
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

            List<DailyModel> list = new List<DailyModel>();

            var loansIds = (from x in l.ListLoans(new AltasoftAPI.LoansAPI.ListLoansQuery() { ControlFlags = AltasoftAPI.LoansAPI.LoanControlFlags.Basic, Status = new AltasoftAPI.LoansAPI.LoanStatus[] { AltasoftAPI.LoansAPI.LoanStatus.Overdue, AltasoftAPI.LoansAPI.LoanStatus.Current, AltasoftAPI.LoansAPI.LoanStatus.Late } })
                            select x.Id.Value).ToList().OrderBy(x => x);

            //var l1 = l.ListLoans(new AltasoftAPI.LoansAPI.ListLoansQuery() { ControlFlags = AltasoftAPI.LoansAPI.LoanControlFlags.Basic, Status = new AltasoftAPI.LoansAPI.LoanStatus[] { AltasoftAPI.LoansAPI.LoanStatus.Overdue } });

            foreach (var loanId in loansIds)
            {
                var loan = l.GetLoan(AltasoftAPI.LoansAPI.LoanControlFlags.Authorities | AltasoftAPI.LoansAPI.LoanControlFlags.Debts | AltasoftAPI.LoansAPI.LoanControlFlags.Basic, true, loanId, true);

                if (loan.BranchId == deptId)
                {
                    var item = new DailyModel()
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

                    item.CalculationDate = loan.CalcDate.Value.ToShortDateString();
                    item.LoanAmountInGel = loan.Amount.Amount;
                    item.LoanCCY = loan.Amount.Ccy;
                    item.ClientNo = loan.BorrowerId.Value;
                    item.AgreementNumber = loan.AgreementNo;
                    item.StartDate = loan.Term.Start.ToShortDateString();
                    item.EndDate = loan.Term.End.ToShortDateString();

                    if (item.ClientNo == 661)
                        continue;

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

            return list.OrderBy(x => x.ClientNo).ToList();
        }

        public static long? Add(int docNum, string ccy, DateTime date, string accountIBAN, decimal amount, string purpose, string cashDeskSymbol, int userId, int deptId)
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
            var cus = c.GetCustomer(AltasoftAPI.CustomersAPI.CustomerControlFlags.Basic, true, acc.CustomerId.Value, true);
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
                OpCode = "120",
                Purpose = purpose,
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

            cusEntity = (c.GetCustomer(AltasoftAPI.CustomersAPI.CustomerControlFlags.IdentityDocuments, true, acc.CustomerId.Value, true).Entity as AltasoftAPI.CustomersAPI.IndividualEntity);

            Order.Customer.IdentityDocument = (AltasoftAPI.OrdersAPI.IdentityDocument)cusEntity.IdentityDocuments[0];
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
    }
}
