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

namespace AltasoftDaily.UserInterface.WindowsForms.Forms
{
    public partial class EntitiesForm : Form
    {
        //public AltasoftDailyContext db { get; set; }
        //public Type DataType { get; set; }
        //public int CaseID { get; set; }

        //public EntitiesForm(int caseId, AltasoftDailyContext _db, List<AgreementAndSummaryJudgementTerms> data)
        //{
        //    db = _db;
        //    CaseID = caseId;

        //    InitializeComponent();
        //}

        //public static EntitiesForm Show(int caseId, AltasoftDailyContext db, List<AgreementAndSummaryJudgementTerms> data)
        //{
        //    var form = new EntitiesForm(caseId, db, data);
        //    form.Show();
        //    return form;
        //}
        //private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        //{
        //    var data = (List<AgreementAndSummaryJudgementTerms>)(dataGridView1.DataSource);
        //    var dbcase = db.EnforcementLoans.FirstOrDefault(x => x.EnforcementID == CaseID);
        //    dbcase.AgreementAndSummaryJudgementTerms = 
        //}
    }
}
