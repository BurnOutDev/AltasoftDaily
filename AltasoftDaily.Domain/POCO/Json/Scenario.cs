using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltasoftDaily.Domain.POCO.Json
{
    public class Scenario
    {
        public DateTime date { get; set; }
        public double principal { get; set; }
        public double interest { get; set; }
        public double balance { get; set; }
        public double? penalty { get; set; }
    }
}
