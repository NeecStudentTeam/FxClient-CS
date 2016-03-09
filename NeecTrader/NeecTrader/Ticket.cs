using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace NeecTrader
{
    // 表示する個々のデータ（データバインド可能）
    public class Ticket
    {
        public bool check { get; set; }
        public int number { get; set; }
        public DateTime time { get; set; }
        public string cmd { get; set; }
        public double lot { get; set; }
        public Symbol symbol { get; set; }
        public double rate { get; set; }
        public double stoploss { get; set; }
        public double takeprofit { get; set; }
        public double nowrate { get; set; }
        public double profit { get; set; }
        public TicketStatus status { get; set; }

        public enum TicketStatus
        {
            STATUS_ORDERED,
            STATUS_CLOSED
        }

    }
}
