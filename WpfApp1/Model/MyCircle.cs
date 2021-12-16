using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    public class MyCircle : MyShape
    {
        public string Center { get; set; }
        public string Radius { get; set; }
        public bool Filled { get; set; }

    }
}
