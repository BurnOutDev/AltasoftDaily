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


            //////////////////////////////////////////////////////////////////////
            //#region Initialize Services
            //#region OrdersService
            //AltasoftAPI.OrdersAPI.OrdersService o = new AltasoftAPI.OrdersAPI.OrdersService();
            //o.RequestHeadersValue = new AltasoftAPI.OrdersAPI.RequestHeaders() { ApplicationKey = "BusinessCreditClient", RequestId = Guid.NewGuid().ToString() };
            //#endregion

            //#region CustomersService
            //AltasoftAPI.CustomersAPI.CustomersService c = new AltasoftAPI.CustomersAPI.CustomersService();
            //c.RequestHeadersValue = new AltasoftAPI.CustomersAPI.RequestHeaders() { ApplicationKey = "BusinessCreditClient", RequestId = Guid.NewGuid().ToString() };
            //#endregion

            //#region AccountsService
            //AltasoftAPI.AccountsAPI.AccountsService a = new AltasoftAPI.AccountsAPI.AccountsService();
            //a.RequestHeadersValue = new AltasoftAPI.AccountsAPI.RequestHeaders() { ApplicationKey = "BusinessCreditClient", RequestId = Guid.NewGuid().ToString() };
            //#endregion

            //#region LoansService
            //AltasoftAPI.LoansAPI.LoansService l = new AltasoftAPI.LoansAPI.LoansService();
            //l.RequestHeadersValue = new AltasoftAPI.LoansAPI.RequestHeaders() { ApplicationKey = "BusinessCreditClient", RequestId = Guid.NewGuid().ToString() };
            //#endregion
            //#endregion

            //var customer = c.GetCustomer(AltasoftAPI.CustomersAPI.CustomerControlFlags.Extensions, true, 1795, true);
        }

        private void takoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void კომენტარებიToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var commentsForm = new CommentsForm(User);
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
    }
}
