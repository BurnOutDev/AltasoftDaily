using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltasoftDaily.Domain.POCO
{
    public class FilterData
    {
        [Key]
        public int FilterInfoID { get; set; }
        public int ClientID { get; set; }
        public int DeptID { get; set; }
        public int OperatorID { get; set; }
    }
}
