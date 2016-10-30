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
using AltasoftDaily.Domain.POCO;
using System.Reflection;
using AltasoftDaily.UserInterface.WindowsForms.Controls;

namespace AltasoftDaily.UserInterface.WindowsForms
{
    public partial class AuthenticationForm : MetroForm
    {
        private User _user { get; set; }
        private int _deptId { get; set; }

        public AuthenticationForm()
        {
            InitializeComponent();

            tbxUsername.Text = Properties.Settings.Default.Username;
            cbxDept.SelectedIndex = Properties.Settings.Default.SelectedBranch;

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ShowLoading()
        {
            loadingControl1.Visible = true;
            foreach (Control item in this.Controls)
            {
                if (item is LoadingControl)
                    continue;
                item.Enabled = false;
            }
            loadingControl1.Refresh();
        }

        private void HideLoading()
        {
            loadingControl1.Visible = false;
            foreach (Control item in this.Controls)
            {
                if (item is LoadingControl)
                    continue;
                item.Enabled = true;
            }
            loadingControl1.Refresh();
        }

        private async void btnEnter_Click(object sender, EventArgs e)
        {
            ShowLoading();


            #region cOMMENT
            //await Task.Run(() => {
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

            //    var apiUsers = l.ListUsers(new AltasoftAPI.LoansAPI.ListUsersQuery());

            //    using (var db = new AltasoftDailyContext())
            //    {
            //        var dbUsers = db.Users.ToList();

            //        foreach (var user in apiUsers)
            //        {
            //            var dbUser = dbUsers.FirstOrDefault(x => x.AltasoftUserID == user.Id);

            //            if (dbUser == null)
            //            {
            //                var newUser = new User();

            //                newUser.Username = user.LoginName.Replace("@businesscredit.ge", "");
            //                newUser.AltasoftUserID = user.Id;
            //                newUser.DeptID = user.DeptId;
            //                newUser.Name = user.DisplayName.Split(' ').FirstOrDefault();
            //                newUser.Password = "123456";
            //                newUser.LastName = user.DisplayName.Split(' ').LastOrDefault();
            //                newUser.LastPasswordChange = DateTime.Now;

            //                db.Users.Add(newUser);
            //            }
            //        }

            //        var saved = db.SaveChanges();
            //    }
            //});

            #endregion

            //await Task.Run(() =>
            //{
            //    try
            //    {
                    bool authenticated;
                    string response;
                    _user = UserManagement.Authenticate(tbxUsername.Text, tbxPassword.Text, cbxDept.SelectedIndex, out authenticated, out response);

                    if (!authenticated)
                    {
                        MessageBox.Show(response);

                        return;
                    }
                    LoggingManagement.LogSign(SignType.SignIn, _user);

                    Properties.Settings.Default.Username = tbxUsername.Text;
                    Properties.Settings.Default.SelectedBranch = cbxDept.SelectedIndex;
                    Properties.Settings.Default.Save();

                    Close();
            //    }
            //    catch (Exception ex)
            //    {
            //        LoggingManagement.LogException(ex, null);
            //        throw;
            //    }
            //});

            HideLoading();
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
