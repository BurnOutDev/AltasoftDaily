using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltasoftDaily.Domain.POCO.Logging
{
    public class ChangeLog : BaseModel
    {
        public ChangeLog(string name, int id, string oldValue, string newValue, User user)
        {
            EntityName = name;
            EntityID = id;
            OldValue = oldValue;
            NewValue = newValue;
            User = user;
        }

        public int ChangeLogID { get; set; }
        public string EntityName { get; set; }
        public int EntityID { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime LogDate { get; set; }
        public User User { get; set; }
    }
}
