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
    }
}
