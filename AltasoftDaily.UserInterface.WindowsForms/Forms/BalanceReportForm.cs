using AltasoftDaily.Core;
using AltasoftDaily.Domain;
using AltasoftDaily.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AltasoftDaily.UserInterface.WindowsForms.Forms
{
    public partial class BalanceReportForm : GridBaseForm
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? BranchID { get; set; }
        public decimal? BalCode { get; set; }

        public BalanceReportForm()
        {
            InitializeComponent();
        }

        private void pbxFilter_Click(object sender, EventArgs e)
        {
            var form = new BalanceReportFilterForm();

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                StartDate = form.StartDate;
                EndDate = form.EndDate;
                BranchID = form.BranchID;
                BalCode = form.BalCode;
            }

            var f = new LoadingForm();
            f.Show();
            f.BringToFront();

            gridData.DataSource = bindingSource;

            dataSet.Tables.Add(ConvertToDataTable<Kunchik>(DailyManagement.GetAccountsTest(StartDate, EndDate, BalCode, BranchID).ToList()));
            bindingSource.DataMember = "Table";

            foreach (DataGridViewColumn item in gridData.Columns)
            {
                if (item.Name.Contains("Specified"))
                    item.Visible = false;
            }

            f.Close();
        }

        private void pbxExport_Click(object sender, EventArgs e)
        {
            var converted = new SortableBindingList<object>(((SortableBindingList<Kunchik>)(gridData.DataSource)).Cast<object>().ToList());
            TaxOrderGenerator.ExportToExcel(converted, typeof(Kunchik));
        }

        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable("Table");
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        protected override void gridData_FilterStringChanged(object sender, EventArgs e)
        {
            this.bindingSource.Filter = this.gridData.FilterString;
        }

        protected override void gridData_SortStringChanged(object sender, EventArgs e)
        {
            this.bindingSource.Sort = this.gridData.SortString;
        }
    }
}
