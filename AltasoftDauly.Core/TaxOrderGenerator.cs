using AltasoftDaily.Domain;
using AltasoftDaily.Domain.POCO;
using AltasoftDaily.Helpers;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AltasoftDaily.Core
{
    public static class TaxOrderGenerator
    {
        public static void ExportToExcel(SortableBindingList<ExcelPayment> data)
        {
            var filePath = Path.Combine(Environment.GetEnvironmentVariable("temp"), Guid.NewGuid().ToString() + ".xlsx");

            using (ExcelPackage ePack = new ExcelPackage())
            {
                ExcelWorksheet ws = ePack.Workbook.Worksheets.Add("Accounts");
                ws.Cells["A1"].LoadFromDataTable(ToDataTable(data.ToList()), true);
                ePack.SaveAs(new FileInfo(filePath));

                Process.Start(filePath);
            }
        }

        public static void ExportToExcel(SortableBindingList<object> data, Type dataType)
        {
            var filePath = Path.Combine(Environment.GetEnvironmentVariable("temp"), Guid.NewGuid().ToString() + ".xlsx");

            using (ExcelPackage ePack = new ExcelPackage())
            {
                ExcelWorksheet ws = ePack.Workbook.Worksheets.Add("_");
                ws.Cells["A1"].LoadFromDataTable(ToDataTable(data.ToList(), dataType.GetProperties(BindingFlags.Public | BindingFlags.Instance)), true);
                ePack.SaveAs(new FileInfo(filePath));

                Process.Start(filePath);
            }
        }

        public static void Generate(string templatePath, TaxOrder data)
        {
            var result = new MemoryStream();
            ExcelPackage ePack = new ExcelPackage();

                MemoryStream mStream = new MemoryStream();

                //FileInfo xFile = new FileInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), @"Work\TaxOrderTemplates.xlsx"));
                FileInfo xFile = new FileInfo(templatePath);

                using (ExcelPackage package = new ExcelPackage(xFile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets["TaxOrderTemplate"];


                    worksheet.Cells["F1"].Value = worksheet.Cells["F27"].Value = string.Concat("#", data.OrderID);

                    worksheet.Cells["G8"].Value = worksheet.Cells["G34"].Value =  data.ExactDate;
                    worksheet.Cells["C5"].Value = worksheet.Cells["C31"].Value = data.ClientId;
                    worksheet.Cells["C13"].Value = worksheet.Cells["C39"].Value = data.Description;
                    worksheet.Cells["G10"].Value = worksheet.Cells["G36"].Value = data.Amount;
                    worksheet.Cells["F17"].Value = worksheet.Cells["F43"].Value = data.AmountInWords;
                    worksheet.Cells["C8"].Value = worksheet.Cells["C34"].Value = data.ReceiverName;
                    worksheet.Cells["C10"].Value = worksheet.Cells["C36"].Value = data.ReceiverId;
                    worksheet.Cells["G12"].Value = worksheet.Cells["G38"].Value = data.Currency;
                    worksheet.Cells["G23"].Value = worksheet.Cells["G49"].Value = data.ResponsibleUser;
                    worksheet.Cells["C3"].Value = worksheet.Cells["C29"].Value = data.ClientName;


                    ePack.Workbook.Worksheets.Add(new Random().Next().ToString(), worksheet);

                    //package.SaveAs(new FileInfo(Path.Combine(saveFolderPath + (new Random().Next()).ToString() + "file.xlsx")));
                }

            var filePath = Path.Combine(Environment.GetEnvironmentVariable("temp"), Guid.NewGuid().ToString() + ".xlsx");

            ePack.SaveAs(new FileInfo(filePath));

            Process.Start(filePath);
        }

        public static long GetTimestamp()
        {
            DateTime dateTime = DateTime.MinValue;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://nist.time.gov/actualtime.cgi?lzbc=siqm9b");
            request.Method = "GET";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/6.0)";
            request.ContentType = "application/x-www-form-urlencoded";
            request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore); //No caching
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                StreamReader stream = new StreamReader(response.GetResponseStream());
                string html = stream.ReadToEnd();//<timestamp time=\"1395772696469995\" delay=\"1395772696469995\"/>
                string time = Regex.Match(html, @"(?<=\btime="")[^""]*").Value;
                long milliseconds = Convert.ToInt64(time);
                return milliseconds;
            }
            else
                return 0;
        }

        public static DataTable ToDataTable<T>(List<T> items, PropertyInfo[] props = null)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            if (props != null)
                Props = props;

            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
    }
}
