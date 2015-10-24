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
using AltasoftDaily.Core;
using AltasoftDaily.Domain;
using log4net;
using System.Reflection;

namespace AltasoftDaily.UserInterface.WindowsForms
{
    public partial class AuthenticationForm : MetroForm
    {
        private readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private User _user { get; set; }
        private int _deptId { get; set; }

        public AuthenticationForm()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //Log
            log.Info("Application exited.");

            Application.Exit();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            try
            {
                bool authenticated;
                string response;
                _user = UserManagement.Authenticate(tbxUsername.Text, tbxPassword.Text, cbxDept.SelectedIndex, out authenticated, out response);

                if (!authenticated)
                {
                    MessageBox.Show(response);

                    //Log
                    log.Warn("User: " + tbxUsername.Text + " " + response);

                    return;
                }

                //Log
                log.Info("User: " + tbxUsername.Text + " logged in successfuly.");

                Close();
            }
            catch (Exception ex)
            {
                log.Error("Authentication Error: ", ex);
            }
        }

        public User GetUser()
        {
            return _user;
        }
        public int GetDeptId()
        {
            return _deptId;
        }

        private void AuthenticationForm_Load(object sender, EventArgs e)
        {

        }

        private void cbxDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            _deptId = cbxDept.SelectedIndex;
        }
    }
}
