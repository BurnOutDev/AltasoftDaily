using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AltasoftDaily.Core;
using AltasoftDaily.Domain.POCO;
using System.Reflection;

namespace AltasoftDaily.UserInterface.WindowsForms.Forms
{
    public partial class EnforcementForm : GridBaseForm
    {
        private int rIndex;
        private int cIndex;

        public User User { get; set; }
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

        public EditEnforcementForm EnfForm { get; set; }


        public EnforcementForm(User user)
        {
            User = user;
            InitializeComponent();
        }

        private void EnforcementForm_Load(object sender, EventArgs e)
        {
            if (MessageBox.Show("გსურთ მონაცემების განახლება?", "მონაცემების განახლება", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var loanids = (from x in db.EnforcementLoans select x.LoanID).ToArray();

                db.EnforcementLoans.AddRange(DailyManagement.ListEnForcementLoans(User, loanids));
                db.SaveChanges();
            }

            var data = db.EnforcementLoans.Where(x => x.Status == EnforcementLoanStatus.Active).Where(x => x.ProblemManagerID == User.AltasoftUserID || User.AltasoftUserID == 33).ToList(); //33 - დაჩი იორამაშვილი

            gridData.DataSource = data;

            gridData.ReadOnly = true;

            #region Hide Columns
            gridData.Columns["IsActive"].Visible = false;
            #endregion

            //for (int i = 0; i < gridData.Rows.Count; i++)
            //{
            //    gridData["CaseStatus", i] = new DataGridViewTextBoxCell() { Value = GetEnumDescription((EnforcementCaseStatus)gridData["CaseStatus", i].Value), ValueType = typeof(string) };
            //    gridData["Status", i] = new DataGridViewTextBoxCell() { Value = GetEnumDescription((EnforcementLoanStatus)gridData["Status", i].Value) };
            //}
        }

        protected override void gridData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.FormattingApplied)
            {
                return;
            }

            gridData.SuspendLayout();

            if (gridData.Columns[e.ColumnIndex].Name == "CaseStatus")
            {
                e.Value = GetEnumDescription((EnforcementCaseStatus)e.Value);
            }
            else if (gridData.Columns[e.ColumnIndex].Name == "Status")
            {
                e.Value = GetEnumDescription((EnforcementLoanStatus)e.Value);
            }

            gridData.ResumeLayout();
        }

        public string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        private void pbxExport_Click(object sender, EventArgs e)
        {
            try
            {
                new ViewCollateralsForm(int.Parse(gridData.Rows[gridData.SelectedCells[0].RowIndex].Cells["LoanID"].Value.ToString())).Show();
            }
            catch (Exception ex)
            {
                LoggingManagement.LogException(ex, User);
                MessageBox.Show(string.Format("სესხი იდენტიფიკატორით {0} არ მოიძებნა!", gridData.Rows[gridData.SelectedCells[0].RowIndex].Cells["LoanID"].Value.ToString()));
            }
        }

        private void pbxFilter_Click(object sender, EventArgs e)
        {
            var index = gridData.SelectedCells[0].RowIndex;
            var lst = ((List<EnforcementLoan>)gridData.DataSource)[index];
            var id = db.EnforcementLoans.FirstOrDefault(x => x.EnforcementID == lst.EnforcementID).AgreementAndSummaryJudgementTerms.ID;

            var form = new EditAgreementTermsForm(id, db);
            form.Show();
        }

        private void pbxRefresh_Click(object sender, EventArgs e)
        {
            var enf = ((List<EnforcementLoan>)gridData.DataSource)[gridData.SelectedCells[0].RowIndex];
            EnfForm = new EditEnforcementForm(enf);
            EnfForm.FormClosed += EnfForm_FormClosed;
            EnfForm.Show();
        }

        void EnfForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (EnfForm.Save)
            {
                var newEntity = EnfForm.Entity;
                var oldEntity = db.EnforcementLoans.FirstOrDefault(x => x.EnforcementID == newEntity.EnforcementID);

                oldEntity.ApplicationCost = newEntity.ApplicationCost;
                oldEntity.ApplicationSubmitDate = newEntity.ApplicationSubmitDate;
                oldEntity.IncumbranceApplicationOrEnforcement = newEntity.IncumbranceApplicationOrEnforcement;
                oldEntity.IncumbranceCost = newEntity.ApplicationCost;
                oldEntity.InsuranceCost = newEntity.ApplicationCost;
                oldEntity.NotificationRegistry = newEntity.NotificationRegistry;
                oldEntity.GivePLD = newEntity.GivePLD;
                oldEntity.ContactPerson = newEntity.ContactPerson;
                oldEntity.CaseNo = newEntity.CaseNo;

                db.SaveChanges();
            }

            EnfForm = null;
        }

        public override void gridData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var form = new CellViewForm(gridData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                form.StartPosition = FormStartPosition.CenterParent;
                form.Show();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            gridData.SuspendLayout();
            if (cbxShowPassive.Checked)
                gridData.DataSource = db.EnforcementLoans.Where(x => x.ProblemManagerID == User.AltasoftUserID || User.AltasoftUserID == 33).ToList();
            else if (!cbxShowPassive.Checked)
                gridData.DataSource = db.EnforcementLoans.Where(x => x.Status == EnforcementLoanStatus.Active).Where(x => x.ProblemManagerID == User.AltasoftUserID || User.AltasoftUserID == 33).ToList();
            gridData.ResumeLayout();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string searchValue = tbxSearchString.Text;

            try
            {
                for (; rIndex < gridData.Rows.Count; rIndex++)
                {
                    for (; cIndex < gridData.Rows[rIndex].Cells.Count; cIndex++)
                    {
                        if (gridData[cIndex, rIndex].Value == null)
                            continue;

                        if (gridData[cIndex, rIndex].Value.ToString().ToUpper().Contains(tbxSearchString.Text.ToUpper()))
                        {
                            gridData.ClearSelection();
                            gridData[cIndex, rIndex].Selected = true;

                            gridData.FirstDisplayedScrollingRowIndex = rIndex;
                            gridData.FirstDisplayedScrollingColumnIndex = cIndex;

                            cIndex++;

                            return;
                        }
                    }
                    cIndex = 0;
                }
            }
            catch (Exception ex)
            {
                LoggingManagement.LogException(ex, User);
                MessageBox.Show(ex.Message);
            }
        }

        private void tbxSearchString_TextChanged(object sender, EventArgs e)
        {
            cIndex = 0;
            rIndex = 0;
        }
    }
}
