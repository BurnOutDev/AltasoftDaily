using AltasoftDaily.Core;
using AltasoftDaily.Domain.POCO;
using AltasoftDaily.Helpers;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AltasoftDaily.UserInterface.WindowsForms
{
    public partial class GridBaseForm : MetroForm
    {
        private SortableBindingList<object> _data;
        public SortableBindingList<object> Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
                gridData.DataSource = _data;
            }
        }
        public Type DataType { get; set; }

        public GridBaseForm()
        {
            InitializeComponent();
        }

        public GridBaseForm(SortableBindingList<object> data, Type dataType)
        {
            InitializeComponent();

            DataType = dataType;
            Data = data;
        }

        private void gridData_SelectionChanged(object sender, EventArgs e)
        {
            var value = 0M;
            var count = gridData.SelectedCells.Count;
            lblCount.Text = count.ToString();

            foreach (dynamic item in gridData.SelectedCells)
            {
                try
                {
                    value += (decimal)item.Value;
                }
                catch
                {
                    lblSum.Text = "";
                    return;
                }
            }

            lblSum.Text = value.ToString();
        }

        private void gridData_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void gridData_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    gridData.Sort(gridData.Columns[e.ColumnIndex], ListSortDirection.Ascending);
                    break;
                case MouseButtons.Right:
                    for (int i = 0; i < gridData.Columns.Count; i++)
                    {
                        gridData.Columns[i].DefaultCellStyle.BackColor = Color.Empty;
                    }

                    gridData.Columns[e.ColumnIndex].Frozen = !gridData.Columns[e.ColumnIndex].Frozen;

                    for (int i = 0; i <= e.ColumnIndex; i++)
                    {
                        if (gridData.Columns[i].Frozen)
                            gridData.Columns[i].DefaultCellStyle.BackColor = Color.FloralWhite;
                    }
                    break;
                default:
                    break;
            }
        }

        public virtual void gridData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
