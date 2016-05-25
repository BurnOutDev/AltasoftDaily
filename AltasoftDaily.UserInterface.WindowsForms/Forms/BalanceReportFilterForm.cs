using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AltasoftDaily.UserInterface.WindowsForms.Forms
{
    public partial class BalanceReportFilterForm : Form
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? BranchID { get; set; }
        public decimal[] BalCode { get; set; }

        public BalanceReportFilterForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartDate = dtpStart.Value;
            EndDate = dtpEnd.Value;

            if (!string.IsNullOrWhiteSpace(txtBalCode.Text))
                BalCode = (from x in txtBalCode.Text.Split(',')
                              select decimal.Parse(x)).ToArray();

            if (!string.IsNullOrWhiteSpace(txtBranchID.Text))
                BranchID = int.Parse(txtBranchID.Text);

            this.DialogResult = DialogResult.OK;
        }
    }
}
