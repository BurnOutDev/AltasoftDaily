using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltasoftDaily.Domain.POCO
{
    public class FormWindow
    {
        [Key]
        public int FormID { get; set; }
        public string FormName { get; set; }
    }
}
