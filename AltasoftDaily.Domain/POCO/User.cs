using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltasoftDaily.Domain.POCO
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public int AltasoftUserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PrivateNumber { get; set; }

        public bool IsLockedOut { get; set; }
        public bool CanSubmit { get; set; }
        public bool CanViewDaily { get; set; }
        public DateTime LastPasswordChange { get; set; }
        public bool ForceUserToChangePassword { get; set; }

        public int DeptID { get; set; }

        public virtual Filter Filter { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}
