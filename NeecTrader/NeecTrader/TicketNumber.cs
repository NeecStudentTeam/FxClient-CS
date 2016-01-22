using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeecTrader
{
    public class TicketNumber
    {
        public bool check { get; set; }
        public int ticketnumber { get; set; }
        public DateTime time { get; set; }
        public string cmd { get; set; }
        public double lot { get; set; }
        public string symbol { get; set; }
        public double rate { get; set; }
        public double stoploss { get; set; }
        public double takeprofit { get; set; }
        public double nowrate { get; set; }
        public double profit { get; set; }

    }
}
