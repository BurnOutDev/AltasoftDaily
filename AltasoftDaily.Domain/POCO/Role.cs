using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltasoftDaily.Domain.POCO
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }

        public virtual ICollection<FormWindow> Forms { get; set; }
        public virtual ICollection<User> Users { get; set; }

        public override string ToString()
        {
            return RoleName;
        }
    }
}
