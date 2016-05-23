using AltasoftDaily.Domain.POCO;
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

namespace AltasoftDaily.UserInterface.WindowsForms.Forms
{
    public partial class EditEnforcementForm : Form
    {
        public EnforcementLoan Entity { get; set; }
        public bool Save { get; set; }

        public EditEnforcementForm(EnforcementLoan enf)
        {
            Entity = enf;
            this.Text = enf.BorrowerName;
            InitializeComponent();
            cbxCaseStatus.Items.AddRange((from x in Enum.GetValues(typeof(EnforcementCaseStatus)).Cast<EnforcementCaseStatus>().ToList()
                                          select new { DisplayMember = GetEnumDescription((EnforcementCaseStatus)x), ValueMember = x }).Cast<object>().ToArray());
            cbxStatus.Items.AddRange((from x in Enum.GetValues(typeof(EnforcementLoanStatus)).Cast<EnforcementLoanStatus>().ToList()
                                      select new { DisplayMember = GetEnumDescription((EnforcementLoanStatus)x), ValueMember = x }).Cast<object>().ToArray());
        }

        private void EditEnforcementForm_Load(object sender, EventArgs e)
        {
            tbxAgreementNo.Text = Entity.LoanAgreementNumber;
            tbxApplicationCost.Text = Entity.ApplicationCost.ToString();
            tbxCaseNo.Text = Entity.CaseNo;
            tbxContactPerson.Text = Entity.ContactPerson;
            tbxIncumbranceApplicationOrEnforcement.Text = Entity.IncumbranceApplicationOrEnforcement;
            tbxIncumbranceCost.Text = Entity.IncumbranceCost.ToString();
            tbxInsuranceCost.Text = Entity.InsuranceCost.ToString();
            tbxLoanID.Text = Entity.LoanID.ToString();
            tbxNotificationRegistry.Text = Entity.NotificationRegistry;
            cbxStatus.SelectedIndex = (int)Entity.Status;
            cbxCaseStatus.SelectedIndex = (int)Entity.CaseStatus;
            dtpApplicationSubmitDate.Value = Entity.ApplicationSubmitDate;
            dtpGivePLD.Value = Entity.GivePLD;
            tbxComment.Text = Entity.Comment;
        }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Entity.ApplicationCost = decimal.Parse(tbxApplicationCost.Text);
            Entity.CaseNo = tbxCaseNo.Text;
            Entity.ContactPerson = tbxContactPerson.Text;
            Entity.IncumbranceApplicationOrEnforcement = tbxIncumbranceApplicationOrEnforcement.Text;
            Entity.IncumbranceCost = decimal.Parse(tbxIncumbranceCost.Text);
            Entity.InsuranceCost = decimal.Parse(tbxInsuranceCost.Text);
            Entity.NotificationRegistry = tbxNotificationRegistry.Text;
            Entity.Status = (EnforcementLoanStatus)cbxStatus.SelectedIndex;
            Entity.CaseStatus = (EnforcementCaseStatus)cbxCaseStatus.SelectedIndex;
            Entity.Comment = tbxComment.Text;
            
            Save = true;
            this.Close();
        }
    }
}
