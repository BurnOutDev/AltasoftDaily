using AltasoftDaily.Domain.POCO;
using log4net;
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
    public partial class MainForm : Form
    {
        public User User { get; set; }
        public int DeptId { get; set; }
        private readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

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
            //Log
            log.Info("Application started.");

            var form = new AuthenticationForm();
            form.ShowDialog();

            if (form.GetUser() == null)
                Application.Exit();
            else
            {
                User = form.GetUser();
                DeptId = form.GetDeptId();
            }

            if (User.Username == "tkobalia")
            {
                takoToolStripMenuItem.Enabled = true;
                ინკასატორიToolStripMenuItem.Enabled = false;
            }

            log.Info("User logged in: " + User.Username);
        }

        private void takoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void კომენტარებიToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var commentsForm = new CommentsForm(User);
            commentsForm.Show();
        }
    }
}
