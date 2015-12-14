using AltasoftAPI.CustomersAPI;
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
    public partial class CustomerForm : MetroForm
    {
        public Form Form { get; set; }
        public Customer Customer { get; set; }
        public IndividualEntity CustomerEntity { get; set; }
        public CustomerForm()
        {
            InitializeComponent();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CustomersForm_Load(object sender, EventArgs e)
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

            Customer = c.GetCustomer(AltasoftAPI.CustomersAPI.CustomerControlFlags.Basic | AltasoftAPI.CustomersAPI.CustomerControlFlags.Classifiers | AltasoftAPI.CustomersAPI.CustomerControlFlags.IdentityDocuments | AltasoftAPI.CustomersAPI.CustomerControlFlags.Addresses | AltasoftAPI.CustomersAPI.CustomerControlFlags.Extensions | AltasoftAPI.CustomersAPI.CustomerControlFlags.ContactPersons, true, 431, true);

            CustomerEntity = Customer.Entity as AltasoftAPI.CustomersAPI.IndividualEntity;
            tbxName.Text = CustomerEntity.Name.FirstName.ValueGeo;
            tbxLastName.Text = CustomerEntity.Name.LastName.ValueGeo;
            if (CustomerEntity.Name.FathersName != null)
                tbxFathersName.Text = CustomerEntity.Name.FathersName.ValueGeo;

            dtpBirthDate.Value = CustomerEntity.BirthPlaceDateAndCountry.Date.Value;
            cbxType.SelectedItem = CustomerEntity.Type.Value.ToString();
            if (CustomerEntity.Subtype2Specified)
                cbxSubType.SelectedItem = CustomerEntity.Subtype2.Value.ToString();

            cbxCitizenship.SelectedItem = CustomerEntity.Citizenship;

            gridDocuments.DataSource = CustomerEntity.IdentityDocuments;

            tbxHome.Text = Customer.ContactInfo.Phone;
            tbxMobile.Text = Customer.ContactInfo.MobilePhone;

            tbxAddress.Text = Customer.AddressLegal.Value.ValueGeo;
            tbxAddressFact.Text = Customer.AddressActual.Value.ValueGeo;

            this.Text += Customer.Id.Value.ToString();

            ///////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////
            //var loan = l.GetLoan(AltasoftAPI.LoansAPI.LoanControlFlags.Basic, true, 43, true);

            //var list = l.ListCollaterals(new AltasoftAPI.LoansAPI.ListCollateralsQuery()
            //{
            //    ControlFlags = AltasoftAPI.LoansAPI.CollateralControlFlags.Basic | AltasoftAPI.LoansAPI.CollateralControlFlags.Attributes
            //});

            //var list2 = l.ListLinkedCollaterals(new AltasoftAPI.LoansAPI.ListLinkedCollateralsQuery()
            //{
            //    ControlFlags = AltasoftAPI.LoansAPI.LinkedCollateralControlFlags.Basic | AltasoftAPI.LoansAPI.LinkedCollateralControlFlags.Attributes,
            //    ApplicationId = 571,
            //    ApplicationIdSpecified = true
            //});

            //var t = l.ListLinkedCollaterals(new AltasoftAPI.LoansAPI.ListLinkedCollateralsQuery()
            //{
            //    CollateralId = list.FirstOrDefault().CollateralId,
            //    CollateralIdSpecified = list.FirstOrDefault().CollateralIdSpecified,
            //    ControlFlags = AltasoftAPI.LoansAPI.LinkedCollateralControlFlags.Basic | AltasoftAPI.LoansAPI.LinkedCollateralControlFlags.Attributes,
            //    ApplicationIdSpecified = true
            //});

            //var tt = l.ListCollaterals(new AltasoftAPI.LoansAPI.ListCollateralsQuery()
            //{
            //    ControlFlags = AltasoftAPI.LoansAPI.CollateralControlFlags.Basic | AltasoftAPI.LoansAPI.CollateralControlFlags.Attributes,
            //    Id = t[0].CollateralId,
            //    IdSpecified = true, 
            //});
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void gridDocuments_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var doc = ((AltasoftAPI.CustomersAPI.IdentityDocument[])(gridDocuments.DataSource)).FirstOrDefault();
            Form = new IdentityDocumentForm(doc.Type.ToString(), doc.Country, doc.Issuer, CustomerEntity.BirthPlaceDateAndCountry.Place, doc.ValidityPeriod.Before.Value, doc.ValidityPeriod.After, false, doc.PIN, doc.PIN);
            Form.ShowDialog();
        }
    }
}
