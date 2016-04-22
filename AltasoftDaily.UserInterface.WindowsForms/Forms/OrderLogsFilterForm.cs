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

namespace AltasoftDaily.UserInterface.WindowsForms.Forms
{
    public partial class OrderLogsFilterForm : Form
    {
        public DateTime Date { get; set; }
        public int UserID { get; set; }
        public OrderLogsFilterForm()
        {
            using (var db = new AltasoftDailyContext())
            {
                comboBox1.Items.AddRange((from u in db.Users.ToArray() select new { FullName = u.Name + " " + u.LastName, ID = u.UserID }).ToArray());
            }

            InitializeComponent();

            comboBox1.DisplayMember = "FullName";
            comboBox1.ValueMember = "ID";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Date = dateTimePicker1.Value;
            UserID = (int)comboBox1.SelectedValue;
        }
    }
}
