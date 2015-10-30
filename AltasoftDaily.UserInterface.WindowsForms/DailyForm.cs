using AltasoftDaily.Core;
using AltasoftDaily.Domain;
using AltasoftDaily.Domain.POCO;
using AltasoftDaily.Helpers;
using log4net;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

                DailyManagement.GetUpdatesByAltasoftUser(User);
                gridDaily.DataSource = new SortableBindingList<DailyPayment>(db.DailyPayments.Where(x => x.CalculationDate == calcDate && x.LocalUserID == User.UserID).ToList());

                foreach (DataGridViewTextBoxColumn col in gridDaily.Columns)
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
                log.Error("DailyForm Loading Error:", ex);
                throw;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DailyManagement.UpdatePaymentsInDaily(((SortableBindingList<DailyPayment>)gridDaily.DataSource).ToList());
            }
            catch (Exception ex)
            {
                log.Error("Error Updating Payments In Daily.", ex);
                throw;
            }
            log.Info("Daily Updated Successfuly.");
            MessageBox.Show("წარმატებით შეინახა.");
        }

        private void DailyForm_Shown(object sender, EventArgs e)
        {
            if (LoadingForm != null)
                LoadingForm.Close();

            int count = 0;
            ((SortableBindingList<DailyPayment>)gridDaily.DataSource).Where(x => x.Payment > 0).ToList().ForEach(x => count++);
            lblPmtCount.Text = count.ToString();

            decimal sum = 0;
            ((SortableBindingList<DailyPayment>)gridDaily.DataSource).Where(x => x.Payment > 0).ToList().ForEach(x => sum += x.Payment);
            lblPmtSum.Text = Math.Round(sum, 2).ToString();

            gridDaily.Columns["Payment"].DefaultCellStyle.BackColor = Color.Yellow;

            #region Hide Columns
            gridDaily.Columns[0].Visible = false;
            gridDaily.Columns[gridDaily.Columns.Count - 1].Visible = false;
            gridDaily.Columns["FirstName"].Visible = false;
            gridDaily.Columns["LastName"].Visible = false;
            gridDaily.Columns["LocalUserID"].Visible = false;
            #endregion
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            var list = ((SortableBindingList<DailyPayment>)gridDaily.DataSource).ToList();
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

        private void btnStats_Click(object sender, EventArgs e)
        {
            var data = (SortableBindingList<DailyPayment>)gridDaily.DataSource;
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

        private void gridDaily_SelectionChanged(object sender, EventArgs e)
        {
            var value = 0M;
            var count = gridDaily.SelectedCells.Count;
            lblCount.Text = count.ToString();

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
            try
            {
                if (MessageBox.Show("ნამდვილად გსურთ ატვირთვა?", "ატვირთვა", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    DailyManagement.SubmitOrdersFromDatabase(User);
            }
            catch (Exception ex)
            {
                log.Error("ალტასოფტში ატვირთვა: ", ex);
                throw;
            }
            MessageBox.Show("წარმატებით აიტვირთა.");
        }

        private void gridDaily_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var count = 0;
            ((SortableBindingList<DailyPayment>)gridDaily.DataSource).Where(x => x.Payment > 0).ToList().ForEach(x => count++);

            decimal sum = 0;

            ((SortableBindingList<DailyPayment>)gridDaily.DataSource).Where(x => x.Payment > 0).ToList().ForEach(x => sum += x.Payment);

            lblPmtSum.Text = sum.ToString();
            lblPmtCount.Text = count.ToString();
        }

        private void btnStats_Click_1(object sender, EventArgs e)
        {
            TaxOrderGenerator.ExportToExcel(ConvertToExcelPayment((SortableBindingList<DailyPayment>)gridDaily.DataSource));
        }

        public static SortableBindingList<ExcelPayment> ConvertToExcelPayment(SortableBindingList<DailyPayment> v)
        {
            var list = new List<ExcelPayment>();
            v.ToList().ForEach(x => list.Add((ExcelPayment)x));
            return new SortableBindingList<ExcelPayment>(list);
        }

        private void gridDaily_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        #region Painting
        //private void gridDaily_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        //{
        //    if (e.ColumnIndex == this.gridDaily.CurrentCell.ColumnIndex

        //       && e.RowIndex == this.gridDaily.CurrentCell.RowIndex)
        //    {
        //        e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Border);

        //        using (Pen p = new Pen(Color.Black, 3))
        //        {
        //            Rectangle rect = e.CellBounds;
        //            rect.Width -= 0;
        //            rect.Height -= 2;
        //            e.Graphics.DrawRectangle(p, rect);
        //        }

        //        e.Handled = true;

        //    }
        //} 
        #endregion

        private void gridDaily_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    gridDaily.Sort(gridDaily.Columns[e.ColumnIndex], ListSortDirection.Ascending);
                    break;
                case MouseButtons.Right:
                    for (int i = 0; i < gridDaily.Columns.Count; i++)
                    {
                        gridDaily.Columns[i].DefaultCellStyle.BackColor = Color.Empty;
                    }

                    gridDaily.Columns[e.ColumnIndex].Frozen = !gridDaily.Columns[e.ColumnIndex].Frozen;

                    for (int i = 0; i <= e.ColumnIndex; i++)
                    {
                        if (gridDaily.Columns[i].Frozen)
                            gridDaily.Columns[i].DefaultCellStyle.BackColor = Color.FloralWhite;
                    }
                    break;
                default:
                    break;
            }
        }

        private void gridDaily_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var form = new SingleOrderForm(((SortableBindingList<DailyPayment>)gridDaily.DataSource)[e.ColumnIndex], User);
            form.Show();
        }

        private void DailyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("ნამდვილად გსურთ დახურვა?", "დახურვა", MessageBoxButtons.YesNo) == DialogResult.No)
                e.Cancel = true;
        }
    }
}
