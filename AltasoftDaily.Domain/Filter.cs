using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltasoftDaily.Domain.POCO
{
    public class Filter
    {
        public Filter()
        {
            
        }
        [Key]
        public int FilterID { get; set; }
        public bool Enabled { get; set; }
        public bool IsDeptFilterEnabled { get; set; }
        public bool IsOperatorFilterEnabled { get; set; }
        public bool IsCustomerFilterEnabled { get; set; }
        public virtual ICollection<FilterData> FilterData { get; set; }
    }
}
