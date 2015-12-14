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
    public partial class CollateralForm : MetroForm
    {
        private int collateralID;

        public CollateralForm(int collateralId)
        {
            collateralID = collateralId;
            InitializeComponent();
        }

        private void CollateralForm_Load(object sender, EventArgs e)
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

            var collateral = l.ListCollaterals(new AltasoftAPI.LoansAPI.ListCollateralsQuery()
            {
                ControlFlags = AltasoftAPI.LoansAPI.CollateralControlFlags.Basic | AltasoftAPI.LoansAPI.CollateralControlFlags.Attributes,
                Id = collateralID,
                IdSpecified = true
            });

            var linked = l.ListLinkedCollaterals(new AltasoftAPI.LoansAPI.ListLinkedCollateralsQuery()
            {
                CollateralId = collateralID,
                CollateralIdSpecified = true,
                ControlFlags = AltasoftAPI.LoansAPI.LinkedCollateralControlFlags.Basic | AltasoftAPI.LoansAPI.LinkedCollateralControlFlags.Attributes,
                ApplicationIdSpecified = true
            });

            tbxType.Text = collateral[0].CollateralType;

            var customer = c.GetCustomer(AltasoftAPI.CustomersAPI.CustomerControlFlags.Basic | AltasoftAPI.CustomersAPI.CustomerControlFlags.Addresses | AltasoftAPI.CustomersAPI.CustomerControlFlags.Extensions, true, collateral[0].OwnerId.Value, true);

            tbxOwner.Text = collateral[0].OwnerId.Value.ToString() + " | " + customer.Name.ValueGeo;
            tbxIdNumber.Text = (customer.Entity as AltasoftAPI.CustomersAPI.IndividualEntity).PIN;
            tbxBranch.Text = customer.BranchId.Value.ToString();
            tbxAgreement.Text = collateral[0].AgreementNo;
            tbxAddressLegal.Text = customer.AddressLegal.Value.ValueGeo;
            tbxMobile.Text = customer.ContactInfo.MobilePhone;
            tbxPhone.Text = customer.ContactInfo.Phone;

            var relations = new List<RelationModel>();

            foreach (var item in linked)
            {
                bool? n;
                bool n2;
                AltasoftAPI.LoansAPI.Application app;
                l.GetApplication(AltasoftAPI.LoansAPI.ApplicationControlFlags.Basic | AltasoftAPI.LoansAPI.ApplicationControlFlags.Extended | AltasoftAPI.LoansAPI.ApplicationControlFlags.ExtraFields | AltasoftAPI.LoansAPI.ApplicationControlFlags.Extensions, true, item.ApplicationId.Value, true, out n, out n2, out app);

                var loan = l.GetLoan(AltasoftAPI.LoansAPI.LoanControlFlags.Basic, true, app.Id.Value, true);
                var customer2 = c.GetCustomer(AltasoftAPI.CustomersAPI.CustomerControlFlags.Basic, true, loan.BorrowerId.Value, true);


                var r = new RelationModel()
                {
                    AgreementNo = loan.AgreementNo,
                    Amount = item.LinkAmount.Amount,
                    Borrower = customer2.Name.ValueGeo,
                    Currency = item.LinkAmount.Ccy,
                    EndDate = loan.Term.End,
                    StartDate = loan.Term.Start,
                    LoanAmount = loan.Amount.Amount,
                    LoanCurrency = loan.Amount.Ccy,
                    LoanInterest = loan.InterestBasis.ToString() + "%",
                    Product = l.GetLoanProduct(loan.ProductId.Value, true).Name,
                    RelationDate = item.LinkDate.Value,
                    Status = item.State == AltasoftAPI.LoansAPI.CollateralLinkState.Closed ? RelationStatus.დახურული : RelationStatus.მიმდინარე,
                };

                relations.Add(r);
            }

            gridRelations.DataSource = relations;
        }
    }
}
