using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT4CScliant
{
    public interface ILogger
    {
        void Write(String msg);
        void WriteLine(String msg);
    }
}
