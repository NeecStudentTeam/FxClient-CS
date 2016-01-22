using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeecTrader
{
    public class PrintLogger : ILogger
    {
        public PrintLogger()
        {

        }

        public void Write(string msg)
        {
            System.Console.Write(msg);
        }

        public void WriteLine(string msg)
        {
            System.Console.WriteLine(msg);
        }
    }
}
