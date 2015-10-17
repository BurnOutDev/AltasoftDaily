using AccountsAPI;
using CustomersAPI;
using MetroFramework.Forms;
using OrdersAPI;
using System;
using System.Windows.Forms;

namespace AltasoftDaily.Test
{
    public partial class Home : MetroForm
    {
        public Home()
        {
            InitializeComponent();
            dtpDate.Value = DateTime.Now;
            cbxCcy.SelectedIndex = 0;
            cbxCashDeskSymbol.SelectedIndex = 0;
            tbxPurpose.Text = "sesxis dafarva programidan";
            tbxAccountNumber.Text = "GE36AL0200000018010213";
        }

        private void Home_Click(object sender, EventArgs e)
        {

        }

        private long? Add(int docNum, string ccy, DateTime date, string accountIBAN, decimal amount, string purpose, string cashDeskSymbol, int userId)
        {
            #region OrdersService
            OrdersService o = new OrdersService();
            o.RequestHeadersValue = new OrdersAPI.RequestHeaders() { ApplicationKey = "BusinessCreditClient", RequestId = DateTime.Now.ToLongTimeString() };
            #endregion

            #region CustomersService
            CustomersService c = new CustomersService();
            c.RequestHeadersValue = new CustomersAPI.RequestHeaders() { ApplicationKey = "BusinessCreditClient", RequestId = DateTime.Now.ToLongTimeString() };
            #endregion

            #region CustomersService
            AccountsService a = new AccountsService();
            a.RequestHeadersValue = new AccountsAPI.RequestHeaders() { ApplicationKey = "BusinessCreditClient", RequestId = DateTime.Now.ToLongTimeString() };
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
                DocNum = 555847,
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Add(
                int.Parse(tbxDocNum.Text),
                cbxCcy.Text,
                dtpDate.Value,
                tbxAccountNumber.Text,
                decimal.Parse(tbxAmount.Text),
                tbxPurpose.Text,
                cbxCashDeskSymbol.Text,
                21
                );
        }

        private void Home_Load(object sender, EventArgs e)
        {
            dtpDate.Value = DateTime.Today;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
