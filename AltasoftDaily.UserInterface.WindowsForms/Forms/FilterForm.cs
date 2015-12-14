using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AltasoftDaily.UserInterface.WindowsForms
{
    public partial class FilterForm : Form
    {
        public DateTime Date { get; set; }

        public FilterForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Date = dtpDate.Value.Date;
            DialogResult = DialogResult.OK;
        }
    }
}
