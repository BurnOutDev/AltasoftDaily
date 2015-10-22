using AltasoftDaily.Domain;
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
    public partial class MainForm : Form
    {
        public User User { get; set; }
        public int DeptId { get; set; }
        public MainForm()
        {
            InitializeComponent();
        }

        private void TmiDaily_Click(object sender, EventArgs e)
        {
                DailyForm frmDaily = new DailyForm(User);
                frmDaily.MdiParent = this;
                frmDaily.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var form = new AuthenticationForm();
            form.ShowDialog();

            if (form.GetUser() == null)
                Application.Exit();
            else
            {
                User = form.GetUser();
                DeptId = form.GetDeptId();
            }
        }
    }
}
