using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT4CScliant
{
    public class SymbolPair
    {
        public string Symbol { set; get; }

        public override string ToString()
        {
            return Symbol;
        }
    }
}
