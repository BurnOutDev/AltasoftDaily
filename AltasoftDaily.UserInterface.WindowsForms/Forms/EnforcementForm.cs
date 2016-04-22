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

namespace AltasoftDaily.UserInterface.WindowsForms.Forms
{
    public partial class EnforcementForm : GridBaseForm
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


        public EnforcementForm()
        {
            InitializeComponent();
        }

        private void EnforcementForm_Load(object sender, EventArgs e)
        {
            var data = db.EnforcementLoans.Where(x => x.Status == EnforcementLoanStatus.Active).ToList();

            gridData.DataSource = data;
        }

        private void pbxExport_Click(object sender, EventArgs e)
        {
            new ViewCollateralsForm(int.Parse(gridData.Rows[gridData.SelectedCells[0].RowIndex].Cells["LoanID"].Value.ToString())).Show();
        }

        private void pbxFilter_Click(object sender, EventArgs e)
        {
            var index = gridData.SelectedCells[0].RowIndex;
            var lst = ((List<EnforcementLoan>)gridData.DataSource)[index];
            var id = db.EnforcementLoans.FirstOrDefault(x => x.EnforcementID == lst.EnforcementID).AgreementAndSummaryJudgementTerms.ID;

            var form = new EditAgreementTermsForm(id, db);
            form.Show();
        }
    }
}
