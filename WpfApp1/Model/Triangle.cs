using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model;

namespace WpfApp1.Model
{
    public class Triangle: MyShape
    {
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public bool Filled { get; set; }
    }
}
