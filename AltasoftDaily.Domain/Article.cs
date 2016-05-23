using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltasoftDaily.Domain
{
    public class Article
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Tax { get; set; }
        public string Barcode { get; set; }
        public int Dept { get; set; }
        public string Flag { get; set; }
        public string Qunatity { get; set; }
    }
}
