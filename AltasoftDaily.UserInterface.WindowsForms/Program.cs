using AltasoftDaily.Core;
using AltasoftDaily.Domain.POCO;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AltasoftDaily.UserInterface.WindowsForms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var db = new AltasoftDailyContext())
            {
                var artura = db.Users.FirstOrDefault(x => x.Username == "atumaniani");
                //var filter = new Filter();
                //filter.Enabled = true;
                //filter.IsDeptFilterEnabled = true;
                //var deptIds = new List<int>();
                //deptIds.Add(2);
                //filter.DeptIDs = deptIds;
                //artura.Filter = filter;

                //artura.Filter.DeptIDs = new List<int>();
                artura.Filter.FilterData = new List<Domain.FilterData>();
                artura.Filter.FilterData.Add(new Domain.FilterData() { DeptID = 2 });

                db.SaveChanges();
            }

            XmlConfigurator.Configure();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
