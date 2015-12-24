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
using AltasoftDaily.Helpers;

namespace AltasoftDaily.UserInterface.WindowsForms
{
    public partial class OldPaymentsForm : GridBaseForm
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
        public DateTime Date { get; set; }

        public OldPaymentsForm(User user)
        {
            User = user;
            InitializeComponent();
        }

        private void pbxFilter_Click(object sender, EventArgs e)
        {
            var form = new FilterForm();

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                Date = form.Date;
                GetData();
            }
        }

        private void GetData()
        {
            var payments = db.DailyPayments.Where(x => x.LocalUserID == User.UserID && x.CalculationDate == Date).ToList();
            gridData.DataSource = payments;
        }

        public override void gridData_SelectionChanged(object sender, EventArgs e)
        {
            base.gridData_SelectionChanged(sender, e);
        }

        private void pbxExport_Click(object sender, EventArgs e)
        {
            var obectList = gridData.DataSource as List<DailyPayment>;
            var lst = new SortableBindingList<object>(obectList.Cast<object>().ToList());

            TaxOrderGenerator.ExportToExcel(lst, typeof(DailyPayment));
        }
    }
}
