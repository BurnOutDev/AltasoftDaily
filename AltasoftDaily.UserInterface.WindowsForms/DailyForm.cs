using AltasoftDaily.Core;
using AltasoftDaily.Domain;
using AltasoftDaily.Domain.POCO;
using log4net;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AltasoftDaily.UserInterface.WindowsForms
{
    public partial class DailyForm : MetroForm
    {
        private readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
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

        public DailyForm(User user)
        {
            User = user;
            InitializeComponent();
        }

        private void DailyForm_Load(object sender, EventArgs e)
        {
            try
            {
                var calcDate = new DateTime(2015, 9, 27);

                button1.Enabled = User.CanSubmit;

                DailyManagement.GetUpdatesByAltasoftUser(User);
                gridDaily.DataSource = db.DailyPayments.Where(x => x.CalculationDate == calcDate && x.LocalUserID == User.UserID).ToList();

                LoadingForm = new LoadingForm();
                LoadingForm.Show();
            }
            catch (Exception ex)
            {
                log.Error("DailyForm Loading Error:", ex);
                throw;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DailyManagement.UpdatePaymentsInDaily(((List<DailyPayment>)gridDaily.DataSource).ToList());
            }
            catch (Exception ex)
            {
                log.Error("Error Updating Payments In Daily.", ex);
                throw;
            }
            log.Info("Daily Updated Successfuly.");
        }

        private void DailyForm_Shown(object sender, EventArgs e)
        {
            if (LoadingForm != null)
                LoadingForm.Close();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            var list = (List<DailyPayment>)gridDaily.DataSource;
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

            TaxOrderGenerator.Generate(@"C:\Users\Irakl\Desktop\TaxOrderTemplate.xlsx", orders.ToArray());
        }

        private void btnStats_Click(object sender, EventArgs e)
        {
            var data = (List<DailyPayment>)gridDaily.DataSource;
            var resData = new List<DailyStats>();


            resData.Add(
                new DailyStats()
                {
                    Name = "NextScheduledPaymentInGel",
                    Sum = data.Sum(x => x.NextScheduledPaymentInGel),
                    Average = data.Average(x => x.NextScheduledPaymentInGel),
                    Maximum = data.Max(x => x.NextScheduledPaymentInGel),
                    Minimum = data.Min(x => x.NextScheduledPaymentInGel)
                });

            resData.Add(
                new DailyStats()
                {
                    Name = "CurrentDebtInGel",
                    Sum = data.Sum(x => x.CurrentDebtInGel),
                    Average = data.Average(x => x.CurrentDebtInGel),
                    Maximum = data.Max(x => x.CurrentDebtInGel),
                    Minimum = data.Min(x => x.CurrentDebtInGel)
                });

            resData.Add(
                new DailyStats()
                {
                    Name = "TotalDebtInGel",
                    Sum = data.Sum(x => x.TotalDebtInGel),
                    Average = data.Average(x => x.TotalDebtInGel),
                    Maximum = data.Max(x => x.TotalDebtInGel),
                    Minimum = data.Min(x => x.TotalDebtInGel)
                });

            resData.Add(
                new DailyStats()
                {
                    Name = "Payment",
                    Sum = data.Sum(x => x.Payment),
                    Average = data.Average(x => x.Payment),
                    Maximum = data.Max(x => x.Payment),
                    Minimum = data.Min(x => x.Payment)
                });

            resData.Add(
                new DailyStats()
                {
                    Name = "CourtAndEnforcementFee",
                    Sum = data.Sum(x => x.CourtAndEnforcementFee),
                    Average = data.Average(x => x.CourtAndEnforcementFee),
                    Maximum = data.Max(x => x.CourtAndEnforcementFee),
                    Minimum = data.Min(x => x.CourtAndEnforcementFee)
                });

            resData.Add(
                new DailyStats()
                {
                    Name = "InterestPenaltyInGel",
                    Sum = data.Sum(x => x.InterestPenaltyInGel),
                    Average = data.Average(x => x.InterestPenaltyInGel),
                    Maximum = data.Max(x => x.InterestPenaltyInGel),
                    Minimum = data.Min(x => x.InterestPenaltyInGel)
                });

            resData.Add(
                new DailyStats()
                {
                    Name = "PrincipalPenaltyInGel",
                    Sum = data.Sum(x => x.PrincipalPenaltyInGel),
                    Average = data.Average(x => x.PrincipalPenaltyInGel),
                    Maximum = data.Max(x => x.PrincipalPenaltyInGel),
                    Minimum = data.Min(x => x.PrincipalPenaltyInGel)
                });

            resData.Add(
                new DailyStats()
                {
                    Name = "OverdueInterestInGel",
                    Sum = data.Sum(x => x.OverdueInterestInGel),
                    Average = data.Average(x => x.OverdueInterestInGel),
                    Maximum = data.Max(x => x.OverdueInterestInGel),
                    Minimum = data.Min(x => x.OverdueInterestInGel)
                });

            resData.Add(
                new DailyStats()
                {
                    Name = "AccruedInterestInGel",
                    Sum = data.Sum(x => x.AccruedInterestInGel),
                    Average = data.Average(x => x.AccruedInterestInGel),
                    Maximum = data.Max(x => x.AccruedInterestInGel),
                    Minimum = data.Min(x => x.AccruedInterestInGel)
                });

            resData.Add(
                new DailyStats()
                {
                    Name = "OverduePrincipalInGel",
                    Sum = data.Sum(x => x.OverduePrincipalInGel),
                    Average = data.Average(x => x.OverduePrincipalInGel),
                    Maximum = data.Max(x => x.OverduePrincipalInGel),
                    Minimum = data.Min(x => x.OverduePrincipalInGel)
                });

            resData.Add(
                new DailyStats()
                {
                    Name = "CurrentPrincipalInGel",
                    Sum = data.Sum(x => x.CurrentPrincipalInGel),
                    Average = data.Average(x => x.CurrentPrincipalInGel),
                    Maximum = data.Max(x => x.CurrentPrincipalInGel),
                    Minimum = data.Min(x => x.CurrentPrincipalInGel)
                });

            resData.Add(
                new DailyStats()
                {
                    Name = "PrincipalInGel",
                    Sum = data.Sum(x => x.PrincipalInGel),
                    Average = data.Average(x => x.PrincipalInGel),
                    Maximum = data.Max(x => x.PrincipalInGel),
                    Minimum = data.Min(x => x.PrincipalInGel)
                });

            resData.Add(
                new DailyStats()
                {
                    Name = "LoanAmountInGel",
                    Sum = data.Sum(x => x.LoanAmountInGel),
                    Average = data.Average(x => x.LoanAmountInGel),
                    Maximum = data.Max(x => x.LoanAmountInGel),
                    Minimum = data.Min(x => x.LoanAmountInGel)
                });

            var frmStats = new StatsForm(resData);
            frmStats.Show();
        }

        private void gridDaily_SelectionChanged(object sender, EventArgs e)
        {
            var value = 0M;
            foreach (dynamic item in gridDaily.SelectedCells)
            {
                try
                {
                    value += (decimal)item.Value;
                }
                catch
                {
                    lblSum.Text = "";
                    return;
                }
            }

            lblSum.Text = value.ToString();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            DailyManagement.SubmitOrdersFromDatabase(User);
        }

        private void gridDaily_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var count = 0;
            ((List<DailyPayment>)gridDaily.DataSource).Where(x => x.Payment > 0).ToList().ForEach(x => count++);

            lblCount.Text = count.ToString();
        }

        private void btnStats_Click_1(object sender, EventArgs e)
        {
            TaxOrderGenerator.ExportToExcel(ConvertToExcelPayment((List<DailyPayment>)gridDaily.DataSource));
        }

        public static List<ExcelPayment> ConvertToExcelPayment(List<DailyPayment> v)
        {
            var list = new List<ExcelPayment>();
            v.ForEach(x => list.Add((ExcelPayment)x));
            return list;
        }
    }
}
