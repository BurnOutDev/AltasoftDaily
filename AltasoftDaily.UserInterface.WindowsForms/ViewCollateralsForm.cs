using AltasoftDaily.Core;
using AltasoftDaily.Domain;
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
    public partial class ViewCollateralsForm : MetroForm
    {
        public int loanId;

        public ViewCollateralsForm(int loanID)
        {
            loanId = loanID;
            InitializeComponent();
        }

        private void ViewCollateralsForm_Load(object sender, EventArgs e)
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

            AltasoftAPI.LoansAPI.Application app;
            bool? n;
            bool n2;

            l.GetApplication(AltasoftAPI.LoansAPI.ApplicationControlFlags.Basic | AltasoftAPI.LoansAPI.ApplicationControlFlags.Extensions, true, loanId, true, out n, out n2, out app);

            var collaterals = l.ListLinkedCollaterals(new AltasoftAPI.LoansAPI.ListLinkedCollateralsQuery()
            {
                ControlFlags = AltasoftAPI.LoansAPI.LinkedCollateralControlFlags.Basic | AltasoftAPI.LoansAPI.LinkedCollateralControlFlags.Attributes,
                ApplicationId = app.Id.Value,
                ApplicationIdSpecified = app.IdSpecified
            });

            List<CollateralModel> list = new List<CollateralModel>();

            foreach (var item in collaterals)
            {
                var coll = l.ListCollaterals(new AltasoftAPI.LoansAPI.ListCollateralsQuery()
                {
                    Id = item.CollateralId,
                    IdSpecified = item.CollateralIdSpecified,
                    ControlFlags = AltasoftAPI.LoansAPI.CollateralControlFlags.Basic | AltasoftAPI.LoansAPI.CollateralControlFlags.Attributes
                });

                var customer = c.GetCustomer(AltasoftAPI.CustomersAPI.CustomerControlFlags.Basic | AltasoftAPI.CustomersAPI.CustomerControlFlags.Addresses, true, coll[0].OwnerId.Value, true);

                var col = new CollateralModel()
                {
                    Name = customer.Name.ValueGeo,
                    AgreementNumber = coll[0].AgreementNo,
                    CCy = item.LinkAmount.Ccy,
                    CollateralID = coll[0].CollateralId,
                    Discount = coll[0].Discount,
                    LiquidationAmount = item.LinkAmount.Amount,
                    MarketPrice = coll[0].MarketValue,
                    CloseDate = item.CloseDate
                };

                list.Add(col);
            }

            dataGridView1.DataSource = list;
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["CollateralID"].Value);

            var form = new CollateralForm(DailyManagement.GetCollateralIdByLoanId(loanId));

            form.Show();
        }
    }
}
