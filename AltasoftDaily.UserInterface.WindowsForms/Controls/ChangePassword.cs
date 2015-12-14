using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Controls;

namespace AltasoftDaily.UserInterface.WindowsForms
{
    public partial class ChangePassword : MetroUserControl
    {
        public string PasswordOld
        {
            get
            {
                return tbxOldPassword.Text;
            }
            set
            {
                tbxOldPassword.Text = value;
            }
        }
        public string PasswordNew
        {
            get
            {
                return tbxNewPassword.Text;
            }
            set
            {
                tbxNewPassword.Text = value;
            }
        }
        public string PasswordNewRe
        {
            get
            {
                return tbxNewPasswordRe.Text;
            }
            set
            {
                tbxNewPasswordRe.Text = value;
            }
        }

        public ChangePassword()
        {
            InitializeComponent();
        }
    }
}
