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
using AltasoftDaily.Domain;
using AltasoftDaily.Domain.POCO;
using AltasoftDaily.Domain.POCO.Logging;
using System.Reflection;

namespace AltasoftDaily.UserInterface.WindowsForms.Forms
{
    public partial class AddCommentForm : Form
    {
        private AltasoftDailyContext _db;
        public AltasoftDailyContext db
        {
            get
            {
                if (_db == null)
                    _db = new AltasoftDailyContext();
                return _db;
            }
        }

        public User User { get; set; }
        public int DailyPaymentID { get; set; }
        public List<CommentLog> Comments { get; set; }

        public AddCommentForm(int dailyPaymentID, User user)
        {
            User = user;
            DailyPaymentID = dailyPaymentID;

            InitializeComponent();

            LoadData();

            cbxCommentType.Items.AddRange((from x in Enum.GetValues(typeof(CommentType)).Cast<CommentType>().ToList()
                                          select new { DisplayMember = GetEnumDescription((CommentType)x), ValueMember = x }).Cast<object>().ToArray());
        }

        private void LoadData()
        {
            Comments = db.CommentLogs.Where(x => x.LocalPayment.DailyPaymentID == DailyPaymentID).ToList();

            var comments = (from x in Comments
                            select new CommentView { Date = x.Date, Type = GetEnumDescription(x.Type), Comment = x.CommentValue }).ToList();

            gridHostory.DataSource = comments;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddCommentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("ნამდვილად გსურთ გაუქმება?", "გაუქმება", MessageBoxButtons.YesNo) == DialogResult.Yes)
                return;
            else
                e.Cancel = true;
        }

        private void b_Click(object sender, EventArgs e)
        {
            dynamic val = cbxCommentType.SelectedItem;

            db.CommentLogs.Add(new CommentLog()
                {
                    CommentValue = tbxNewComment.Text,
                    LocalPayment = db.DailyPayments.First(x => x.DailyPaymentID == DailyPaymentID),
                    Date = DateTime.Now,
                    Type = (CommentType)val.ValueMember,
                    User = User
                });
            db.SaveChanges();

            tbxNewComment.Clear();

            LoadData();

            MessageBox.Show("შენახა წარმატებით!");
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
    }
}
