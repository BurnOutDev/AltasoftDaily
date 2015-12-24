using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Controls;

namespace AltasoftDaily.UserInterface.WindowsForms.Controls
{
    public partial class ChoosePropertiesControl : MetroUserControl
    {
        public ChoosePropertiesControl(List<Tuple<string, bool>> items)
        {
            InitializeComponent();

            foreach (var item in items)
            {
                checkedListBox1.Items.Add(item.Item1, item.Item2);
            }
        }
    }
}
