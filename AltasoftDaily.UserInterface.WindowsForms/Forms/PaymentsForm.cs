using AltasoftDaily.Core;
using AltasoftDaily.Domain.POCO;
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
    public partial class PaymentsForm : Form
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

        private int loanID;
        public User User { get; set; }

        public PaymentsForm(int loanId)
        {
            loanID = loanId;
            InitializeComponent();
            dataGridView1.DataSource = db.DailyPayments.Where(x => x.LoanID == loanId).OrderBy(x => x.CalculationDate).ToList();
        }

        private void PaymentsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (MessageBox.Show("შევინახო ცვლილებები?", "დახურვა", MessageBoxButtons.YesNoCancel))
            {
                case DialogResult.None:
                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
                case DialogResult.Yes:
                    DailyManagement.UpdateCommentsInDaily(((List<DailyPayment>)dataGridView1.DataSource));
                    break;
                case DialogResult.No:
                    break;
            }
        }

        private void PaymentsForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form = new ViewCollateralsForm(loanID);
            form.Show();
        }
    }
}
