using MetroFramework.Forms;
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
    public partial class ChoosePrinterForm : MetroForm
    {
        public KeyValuePair<int, string>? SelectedItem
        {
            get
            {
                return cbxChoosePrinter.SelectedItem as KeyValuePair<int, string>?;
            }
        }

        public ChoosePrinterForm(KeyValuePair<int, string>[] items)
        {
            InitializeComponent();
            cbxChoosePrinter.DataSource = items;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
