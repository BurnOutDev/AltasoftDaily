using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AltasoftDaily.Domain.POCO.Logging;

namespace AltasoftDaily.Domain
{
    public class CommentView
    {
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Comment { get; set; }
    }
}
