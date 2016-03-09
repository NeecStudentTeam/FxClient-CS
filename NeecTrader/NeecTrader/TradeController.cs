using MT4DLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeecTrader
{
    public class TradeController
    {

        ILogger logger;

        IMT4 mt4;

        /// <summary>
        /// インスタンス化。
        /// </summary>
        /// <param name="useStub">ネットワーク通信をしないスタブを使用する場合はtrue</param>
        public TradeController(bool useStub = false)
        {
            if (useStub)
            {
                this.mt4 = new MT4Stub();
            }
            else
            {
                this.mt4 = new MT4();
            }
            this.logger = new PrintLogger();
        }

        public int Connect()
        {
            return this.mt4.Connect();
        }

        public void Disconnect()
        {
            mt4.Disconnect();
            mt4 = null;
        }

        public bool isConnected()
        {
            return this.mt4.isConnected();
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

        public void UpdateRateSymbol(Symbol symbol)
        {
            symbol.ask = this.GetAsk(symbol);
            symbol.bid = this.GetBid(symbol);
        }

        public Ticket OrderSend(Symbol symbol, int cmd, double volume, double price, int slippage, double stoploss, double takeprofit, string comment, int magic, MT4DateTime expiration, MT4Color arrow_color)
        {
            int number = mt4.OrderSend(symbol.symbolName, cmd, volume, price, slippage, stoploss, takeprofit, comment, magic, expiration, arrow_color);


            if(number != -1)
            {
                Ticket ticket = new Ticket() { number = number, cmd = cmd == 0 ? "Ask" : "Bid", symbol = symbol, time = DateTime.Now, rate = cmd == 0 ? this.GetAsk(symbol) : this.GetBid(symbol), status = Ticket.TicketStatus.STATUS_ORDERED};
                return ticket;
            }
            else
            {
                return null;
            }
        }

        public bool OrderClose(Ticket ticket, double lots, double price, int slippage, MT4Color arrow_color)
        {
            bool result = this.mt4.OrderClose(ticket.number, lots, price, slippage, arrow_color);
            if (result)
            {
                ticket.status = Ticket.TicketStatus.STATUS_CLOSED;
                return true;
            }
            else
            {
                return false;
            }
        }

        public double GetBid(Symbol symbol)
        {
            return mt4.MarketInfo(symbol.symbolName, 9);
        }

        public double GetAsk(Symbol symbol)
        {
            return mt4.MarketInfo(symbol.symbolName, 10);
        }

        public void SetLogger(ILogger logger)
        {
            this.logger = logger;
        }

        public Double GetOrderProfit(Ticket ticket)
        {
            mt4.OrderSelect(ticket.number,1,0);
            return mt4.OrderProfit();
        }


    }

}