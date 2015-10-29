using AltasoftDaily.Core;
using AltasoftDaily.Domain.POCO;
using MetroFramework.Forms;
using System.Linq;
using System;
using System.Windows.Forms;

namespace AltasoftDaily.UserInterface.WindowsForms
{
    public partial class SingleOrderForm : MetroForm
    {
        public DailyPayment Payment { get; set; }
        public User User { get; set; }

        public SingleOrderForm(DailyPayment payment, User user)
        {
            InitializeComponent();
            Payment = payment;
            User = user;

            dtpDate.Value = Payment.CalculationDate;

            cbxCcy.SelectedIndex = 0;
            cbxCashDeskSymbol.SelectedIndex = 0;
        }

        private void Home_Load(object sender, EventArgs e)
        {
            dtpDate.Value = Payment.CalculationDate;
            tbxAccountNumber.Text = Payment.ClientAccountIban;
            tbxAccountName.Text = Payment.ClientName;
            tbxDocNum.Text = Payment.TaxOrderNumber.ToString();
            tbxPurpose.Text = "სესხის დაფარვა სესხის ხელშ. " + Payment.AgreementNumber + "-ის საფუძველზე";
            tbxCashDesk.Text = DailyManagement.GetAccountIbanByDept(User.DeptID);
            cbxCashDesk.SelectedIndex = User.DeptID;
            cbxBranch.SelectedIndex = User.DeptID;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var db = new AltasoftDailyContext())
            {
                db.DailyPayments.FirstOrDefault(x => x.DailyPaymentID == Payment.DailyPaymentID).Payment = Payment.Payment;
                db.SaveChanges();
            }
            DailyManagement.SubmitOrder(Payment.TaxOrderNumber, Payment.LoanCCY, Payment.CalculationDate, Payment.ClientAccountIban, decimal.Parse(tbxAmount.Text), Payment.AgreementNumber, "120", User.AltasoftUserID, User.DeptID);
        }
    }
}
