using AccountsAPI;
using CustomersAPI;
using LoansAPI;
using OrdersAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Protocols;
using System.Windows.Forms;

namespace AltasoftDaily.Test
{
    public partial class MainForm : Form
    {
        #region Init Services
        private LoansService _loansService;
        public LoansService LoansService
        {
            get
            {
                //if (_loansService == null)
                    _loansService = new LoansService() { RequestHeadersValue = new LoansAPI.RequestHeaders() { ApplicationKey = "appkey", RequestId = "reqid" }, Timeout = int.MaxValue };
                return _loansService;
            }
        }

        private CustomersService _customersService;
        public CustomersService CustomersService
        {
            get
            {
                if (_customersService == null)
                    _customersService = new CustomersService() { RequestHeadersValue = new CustomersAPI.RequestHeaders() { ApplicationKey = "appkey", RequestId = "reqid" } };
                return _customersService;
            }
        }

        private AccountsService _accountsService;
        public AccountsService AccountsService
        {
            get
            {
                if (_accountsService == null)
                    _accountsService = new AccountsService() { RequestHeadersValue = new AccountsAPI.RequestHeaders() { ApplicationKey = "appkey", RequestId = "reqid" } };
                return _accountsService;
            }
        } 
        #endregion

        public MainForm()
        {
            InitializeComponent();
        }

        private object GetDailyByUserId()
        {
            List<DailyModel> list = new List<DailyModel>();

            var loansIds = (from x in LoansService.ListLoans(new ListLoansQuery() { ControlFlags = LoanControlFlags.Basic, Status = new LoanStatus[] { LoanStatus.Overdue }, })
                            select x.Id.Value).ToList().OrderBy(x => x);

            var apps = LoansService.ListApplications(new ListApplicationsQuery() { ControlFlags = ApplicationControlFlags.Basic, Status = new ApplicationStatus[] { ApplicationStatus.Approved } });

            foreach (var loanId in loansIds)
            {
                var loan = LoansService.GetLoan(LoanControlFlags.Debts | LoanControlFlags.Basic | LoanControlFlags.Authorities, true, loanId, true);
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
                    CurrentPrincipalInGel = loan.Debts.Where(x => x.Name == ("principal")).Sum(x => x.Amount),
                    ResponsibleUser = loan.Authorities.FirstOrDefault(x => x.Role == AuthorityRole.Operator).Name
                };

                item.CalculationDate = loan.CalcDate.Value.ToShortDateString();
                item.LoanAmountInGel = loan.Amount.Amount;
                item.LoanCCY = loan.Amount.Ccy;
                item.ClientNo = loan.BorrowerId.Value;
                item.AgreementNumber = loan.AgreementNo;
                item.StartDate = loan.Term.Start.ToShortDateString();
                item.EndDate = loan.Term.End.ToShortDateString();

                var customer = CustomersService.GetCustomer(CustomerControlFlags.Basic | CustomerControlFlags.Extensions | CustomerControlFlags.Addresses, true, loan.BorrowerId.Value, true);
 
                var account = AccountsService.GetAccount(AccountControlFlags.Basic, true, new AccountsAPI.InternalAccountIdentification() { Id = loan.AccountIdentifier, IdSpecified = true }, item.LoanCCY);

                item.Phone = customer.ContactInfo.MobilePhone;
                item.ClientAccountDescrip = account.DisplayName.ValueGeo;
                item.ClientName = customer.Name.ValueGeo;
                item.FirstName = (customer.Entity as IndividualEntity).Name.FirstName.ValueGeo;
                item.LastName = (customer.Entity as IndividualEntity).Name.LastName.ValueGeo;
                item.PersonalID = (customer.Entity as IndividualEntity).PIN;
                item.ClientAccountBranchCode = customer.BranchId.Value.ToString();
                item.ClientAccountIban = account.IBAN;

                customer = CustomersService.GetCustomer(CustomerControlFlags.Addresses, true, loan.BorrowerId.Value, true);
                item.ClientAddressFact = customer.AddressActual.Value.ValueGeo;


                item.NextScheduledPaymentInGel = LoansService.GetLoanSchedule(DebtComponentDetalization.Detailed, true, GetLoanScheduleControlFlags.Full, true, item.LoanID, true).FirstOrDefault(x => x.Date == DateTime.Today) != null ?
                    LoansService.GetLoanSchedule(DebtComponentDetalization.Detailed, true, GetLoanScheduleControlFlags.Full, true, item.LoanID, true).FirstOrDefault(x => x.Date == DateTime.Today).Elements.Where(x => x.Name != "balance").Sum(x => x.Amount) : 0;

                LoansAPI.Application app;
                bool? notm;
                bool notms;
                LoansService.GetApplication(ApplicationControlFlags.ExtraFields, true, loan.Id.Value, true, out notm, out notms, out app);

                item.BusinessAddress = app.Businesses.FirstOrDefault().Address;

                list.Add(item);
            }

            return list.OrderBy(x => x.ClientNo).ToList();
        }

        private object GetDaily()
        {
            List<DailyModel> list = new List<DailyModel>();

            var loansIds = (from x in LoansService.ListLoans(new ListLoansQuery() { ControlFlags = LoanControlFlags.Basic, Status = new LoanStatus[] { LoanStatus.Overdue },  })
                            select x.Id.Value).ToList().OrderBy(x => x);

            var l = LoansService.ListLoans(new ListLoansQuery() { ControlFlags = LoanControlFlags.Basic, Status = new LoanStatus[] { LoanStatus.Overdue } });

            foreach (var loanId in loansIds)
            {//01024071605
                var loan = LoansService.GetLoan(LoanControlFlags.Debts, true, loanId, true);
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


                loan = LoansService.GetLoan(LoanControlFlags.Basic, true, loanId, true);

                item.CalculationDate = loan.CalcDate.Value.ToShortDateString();
                item.LoanAmountInGel = loan.Amount.Amount;
                item.LoanCCY = loan.Amount.Ccy;
                item.ClientNo = loan.BorrowerId.Value;
                item.AgreementNumber = loan.AgreementNo;
                item.StartDate = loan.Term.Start.ToShortDateString();
                item.EndDate = loan.Term.End.ToShortDateString();

                var customer = CustomersService.GetCustomer(CustomerControlFlags.Basic, true, loan.BorrowerId.Value, true);
                Customer customer2;
                var account = AccountsService.GetAccount(AccountControlFlags.Basic, true, new AccountsAPI.InternalAccountIdentification() { Id = loan.AccountIdentifier, IdSpecified = true }, item.LoanCCY);

                customer2 = CustomersService.GetCustomer(CustomerControlFlags.Extensions, true, loan.BorrowerId.Value, true);
                item.Phone = customer2.ContactInfo.MobilePhone;
                item.ClientAccountDescrip = account.DisplayName.ValueGeo;
                item.ClientName = customer.Name.ValueGeo;
                item.FirstName = (customer.Entity as IndividualEntity).Name.FirstName.ValueGeo;
                item.LastName = (customer.Entity as IndividualEntity).Name.LastName.ValueGeo;
                item.PersonalID = (customer.Entity as IndividualEntity).PIN;
                item.ClientAccountBranchCode = customer.BranchId.Value.ToString();
                item.ClientAccountIban = account.IBAN;

                Customer customer3;

                customer3 = CustomersService.GetCustomer(CustomerControlFlags.Addresses, true, loan.BorrowerId.Value, true);
                item.ClientAddressFact = customer3.AddressActual.Value.ValueGeo;


                item.NextScheduledPaymentInGel = LoansService.GetLoanSchedule(DebtComponentDetalization.Detailed, true, GetLoanScheduleControlFlags.Full, true, item.LoanID, true).FirstOrDefault(x => x.Date == DateTime.Today) != null ?
                    LoansService.GetLoanSchedule(DebtComponentDetalization.Detailed, true, GetLoanScheduleControlFlags.Full, true, item.LoanID, true).FirstOrDefault(x => x.Date == DateTime.Today).Elements.Where(x => x.Name != "balance").Sum(x => x.Amount) : 0;

                LoansAPI.Application app;
                bool? notm;
                bool notms;
                LoansService.GetApplication(ApplicationControlFlags.ExtraFields, true, loan.Id.Value, true, out notm, out notms, out app);

                item.BusinessAddress = app.Businesses.FirstOrDefault().Address;

                list.Add(item);
            }

            return list.OrderBy(x => x.ClientNo).ToList();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            dataGrid.DataSource = GetDailyByUserId();
            foreach (DataGridViewColumn item in dataGrid.Columns)
            {
                if (item.Name != "Payment")
                {
                    item.ReadOnly = true;
                }
            }
        }

        private void dataGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var data = ((List<DailyModel>)dataGrid.DataSource).Where(x => x.Payment > 0);

            string message = "starting count: " + data.Count() + "\n";
            message += "ids: ";
            foreach (var item in data)
            {
                message += Add(0, item.LoanCCY, DateTime.Parse(item.CalculationDate), item.ClientAccountIban, item.Payment, "sesxis dafarva MainForm", "09", 21) + "\n";
            }

            MessageBox.Show(message);
        }

        private long? Add(int docNum, string ccy, DateTime date, string accountIBAN, decimal amount, string purpose, string cashDeskSymbol, int userId)
        {
            #region OrdersService
            OrdersService o = new OrdersService();
            o.RequestHeadersValue = new OrdersAPI.RequestHeaders() { ApplicationKey = "BusinessCreditClient",    RequestId = Guid.NewGuid().ToString()};
            #endregion

            #region CustomersService
            CustomersService c = new CustomersService();
            c.RequestHeadersValue = new CustomersAPI.RequestHeaders() { ApplicationKey = "BusinessCreditClient", RequestId = Guid.NewGuid().ToString() };
            #endregion

            #region CustomersService
            AccountsService a = new AccountsService();
            a.RequestHeadersValue = new AccountsAPI.RequestHeaders() { ApplicationKey = "BusinessCreditClient",  RequestId = Guid.NewGuid().ToString() };
            #endregion

            var acc = a.GetAccount(AccountControlFlags.Basic, true, new AccountsAPI.InternalAccountIdentification() { IBAN = accountIBAN }, ccy);
            var cus = c.GetCustomer(CustomerControlFlags.Basic, true, acc.CustomerId.Value, true);
            var cusEntity = cus.Entity as IndividualEntity;


            #region Variables
            long id;
            bool specified;
            #endregion

            #region Order Init
            var Order = new CashOrderData
            {
                Amount = new OrdersAPI.AmountAndCurrency { Amount = amount, Ccy = ccy },
                Date = date,
                Status = OrdersAPI.TransactionStatus.Green,
                StatusSpecified = true,
                TransactionCode = "qwe34242342", //09 
                OpCode = "120",
                Purpose = purpose,
                //ExtraAccount = 0,
                //ExtraAccountSpecified = false,
                CustomerAccount = new OrdersAPI.InternalAccountIdentification { IBAN = accountIBAN },
                OrderDate = date,
                OrderDateSpecified = true,
                Type = CashOrderType.Deposit,
                Customer = new CustomerData { Name = (OrdersAPI.PersonName)cusEntity.Name },
                //DeptId = 5,
                //DeptIdSpecified = true,
                DocNum = docNum,
                DocNumSpecified = docNum == 0 ? false : true
            };

            cusEntity = (c.GetCustomer(CustomerControlFlags.IdentityDocuments, true, acc.CustomerId.Value, true).Entity as IndividualEntity);

            Order.Customer.IdentityDocument = (OrdersAPI.IdentityDocument)cusEntity.IdentityDocuments[0];
            #endregion

            #region Put Order
            o.PutOrder(new OrdersAPI.UserAndDeptId()
            {
                DeptId = 2,
                DeptIdSpecified = true,
                UserIdentification = new OrdersAPI.UserIdentification() { Id = 21, IdSpecified = true }
            }, 0, false,
                   new Guid().ToString(),
                   true, true, false, true, Order, out id, out specified);
            #endregion

            return id;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ნამდვილად გსურთ გაუქმება?", "გაუქმება", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Close();
        }

        private void გამოსვლაToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
