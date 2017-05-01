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
using KasaGE.Core;
using KasaGE.Commands;
using KasaGE.Responses;
using KasaGE;
using System.IO.Ports;
using System.Diagnostics;

namespace AltasoftDaily.UserInterface.WindowsForms
{
    public partial class DailyPaymentsForm : GridBaseForm
    {
        private Dp25 _ecr;

        public Dp25 Ecr
        {
            get
            {
                if (_ecr == null)
                    _ecr = new Dp25(cbxPorts.SelectedItem.ToString());
                return _ecr;
            }
        }

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
        public DailyPaymentsForm(User user)
        {
            User = user;
            InitializeComponent();

            cbxPorts.Items.AddRange(SerialPort.GetPortNames());
        }

        private async void GG_Load(object sender, EventArgs e)
        {
            loadingControl1.ShowLoading();

            var calcDate = DailyManagement.GetCalculationDate();

            if (MessageBox.Show(this, "გსურთ მონაცემების განახლება?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DailyManagement.GetUpdatesByAltasoftUser(User);
            }

            var payments = db.DailyPayments.Where(x => x.CalculationDate == calcDate && x.LocalUserID == User.UserID).OrderBy(x => x.IsOld).ThenBy(x => x.LoanID).ToList();
            gridData.DataSource = new SortableBindingList<DailyPayment>(payments.OrderByDescending(x => x.ResponsibleUser).ThenBy(x => x.LoanID).ToList());

            foreach (DataGridViewColumn col in gridData.Columns)
            {
                if (col.Name == "Payment")
                    continue;

                col.ReadOnly = true;
            }

            loadingControl1.HideLoading();
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
            var exPayments = ConvertToExcelPayment((SortableBindingList<DailyPayment>)gridData.DataSource);
            int i = 0;
            exPayments.ToList().ForEach(x => { x.N = i + 1; i++; });

            TaxOrderGenerator.ExportToExcel(exPayments);

            //var LIST = new SortableBindingList<Article>();
            //i = 0;
            //foreach (var item in (SortableBindingList<DailyPayment>)gridData.DataSource)
            //{
            //    LIST.Add(new Article()
            //        {
            //            Number = i + 1,
            //            Dept = 1,
            //            Name = item.LoanID + "/" + item.PersonalID,
            //            Price = 1
            //        });
            //    i++;
            //}

            //TaxOrderGenerator.ExportToExcel(new SortableBindingList<object>(LIST.Cast<object>().ToList()), typeof(Article));
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
            var list = ((SortableBindingList<DailyPayment>)gridData.DataSource).ToList().Where(x => x.IsSelected);

            var orders = list.Select(x =>
            new TaxOrder()
            {
                Date = DateTime.Now,
                OrderID = x.TaxOrderNumber.ToString(),
                ClientName = x.ClientName,
                ClientId = x.PersonalID,
                Description = "სესხის დაფარვა სესხის ხელშ. " + x.AgreementNumber + "-ის საფუძველზე",
                ResponsibleUser = User.Name + " " + User.LastName,
                ReceiverId = "ს/ნ 402000179",
                Amount = string.Empty,
                Currency = "GEL",
                ReceiverName = "სს მისო ბიზნეს კრედიტი"
            }).ToArray();

            var printer = string.Empty;
            var printersList = System.Drawing.Printing.PrinterSettings.InstalledPrinters;
            var printersToPass = new List<KeyValuePair<int, string>>();

            for (int i = 0; i < printersList.Count; i++)
                printersToPass.Add(new KeyValuePair<int, string>(i, printersList[i]));

            var printerForm = new ChoosePrinterForm(printersToPass.ToArray());

            if (printerForm.ShowDialog() == DialogResult.OK)
                printer = printerForm.SelectedItem?.Value;

            TaxOrderGenerator.GenerateMultiple(Path.Combine(Environment.CurrentDirectory, "TaxOrderTemplate.xlsx"), printer, orders);
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

            //try
            //{
            List<DailyPaymentIDOrderID> result = new List<DailyPaymentIDOrderID>();

            if (MessageBox.Show("ნამდვილად გსურთ ატვირთვა?", "ატვირთვა", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                result = DailyManagement.SubmitOrdersFromDatabase(User);
                //DailyManagement.InsertPaymentsInBusinessCreditDb(User);
            }

            foreach (var item in result)
            {
                var payment = db.DailyPayments.FirstOrDefault(x => x.DailyPaymentID == item.PaymentID);
                payment.OrderID = item.OrderID;

                LoggingManagement.LogOrder(payment, User);
            }

            db.SaveChanges();

            MessageBox.Show(string.Format("წარმატებით აიტვირთა {0} გადახდა.", result.Count));
            //}
            //catch (Exception ex)
            //{
            //    LoggingManagement.LogException(ex, User);
            //    throw;
            //}
        }

        private void GG_Shown(object sender, EventArgs e)
        {

        }

        public override void gridData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = sender as ADGV.AdvancedDataGridView;
            var cells = grid.Rows[e.RowIndex].Cells;
            var paymentID = Convert.ToInt32(cells["DailyPaymentID"].Value);
            var datasource = grid.DataSource as SortableBindingList<DailyPayment>;
            var payment = datasource.FirstOrDefault(x => x.DailyPaymentID == paymentID);

            payment.IsSelected = !payment.IsSelected;
            foreach (DataGridViewCell cell in cells)
            {
                cell.Style.BackColor = payment.IsSelected ? Color.LightGreen : Color.Empty;
            }

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

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            try
            {
                var data = (SortableBindingList<DailyPayment>)gridData.DataSource;

                if (MessageBox.Show("ნამდვილად გსურთ ჩაწერა?", "საქონლის ჩაწერა", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (MessageBox.Show("ამოიბეჭდოს Z ანგარიში?", "განულება", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        Ecr.PrintReport(ReportType.Z);

                    Ecr.DeleteItems(1, 10000);

                    var list = new SortableBindingList<object>();

                    foreach (var item in data)
                        list.Add(Ecr.ProgramItem(item.LoanID + " #" + item.TaxOrderNumber + " " + item.LastName, item.PLU, KasaGE.Commands.TaxGr.A, 1, 1, 1));

                    TaxOrderGenerator.ExportToExcel(list, typeof(ProgramItemResponse));

                    var success = list.All(x => ((ProgramItemResponse)x).CommandPassed);

                    if (success)
                        MessageBox.Show("ჩაიწერა წარმატებით.");
                    else
                        MessageBox.Show("მოხდა შეცდომა, გადახედე გახსნილ ექსელის ფაილს.");
                }
            }
            catch (Exception ex)
            {
                _ecr = null;
                throw;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            try
            {
                var data = (SortableBindingList<DailyPayment>)gridData.DataSource;
                var items = new List<ReadNextItemResponse>();

                var firsIitem = Ecr.ExecuteCustomCommand<ReadItemsResponse>(new ReadItemsCommand(1));

                ReadNextItemResponse response = null;

                if (firsIitem.CommandPassed)
                {
                    do
                    {
                        response = Ecr.ExecuteCustomCommand<ReadNextItemResponse>(new ReadNextItemCommand());
                        items.Add(response);
                    } while (response.CommandPassed);

                    data.FirstOrDefault(x => x.LoanID == int.Parse(firsIitem.Name.Substring(0, firsIitem.Name.IndexOf("/")))).Payment = firsIitem.Turnover;

                    foreach (var pay in items)
                    {
                        if (pay.CommandPassed)
                        {
                            data.FirstOrDefault(x => x.LoanID == int.Parse(pay.Name.Substring(0, pay.Name.IndexOf(" ")))).Payment = pay.Turnover;
                        }
                    }
                }

                MessageBox.Show(string.Format("განახლდა {0} გადახდა. გთხოვთ ატვირთოთ. \nჯამი: {1}", items.Count, items.Sum(x => x.Turnover) + firsIitem.Turnover));

            }
            catch (Exception ex)
            {
                _ecr = null;
                throw;
            }
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("ნამდვილად გსურთ Z რეპორტის დაბეჭდვა?", "Z რეპორტი", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (Ecr.PrintReport(ReportType.Z).CommandPassed)
                        MessageBox.Show("რეპორტი დაიბეჭდა.");
                    else
                        MessageBox.Show("შეცდომა: რეპორტი არ დაიბეჭდა.");
                }
            }
            catch (Exception ex)
            {
                _ecr = null;
                throw;
            }
        }

        public override void gridData_VisibleChanged(object sender, EventArgs e)
        {
            var grid = sender as ADGV.AdvancedDataGridView;

            foreach (DataGridViewRow row in grid.Rows)
            {
                var paymentID = Convert.ToInt32(row.Cells["DailyPaymentID"].Value);
                var datasource = grid.DataSource as SortableBindingList<DailyPayment>;
                var payment = datasource.FirstOrDefault(x => x.DailyPaymentID == paymentID);

                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Style.BackColor = payment.IsSelected ? Color.LightGreen : Color.Empty;
                }
            }
        }
    }
}
