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

namespace AltasoftDaily.UserInterface.WindowsForms
{
    public partial class PropertiesEditForm : MetroForm
    {
        public static PropertiesEditForm ShowForm<T>(T entity)
        {
            var obj = new PropertiesEditForm();
            obj.propertyGrid1.SelectedObject = entity;
            return obj;
        }

        private PropertiesEditForm()
        {
            InitializeComponent();
        }

        private void PropertiesEditForm_Load(object sender, EventArgs e)
        {

        }
    }
}
