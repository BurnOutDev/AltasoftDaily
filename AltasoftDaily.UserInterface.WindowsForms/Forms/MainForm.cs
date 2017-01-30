using AltasoftAPI.AccountsAPI;
using AltasoftDaily.Core;
using AltasoftDaily.Domain.POCO;
using AltasoftDaily.Helpers;
using AltasoftDaily.UserInterface.WindowsForms.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
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

        public MainForm()
        {
            //Task.Run(SyncUsers).Wait();
            
            InitializeComponent();

            WindowState = FormWindowState.Minimized;
        }

        private async Task SyncUsers()
        {
            #region Initialize Services
            #region OrdersService
            AltasoftAPI.OrdersAPI.OrdersService o = new AltasoftAPI.OrdersAPI.OrdersService();
            o.RequestHeadersValue = new AltasoftAPI.OrdersAPI.RequestHeaders() { ApplicationKey = "BusinessCreditClient", RequestId = Guid.NewGuid().ToString() };
            #endregion

            #region CustomersService
            AltasoftAPI.CustomersAPI.CustomersService c = new AltasoftAPI.CustomersAPI.CustomersService();
            c.RequestHeadersValue = new AltasoftAPI.CustomersAPI.RequestHeaders() { ApplicationKey = "BusinessCreditClient", RequestId = Guid.NewGuid().ToString() };
            #endregion

            #region AccountsService
            AltasoftAPI.AccountsAPI.AccountsService a = new AltasoftAPI.AccountsAPI.AccountsService();
            a.RequestHeadersValue = new AltasoftAPI.AccountsAPI.RequestHeaders() { ApplicationKey = "BusinessCreditClient", RequestId = Guid.NewGuid().ToString() };
            #endregion

            #region LoansService
            AltasoftAPI.LoansAPI.LoansService l = new AltasoftAPI.LoansAPI.LoansService();
            l.RequestHeadersValue = new AltasoftAPI.LoansAPI.RequestHeaders() { ApplicationKey = "BusinessCreditClient", RequestId = Guid.NewGuid().ToString() };
            #endregion
            #endregion

            var apiUsers = l.ListUsers(new AltasoftAPI.LoansAPI.ListUsersQuery());

            using (var db = new AltasoftDailyContext())
            {
                var dbUsers = db.Users.ToList();

                foreach (var user in apiUsers)
                {
                    var dbUser = dbUsers.FirstOrDefault(x => x.AltasoftUserID == user.Id);

                    if (dbUser == null)
                    {
                        var newUser = new User();

                        newUser.Username = user.LoginName.Replace("@businesscredit.ge", "");
                        newUser.AltasoftUserID = user.Id;
                        newUser.DeptID = user.DeptId;
                        newUser.Name = user.DisplayName.Split(' ').FirstOrDefault();
                        newUser.Password = "123456";
                        newUser.LastName = user.DisplayName.Split(' ').LastOrDefault();
                        newUser.LastPasswordChange = DateTime.Now;

                        db.Users.Add(newUser);
                    }
                }

                var saved = db.SaveChanges();
            }
        }
        private async void TmiDaily_Click(object sender, EventArgs e)
        {
            DailyPaymentsForm frmDaily = new DailyPaymentsForm(User);
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

                if (User.LastPasswordChange <= DateTime.Today.AddDays(-30) || User.ForceUserToChangePassword)
                {
                    var changeForm = new ChangePasswordForm(User, true);
                    changeForm.ShowDialog();
                }

                if (!User.ForceUserToChangePassword && User.LastPasswordChange > DateTime.Today.AddDays(-30))
                {
                    DeptId = form.GetDeptId();
                    this.Text += string.Format(" ({0} {1})", User.Name, User.LastName);

                    if (User.Username == "tkobalia")
                    {
                        takoToolStripMenuItem.Enabled = true;
                        ინკასატორიToolStripMenuItem.Enabled = false;
                    }

                    if (User.Username == "mesakia")
                    {
                        ბუღალტერიაToolStripMenuItem.Enabled = true;
                        takoToolStripMenuItem.Enabled = false;
                        ინკასატორიToolStripMenuItem.Enabled = false;
                    }

                    this.WindowState = FormWindowState.Maximized; 
                }
                else
                {
                    Application.Exit();
                }
            }
        }

        private void takoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void კომენტარებიToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var commentsForm = new CommentsForm(User);
            commentsForm.MdiParent = this;
            commentsForm.Show();
        }

        private void რეპორტებიToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //    #region Initialize Services
            //    #region OrdersService
            //    AltasoftAPI.OrdersAPI.OrdersService o = new AltasoftAPI.OrdersAPI.OrdersService();
            //    o.RequestHeadersValue = new AltasoftAPI.OrdersAPI.RequestHeaders() { ApplicationKey = "BusinessCreditClient", RequestId = Guid.NewGuid().ToString() };
            //    #endregion

            //    #region CustomersService
            //    AltasoftAPI.CustomersAPI.CustomersService c = new AltasoftAPI.CustomersAPI.CustomersService();
            //    c.RequestHeadersValue = new AltasoftAPI.CustomersAPI.RequestHeaders() { ApplicationKey = "BusinessCreditClient", RequestId = Guid.NewGuid().ToString() };
            //    #endregion

            //    #region AccountsService
            //    AltasoftAPI.AccountsAPI.AccountsService a = new AltasoftAPI.AccountsAPI.AccountsService();
            //    a.RequestHeadersValue = new AltasoftAPI.AccountsAPI.RequestHeaders() { ApplicationKey = "BusinessCreditClient", RequestId = Guid.NewGuid().ToString() };
            //    #endregion

            //    #region LoansService
            //    AltasoftAPI.LoansAPI.LoansService l = new AltasoftAPI.LoansAPI.LoansService();
            //    l.RequestHeadersValue = new AltasoftAPI.LoansAPI.RequestHeaders() { ApplicationKey = "BusinessCreditClient", RequestId = Guid.NewGuid().ToString() };
            //    #endregion
            //    #endregion

            //    var t = c.GetCustomer(AltasoftAPI.CustomersAPI.CustomerControlFlags.Basic, true, 1795, true);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoggingManagement.LogSign(SignType.SignOut, User);
        }

        public void ReloadData(GridBaseForm target)
        {

        }

        private void კლიენტებიToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new OldPaymentsForm(User);
            form.MdiParent = this;
            form.Show();
        }

        private void b6ისრეპორტიToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new BalanceReportForm();
            form.MdiParent = this;
            form.Show();
        }

        private void შეტვირთვებიToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void პრობლემურებიToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new EnforcementForm(User);
            form.Show();
        }
    }
}
