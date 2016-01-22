using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeecTrader
{
    interface IUpdater
    {
        void Start(int intervalMillisecond);
        void Stop();
        void Start();
    }
}
