using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model;

namespace WpfApp1
{
    public class Line : MyShape
    {
        public string A { get; set; }
        public string B { get; set; }

        public static implicit operator Line(JObject v)
        {
            throw new NotImplementedException();
        }
    }
}
