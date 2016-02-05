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
        public string symbolName { get; set; }
        public double bid { get; set; }
        //public bool ordersend_bid { get; set; }
        public double ask { get; set; }
        //public bool ordersend_ask{ get; set; }


        public override string ToString()
        {
            return symbolName;
        }

        public static Symbol GetSymbolWithName(String symbolName)
        {

            var symbols = GetSymbols();

            for(int i = 0; i < symbols.Length; i++)
            {
                if(symbols[i].symbolName == symbolName)
                {
                    return symbols[i];
                }
            }

            return null;
        }

        public static Symbol[] GetSymbols()
        {
            Symbol[] symbols = new[]
            {
                new Symbol {symbolName = "USDCHF"},
                new Symbol {symbolName = "GBPUSD"},
                new Symbol {symbolName = "EURUSD"},
                new Symbol {symbolName = "USDJPY"},
                new Symbol {symbolName = "USDCAD"},
                new Symbol {symbolName = "AUDUSD"},
                new Symbol {symbolName = "EURGBP"},
                new Symbol {symbolName = "EURAUD"},
                new Symbol {symbolName = "EURCHF"},
                new Symbol {symbolName = "EURJPY"},
                new Symbol {symbolName = "GBPJPY"},
                new Symbol {symbolName = "GBPCHF"},
                new Symbol {symbolName = "CADJPY"},
                new Symbol {symbolName = "GBPJPY"},
                new Symbol {symbolName = "AUDNZD"},
                new Symbol {symbolName = "AUDCAD"},
                new Symbol {symbolName = "AUDCHF"},
                new Symbol {symbolName = "AUDJPY"},
                new Symbol {symbolName = "CHFJPY"},
                new Symbol {symbolName = "EURNZD"},
                new Symbol {symbolName = "EURCAD"},
                new Symbol {symbolName = "CADCHF"},
                new Symbol {symbolName = "NZDJPY"},
                new Symbol {symbolName = "NZDUSD"}

            };

            return symbols;
        }

    }
}
