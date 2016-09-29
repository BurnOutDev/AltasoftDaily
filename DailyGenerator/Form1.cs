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

namespace DailyGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public IQueryable<User> GetUsers()
        {
            var date = DateTime.Today.AddDays(-1);

            using (var db = new AltasoftDailyContext())
            {
                var userIds = db.DailyPayments.Where(x => x.CalculationDate == date).Select(x => x.LocalUserID).Distinct();
                return userIds.Select(x => db.Users.FirstOrDefault(y => y.UserID == x));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var date = DateTime.Today.AddDays(-1);

            using (var db = new AltasoftDailyContext())
            {
                var userIds = db.DailyPayments.Where(x => x.CalculationDate == date).Select(x => x.LocalUserID).Distinct();
                var users = userIds.Select(x => db.Users.FirstOrDefault(y => y.UserID == x));
           

            button1.Enabled = false;
            string status = "Loading...";
            button1.Text = status;
            foreach (var user in users)
            {
                DailyManagement.GetUpdatesByAltasoftUser(user, ref status);
            }

            button1.Enabled = true;

            }
        }
    }
}
