using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltasoftDaily.Domain
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public bool IsLockedOut { get; set; }
        public bool CanSubmit { get; set; }
        public bool CanViewDaily { get; set; }

        public Branch Branch { get; set; }
        public int DeptId { get; set; }
        public int AltasoftUserID { get; set; }

        public virtual ICollection<User> VisibleUsers { get; set; }
    }
}
