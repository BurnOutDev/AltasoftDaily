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
    public partial class DailyForm : MetroForm
    {
        public User User { get; set; }
        public int DeptId { get; set; }
        public LoadingForm LoadingForm { get; set; }

        public DailyForm(User user)
        {
            User = user;
            InitializeComponent();
        }

        private void DailyForm_Load(object sender, EventArgs e)
        {
            gridDaily.DataSource = DailyManagement.GetDailyByUserId(User.DeptId, User.AltasoftUserID);
            LoadingForm = new LoadingForm();
            LoadingForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var data = ((List<DailyModel>)gridDaily.DataSource).Where(x => x.Payment > 0);

            string message = "starting count: " + data.Count() + "\n";
            message += "ids: ";
            foreach (var item in data)
            {
                message += DailyManagement.Add(0, item.LoanCCY, DateTime.Parse(item.CalculationDate), item.ClientAccountIban, item.Payment, "sesxis dafarva MainForm", "09", 21, 2) + "\n";
            }

            MessageBox.Show(message);
        }

        private void DailyForm_Shown(object sender, EventArgs e)
        {
            if (LoadingForm != null)
                LoadingForm.Close();
        }
    }
}
