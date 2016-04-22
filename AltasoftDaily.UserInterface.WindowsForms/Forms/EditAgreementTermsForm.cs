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
using AltasoftDaily.Domain.POCO;
using AltasoftDaily.Core;

namespace AltasoftDaily.UserInterface.WindowsForms.Forms
{
    public partial class EditAgreementTermsForm : MetroForm
    {
        public AgreementAndSummaryJudgementTerms Terms { get; set; }
        public int ID { get; set; }
        public AltasoftDailyContext _db { get; set; }

        public EditAgreementTermsForm(int AgreementAndSummaryJudgementTermsID, AltasoftDailyContext db)
        {
            ID = AgreementAndSummaryJudgementTermsID;
            Terms = db.AgreementAndSummaryJudgementTerms.FirstOrDefault(x => x.ID == AgreementAndSummaryJudgementTermsID);
            _db = db;
            InitializeComponent();

            tbxAdmittedAmount.Text = Terms.AdmittedAmount.ToString();
            tbxMonthlyPayment.Text = Terms.MonthlyPayment.ToString();
            dtpPaymentDate.Value = Terms.PaymentDate;
            dtpStartDate.Value = Terms.Start;
            dtpEndDate.Value = Terms.End;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var term = _db.AgreementAndSummaryJudgementTerms.FirstOrDefault(x => x.ID == ID);
            term.MonthlyPayment = decimal.Parse(tbxMonthlyPayment.Text);
            term.PaymentDate = dtpPaymentDate.Value;
            term.AdmittedAmount = decimal.Parse(tbxAdmittedAmount.Text);
            term.End = dtpEndDate.Value;
            term.Start = dtpStartDate.Value;

            _db.SaveChanges();

            MessageBox.Show("შეინახა");
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
