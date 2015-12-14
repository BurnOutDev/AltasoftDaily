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
    public partial class StatsForm : Form
    {
        public StatsForm(object source)
        {
            InitializeComponent();
            gridStats.DataSource = source;
        }

        private void StatsForm_Load(object sender, EventArgs e)
        {

        }
    }
}
