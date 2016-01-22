using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeecTrader
{
    // DataGridに表示するデータ
    public class Symbol
    {
        public bool check { get; set; }
        public string symbol { get; set; }
        public double bid { get; set; }
        //public bool ordersend_bid { get; set; }
        public double ask { get; set; }
        //public bool ordersend_ask{ get; set; }

    }
}
