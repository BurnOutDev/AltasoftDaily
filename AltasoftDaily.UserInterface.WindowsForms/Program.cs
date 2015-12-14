using AltasoftDaily.Core;
using AltasoftDaily.Domain.POCO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace AltasoftDaily.UserInterface.WindowsForms
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            #region Initialize Services
            #region OrdersService
            AltasoftAPI.OrdersAPI.OrdersService o = new AltasoftAPI.OrdersAPI.OrdersService();
            o.RequestHeadersValue = new AltasoftAPI.OrdersAPI.RequestHeaders() { ApplicationKey = "BusinessCreditClient", RequestId = Guid.NewGuid().ToString() };
            #endregion

            #region CustomersService
            AltasoftAPI.CustomersAPI.CustomersService c = new AltasoftAPI.CustomersAPI.CustomersService();
            c.RequestHeadersValue = new AltasoftAPI.CustomersAPI.RequestHeaders() { ApplicationKey = "BusinessCreditClient", RequestId = Guid.NewGuid().ToString() };
            #endregion

            #region AccountsService
            AltasoftAPI.AccountsAPI.AccountsService a = new AltasoftAPI.AccountsAPI.AccountsService();
            a.RequestHeadersValue = new AltasoftAPI.AccountsAPI.RequestHeaders() { ApplicationKey = "BusinessCreditClient", RequestId = Guid.NewGuid().ToString() };
            #endregion

            #region LoansService
            AltasoftAPI.LoansAPI.LoansService l = new AltasoftAPI.LoansAPI.LoansService();
            l.RequestHeadersValue = new AltasoftAPI.LoansAPI.RequestHeaders() { ApplicationKey = "BusinessCreditClient", RequestId = Guid.NewGuid().ToString() };
            #endregion
            #endregion

            //var accs = from x in a.ListAccounts(new AltasoftAPI.AccountsAPI.ListAccountsQuery()
            //   {
            //       ControlFlags = AltasoftAPI.AccountsAPI.AccountControlFlags.Basic
            //   }).ToList()
            //           select new { AccountNumber = x.IBAN, Name = x.Name.ValueGeo };

            //using (var txtWriter = File.CreateText("exported.txt"))
            //{
            //    var serializer = JsonSerializer.Create();
            //    serializer.Serialize(txtWriter, accs.Where(x => x.Name.Contains("დაფარვის")));
            //}

            #region Old Comments
            //using (var db = new AltasoftDailyContext())
            //{
            //var artura = db.Users.FirstOrDefault(x => x.Username == "atumaniani");
            //var filter = new Filter();
            //filter.Enabled = true;
            //filter.IsDeptFilterEnabled = true;
            //var deptIds = new List<int>();
            //deptIds.Add(2);
            //filter.DeptIDs = deptIds;
            //artura.Filter = filter;

            //artura.Filter.DeptIDs = new List<int>();
            //artura.Filter.FilterData = new List<Domain.FilterData>();
            //artura.Filter.FilterData.Add(new Domain.FilterData() { DeptID = 3 });

            //db.SaveChanges();
            //} 
            //}
            #endregion

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
