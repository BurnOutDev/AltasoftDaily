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

namespace AltasoftDaily.UserInterface.WindowsForms
{
    public partial class AuthenticationForm : MetroForm
    {
        private User _user { get; set; }
        private int _deptId { get; set; }

        public AuthenticationForm()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            bool authenticated;
            string response;
            _user = UserManagement.Authenticate(tbxUsername.Text, tbxPassword.Text, out authenticated, out response);

            if (!authenticated)
                MessageBox.Show(response);
            else
                Close();
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
