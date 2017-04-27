using AltasoftDaily.Core;
using AltasoftDaily.Domain.POCO;
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
using AltasoftDaily.Helpers;
using AltasoftDaily.Domain.POCO.Logging;

namespace AltasoftDaily.UserInterface.WindowsForms
{
    public partial class LoggingForm : MetroForm
    {
        public User User { get; set; }
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
        public LogType LogType { get; set; }
        public Type DataType { get; set; }

        public LoggingForm(LogType type, User user)
        {
            InitializeComponent();
            LogType = type;
        }

        private void LoggingForm_Load(object sender, EventArgs e)
        {
            object data;

            switch (LogType)
            {
                case LogType.Signs:
                    data = new SortableBindingList<SignLog>(db.SignLogs.Include("User").ToList());
                    DataType = typeof(SignLog);
                    break;
                case LogType.Orders:
                    data = new SortableBindingList<OrderLog>(db.OrderLogs.Include("User").Include("LocalPayment").ToList());
                    DataType = typeof(OrderLog);
                    break;
                case LogType.Comments:
                    data = new SortableBindingList<CommentLog>(db.CommentLogs.Include("User").ToList());
                    DataType = typeof(CommentLog);
                    break;
                case LogType.Exceptions:
                    data = new SortableBindingList<ExceptionLog>(db.ExceptionLogs.Include("User").Where(x => x.IsInner == false).ToList());
                    DataType = typeof(ExceptionLog);
                    break;
                default:
                    data = null;
                    break;
            }
            
            dataGridView1.DataSource = data;
        }

        private void LoggingForm_Shown(object sender, EventArgs e)
        {
            dataGridView1.Columns[0].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }
    }

    public enum LogType
    {
        Signs,
        Orders,
        Comments,
        Exceptions
    }
}
