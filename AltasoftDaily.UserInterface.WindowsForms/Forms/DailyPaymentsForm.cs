using AltasoftDaily.Core;
using AltasoftDaily.Domain;
using AltasoftDaily.Domain.POCO;
using AltasoftDaily.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AltasoftDaily.UserInterface.WindowsForms
{
    public partial class DailyPaymentsForm : GridBaseForm
    {
        private AltasoftDailyContext _db;
        public AltasoftDailyContext db
        {
            get
            {
                if (_db == null)
                    _db = new AltasoftDailyContext();
                return _db;
            }
        }
        public User User { get; set; }
        public int DeptId { get; set; }
        public LoadingForm LoadingForm { get; set; }
        public DailyPaymentsForm(User user)
        {
            User = user;
            InitializeComponent();
        }

        private void GG_Load(object sender, EventArgs e)
        {
            try
            {
                var calcDate = DailyManagement.GetCalculationDate();

                DailyManagement.GetUpdatesByAltasoftUser(User);

                var payments = db.DailyPayments.Where(x => x.CalculationDate == calcDate && x.LocalUserID == User.UserID).ToList();
                payments.AddRange(DailyManagement.GetDailyByDeptId() as IEnumerable<DailyPayment>);
                gridData.DataSource = new SortableBindingList<DailyPayment>(payments);

                foreach (DataGridViewTextBoxColumn col in gridData.Columns)
                {
                    if (col.Name == "Payment")
                        continue;

                    col.ReadOnly = true;
                }

                LoadingForm = new LoadingForm();
                LoadingForm.Show();
            }
            catch (Exception ex)
            {
                LoggingManagement.LogException(ex, User);
                throw;
            }
        }

        private void pbxSave_Click(object sender, EventArgs e)
        {
            try
            {
                DailyManagement.UpdatePaymentsInDaily(((SortableBindingList<DailyPayment>)gridData.DataSource).ToList());
            }
            catch (Exception ex)
            {
                LoggingManagement.LogException(ex, User);
                throw;
            }
            MessageBox.Show("წარმატებით შეინახა.");
        }

        private void pbxFilter_Click(object sender, EventArgs e)
        {

        }

        private void pbxExport_Click(object sender, EventArgs e)
        {
            TaxOrderGenerator.ExportToExcel(ConvertToExcelPayment((SortableBindingList<DailyPayment>)gridData.DataSource));
        }

        private void GG_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("ნამდვილად გსურთ დახურვა?", "დახურვა", MessageBoxButtons.YesNo) == DialogResult.No)
                e.Cancel = true;
        }

        public static SortableBindingList<ExcelPayment> ConvertToExcelPayment(SortableBindingList<DailyPayment> v)
        {
            var list = new List<ExcelPayment>();
            v.ToList().ForEach(x => list.Add((ExcelPayment)x));
            return new SortableBindingList<ExcelPayment>(list);
        }

        private void pbxOrders_Click(object sender, EventArgs e)
        {
            var list = ((SortableBindingList<DailyPayment>)gridData.DataSource).ToList();
            List<TaxOrder> orders = new List<TaxOrder>();
            int count = 1;
            foreach (var item in list)
            {
                orders.Add(new TaxOrder()
                {
                    Date = item.CalculationDate.ToShortDateString(),
                    TaxOrderID = item.TaxOrderNumber,
                    TaxOrderNumber = item.TaxOrderNumber,
                    AccountFirstName = item.FirstName,
                    AccountLastName = item.LastName,
                    AccountPrivateNumber = item.PersonalID,
                    Basis = "სესხის დაფარვა სესხის ხელშ. " + item.AgreementNumber + "-ის საფუძველზე",
                    CollectorFirstName = User.Name,
                    CollectorLastName = User.LastName,
                    CollectorPrivateNumber = User.PrivateNumber
                });
                count++;
            }

            TaxOrderGenerator.Generate(Path.Combine(Environment.CurrentDirectory, "TaxOrderTemplate.xlsx"), orders.ToArray());
        }

        private void pbxStats_Click(object sender, EventArgs e)
        {
            var data = (SortableBindingList<DailyPayment>)gridData.DataSource;
            var resData = new List<DailyStats>();

            resData.Add(
                new DailyStats()
                {
                    Name = "NextScheduledPaymentInGel",
                    Sum = data.Sum(x => x.NextScheduledPaymentInGel),
                    Average = Math.Round(data.Average(x => x.NextScheduledPaymentInGel), 2),
                    Maximum = data.Max(x => x.NextScheduledPaymentInGel),
                    Minimum = data.Min(x => x.NextScheduledPaymentInGel)
                });

            resData.Add(
                new DailyStats()
                {
                    Name = "CurrentDebtInGel",
                    Sum = data.Sum(x => x.CurrentDebtInGel),
                    Average = Math.Round(data.Average(x => x.CurrentDebtInGel), 2),
                    Maximum = data.Max(x => x.CurrentDebtInGel),
                    Minimum = data.Min(x => x.CurrentDebtInGel)
                });

            resData.Add(
                new DailyStats()
                {
                    Name = "TotalDebtInGel",
                    Sum = data.Sum(x => x.TotalDebtInGel),
                    Average = Math.Round(data.Average(x => x.TotalDebtInGel), 2),
                    Maximum = data.Max(x => x.TotalDebtInGel),
                    Minimum = data.Min(x => x.TotalDebtInGel)
                });

            resData.Add(
                new DailyStats()
                {
                    Name = "Payment",
                    Sum = data.Sum(x => x.Payment),
                    Average = Math.Round(data.Average(x => x.Payment), 2),
                    Maximum = data.Max(x => x.Payment),
                    Minimum = data.Min(x => x.Payment)
                });

            resData.Add(
                new DailyStats()
                {
                    Name = "CourtAndEnforcementFee",
                    Sum = data.Sum(x => x.CourtAndEnforcementFee),
                    Average = Math.Round(data.Average(x => x.CourtAndEnforcementFee), 2),
                    Maximum = data.Max(x => x.CourtAndEnforcementFee),
                    Minimum = data.Min(x => x.CourtAndEnforcementFee)
                });

            resData.Add(
                new DailyStats()
                {
                    Name = "InterestPenaltyInGel",
                    Sum = data.Sum(x => x.InterestPenaltyInGel),
                    Average = Math.Round(data.Average(x => x.InterestPenaltyInGel), 2),
                    Maximum = data.Max(x => x.InterestPenaltyInGel),
                    Minimum = data.Min(x => x.InterestPenaltyInGel)
                });

            resData.Add(
                new DailyStats()
                {
                    Name = "PrincipalPenaltyInGel",
                    Sum = data.Sum(x => x.PrincipalPenaltyInGel),
                    Average = Math.Round(data.Average(x => x.PrincipalPenaltyInGel), 2),
                    Maximum = data.Max(x => x.PrincipalPenaltyInGel),
                    Minimum = data.Min(x => x.PrincipalPenaltyInGel)
                });

            resData.Add(
                new DailyStats()
                {
                    Name = "OverdueInterestInGel",
                    Sum = data.Sum(x => x.OverdueInterestInGel),
                    Average = Math.Round(data.Average(x => x.OverdueInterestInGel), 2),
                    Maximum = data.Max(x => x.OverdueInterestInGel),
                    Minimum = data.Min(x => x.OverdueInterestInGel)
                });

            resData.Add(
                new DailyStats()
                {
                    Name = "AccruedInterestInGel",
                    Sum = data.Sum(x => x.AccruedInterestInGel),
                    Average = Math.Round(data.Average(x => x.AccruedInterestInGel), 2),
                    Maximum = data.Max(x => x.AccruedInterestInGel),
                    Minimum = data.Min(x => x.AccruedInterestInGel)
                });

            resData.Add(
                new DailyStats()
                {
                    Name = "OverduePrincipalInGel",
                    Sum = data.Sum(x => x.OverduePrincipalInGel),
                    Average = Math.Round(data.Average(x => x.OverduePrincipalInGel), 2),
                    Maximum = data.Max(x => x.OverduePrincipalInGel),
                    Minimum = data.Min(x => x.OverduePrincipalInGel)
                });

            resData.Add(
                new DailyStats()
                {
                    Name = "CurrentPrincipalInGel",
                    Sum = data.Sum(x => x.CurrentPrincipalInGel),
                    Average = Math.Round(data.Average(x => x.CurrentPrincipalInGel), 2),
                    Maximum = data.Max(x => x.CurrentPrincipalInGel),
                    Minimum = data.Min(x => x.CurrentPrincipalInGel)
                });

            resData.Add(
                new DailyStats()
                {
                    Name = "PrincipalInGel",
                    Sum = data.Sum(x => x.PrincipalInGel),
                    Average = Math.Round(data.Average(x => x.PrincipalInGel), 2),
                    Maximum = data.Max(x => x.PrincipalInGel),
                    Minimum = data.Min(x => x.PrincipalInGel)
                });

            resData.Add(
                new DailyStats()
                {
                    Name = "LoanAmountInGel",
                    Sum = data.Sum(x => x.LoanAmountInGel),
                    Average = Math.Round(data.Average(x => x.LoanAmountInGel), 2),
                    Maximum = data.Max(x => x.LoanAmountInGel),
                    Minimum = data.Min(x => x.LoanAmountInGel)
                });

            var frmStats = new StatsForm(resData);
            frmStats.Show();
        }

        private void pbxUpload_Click(object sender, EventArgs e)
        {
            pbxSave_Click(sender, e);

            try
            {
                List<DailyPaymentIDOrderID> result = new List<DailyPaymentIDOrderID>();

                if (MessageBox.Show("ნამდვილად გსურთ ატვირთვა?", "ატვირთვა", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    result = DailyManagement.SubmitOrdersFromDatabase(User);

                foreach (var item in result)
                {
                    var payment = db.DailyPayments.FirstOrDefault(x => x.DailyPaymentID == item.PaymentID);
                    payment.OrderID = item.OrderID;
                    LoggingManagement.LogOrder(payment, User);
                }

                db.SaveChanges();

                MessageBox.Show(string.Format("წარმატებით აიტვირთა {0} გადახდა.", result.Count));
            }
            catch (Exception ex)
            {
                LoggingManagement.LogException(ex, User);
                throw;
            }
        }

        private void GG_Shown(object sender, EventArgs e)
        {
            if (LoadingForm != null)
                LoadingForm.Close();
        }

        public override void gridData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //var form = new ViewCollateralsForm(int.Parse(gridData.Rows[e.RowIndex].Cells["LoanID"].Value.ToString()));
            //form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (gridData.AreAllCellsSelected(false))
            {
                
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            new ViewCollateralsForm(int.Parse(gridData.Rows[gridData.SelectedCells[0].RowIndex].Cells["LoanID"].Value.ToString())).Show();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public override void gridData_SelectionChanged(object sender, EventArgs e)
        {
            base.gridData_SelectionChanged(sender, e);

            if (MultipleRowsSelected)
                pbxGuarantors.Enabled = false;
            else
                pbxGuarantors.Enabled = true;
        }
    }
}
