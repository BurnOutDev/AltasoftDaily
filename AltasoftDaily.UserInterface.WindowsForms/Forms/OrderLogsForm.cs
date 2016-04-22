using AltasoftDaily.Core;
using AltasoftDaily.Domain.POCO;
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
    public partial class OrderLogsForm : GridBaseForm
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
        public int DeptId { get; set; }
        public LoadingForm LoadingForm { get; set; }
        public OrderLogsForm(User user)
        {
            InitializeComponent();
        }

        private void pbxFilter_Click(object sender, EventArgs e)
        {

        }
    }
}
