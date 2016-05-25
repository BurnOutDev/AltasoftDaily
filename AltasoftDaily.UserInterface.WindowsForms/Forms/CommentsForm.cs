using AltasoftDaily.Core;
using AltasoftDaily.Domain;
using AltasoftDaily.Domain.POCO;
using AltasoftDaily.Helpers;
using AltasoftDaily.UserInterface.WindowsForms.Forms;
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
    public partial class CommentsForm : MetroForm
    {
        private int _row;
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

        public CommentsForm(User user)
        {
            User = user;
            InitializeComponent();
        }

        private void DailyForm_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                LoggingManagement.LogException(ex, User);
                throw;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var updated = DailyManagement.UpdateCommentsInDaily(((SortableBindingList<DailyPayment>)gridDaily.DataSource).ToList());
                updated.ToList().ForEach(x => LoggingManagement.LogComment(x, User));
            }
            catch (Exception ex)
            {
                LoggingManagement.LogException(ex, User);
                throw;
            }

            MessageBox.Show("წარმატებით შეინახა.");
        }

        private void DailyForm_Shown(object sender, EventArgs e)
        {
            if (LoadingForm != null)
                LoadingForm.Close();

            int count = 0;
            //((SortableBindingList<DailyPayment>)gridDaily.DataSource).Where(x => x.Payment > 0).ToList().ForEach(x => count++);
            //lblPmtCount.Text = count.ToString();

            //decimal sum = 0;
            //((SortableBindingList<DailyPayment>)gridDaily.DataSource).Where(x => x.Payment > 0).ToList().ForEach(x => sum += x.Payment);
            //lblPmtSum.Text = Math.Round(sum, 2).ToString();
        }

        private void btnStats_Click(object sender, EventArgs e)
        {
            new ViewCollateralsForm(int.Parse(gridDaily.Rows[gridDaily.SelectedCells[0].RowIndex].Cells["LoanID"].Value.ToString())).Show();

            return;
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
            var data = (SortableBindingList<DailyPayment>)gridDaily.DataSource;

            var comments = ConvertToCommentsPaymentModel(data);

            var converted = new SortableBindingList<object>(ConvertToCommentsPaymentModel(data).Cast<object>().ToList());

            TaxOrderGenerator.ExportToExcel(converted, typeof(CommentsPaymentModel));
        }

        public static SortableBindingList<CommentsPaymentModel> ConvertToCommentsPaymentModel(SortableBindingList<DailyPayment> v)
        {
            var list = new List<CommentsPaymentModel>();
            v.ToList().ForEach(x => list.Add((CommentsPaymentModel)x));
            return new SortableBindingList<CommentsPaymentModel>(list);
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
            var loanId = ((SortableBindingList<DailyPayment>)gridDaily.DataSource)[e.ColumnIndex].LoanID;

            var paymentsForm = new PaymentsForm(loanId);
            paymentsForm.Show();
        }

        private void DailyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("ნამდვილად გსურთ დახურვა?", "დახურვა", MessageBoxButtons.YesNo) == DialogResult.No)
                e.Cancel = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            gridDaily.DataSource = null;
            gridDaily.Rows.Clear();
            gridDaily.Refresh();

            try
            {
                var calcDate = dateTimePicker1.Value.Date;

                var users = db.Users.Where(x => x.DeptID == comboBox1.SelectedIndex).ToList();

                var t = db.DailyPayments.Where(x => users.Any(y => y.UserID == x.LocalUserID) && x.CalculationDate == calcDate);

                if (cbxOnlyZeroPayment.Checked)
                //gridDaily.DataSource = new SortableBindingList<DailyPayment>(db.DailyPayments.Where(x => x.CalculationDate == calcDate && x.DeptID == comboBox1.SelectedIndex && x.Payment <= 0).ToList());
                {
                    var lst = new List<DailyPayment>();
                    foreach (var user in users)
                    {
                        lst.AddRange(db.DailyPayments.Where(x => user.UserID == x.LocalUserID && x.CalculationDate == calcDate && x.Payment > 0 && x.IsOld).ToList());
                    }
                    gridDaily.DataSource = new SortableBindingList<DailyPayment>(lst);
                }
                else
                //gridDaily.DataSource = new SortableBindingList<DailyPayment>(db.DailyPayments.Where(x => x.CalculationDate == calcDate && x.DeptID == comboBox1.SelectedIndex).ToList());
                {
                    var lst = new List<DailyPayment>();
                    foreach (var user in users)
                    {
                        lst.AddRange(db.DailyPayments.Where(x => user.UserID == x.LocalUserID && x.CalculationDate == calcDate).ToList());
                    }
                    gridDaily.DataSource = new SortableBindingList<DailyPayment>(lst);
                }

                ////

                var expressPay = DailyManagement.GetAccountsTest(calcDate.Date, calcDate.Date, new decimal[] { 2501 }, null);

                foreach (var row in (SortableBindingList<DailyPayment>)gridDaily.DataSource)
                {
                    var expay = expressPay.Where(x => x.Purpose.Contains(row.AgreementNumber));

                    if (expay.Count() > 0)
                        row.Payment += expay.Sum(x => x.DebitAmount.Value);
                }

                gridDaily.Refresh();
            }
            catch (Exception ex)
            {
                LoggingManagement.LogException(ex, User);
            }
        }

        private void gridDaily_DataSourceChanged(object sender, EventArgs e)
        {
            if (((DataGridView)sender).DataSource != null)
            {
                gridDaily.Columns["Payment"].DefaultCellStyle.BackColor = Color.Yellow;

                #region Hide Columns
                gridDaily.Columns[0].Visible = false;
                gridDaily.Columns[gridDaily.Columns.Count - 2].Visible = false;
                gridDaily.Columns["FirstName"].Visible = false;
                gridDaily.Columns["LastName"].Visible = false;
                gridDaily.Columns["LocalUserID"].Visible = false;
                #endregion

                foreach (DataGridViewColumn col in gridDaily.Columns)
                {
                    if (col.Name == "Comment")
                        continue;

                    col.ReadOnly = true;
                } 
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var data = (SortableBindingList<DailyPayment>)gridDaily.DataSource;

            foreach (var item in data)
            {
                var loan = DailyManagement.GetLoanAndDailyModel(item.LoanID);
                item.Comment = (item.CurrentDebtInGel - loan.DailyPayment.CurrentDebtInGel - item.Payment).ToString();
            }
        }

        private void gridDaily_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip m = contextMenuStrip1;

                _row = gridDaily.HitTest(e.X, e.Y).RowIndex;

                m.Show(gridDaily, new Point(e.X, e.Y));
            }
        }

        private void კომენტარისდამატებაToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var pid = (int)gridDaily.Rows[_row].Cells["DailyPaymentID"].Value;
            var form = new AddCommentForm(pid, User);
            form.Show();
        }
    }
}
