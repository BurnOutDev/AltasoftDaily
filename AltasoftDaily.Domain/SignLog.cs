using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltasoftDaily.Domain.POCO
{
    public class SignLog
    {
        [Key]
        public int OrderLogID { get; set; }
        public DateTime Date { get; set; }
        public User User { get; set; }

        public string InternalIP { get; set; }
        public string ExternalIP { get; set; }
        public string InternalUsername { get; set; }
        public string ExternalUsername { get; set; }
        public SignType SignType { get; set; }
    }
}

public enum SignType
{
    SignIn = 1,
    SignOut = 0
}