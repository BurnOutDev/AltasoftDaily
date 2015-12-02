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
    public partial class IdentityDocumentForm : MetroForm
    {
        public IdentityDocumentForm(string docType, string country, string regOrgan, string birthPlace, DateTime issueDate1, DateTime issueDate2, bool isPermanent, string docNumber, string personalID)
        {
            InitializeComponent();
            cbxDocumentType.SelectedItem = docType;
            cbxCountry.SelectedItem = country;
            tbxRegOrgan.Text = regOrgan;
            tbxBirthPlace.Text = birthPlace;
            dtpIssueDate1.Value = issueDate1;
            dtpIssueDate2.Value = issueDate2;

            chkIsPermanent.Checked = isPermanent;
            tbxDocumentNumber.Text = docNumber;
            tbxPersonalID.Text = personalID;
        }

        private void IdentityDocumentForm_Load(object sender, EventArgs e)
        {

        }
    }
}
