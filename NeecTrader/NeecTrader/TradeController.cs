using MT4DLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT4CScliant
{
    public class TradeController
    {
        static SymbolPair[] symbolPairs = new[]
            {
                new SymbolPair {Symbol = "USDCHF"},
                new SymbolPair {Symbol = "GBPUSD"},
                new SymbolPair {Symbol = "EURUSD"},
                new SymbolPair {Symbol = "USDJPY"},
                new SymbolPair {Symbol = "USDCAD"},
                new SymbolPair {Symbol = "AUDUSD"},
                new SymbolPair {Symbol = "EURGBP"},
                new SymbolPair {Symbol = "EURAUD"},
                new SymbolPair {Symbol = "EURCHF"},
                new SymbolPair {Symbol = "EURJPY"},
                new SymbolPair {Symbol = "GBPJPY"},
                new SymbolPair {Symbol = "GBPCHF"},
                new SymbolPair {Symbol = "CADJPY"},
                new SymbolPair {Symbol = "GBPJPY"},
                new SymbolPair {Symbol = "AUDNZD"},
                new SymbolPair {Symbol = "AUDCAD"},
                new SymbolPair {Symbol = "AUDCHF"},
                new SymbolPair {Symbol = "AUDJPY"},
                new SymbolPair {Symbol = "CHFJPY"},
                new SymbolPair {Symbol = "EURNZD"},
                new SymbolPair {Symbol = "EURCAD"},
                new SymbolPair {Symbol = "CADCHF"},
                new SymbolPair {Symbol = "NZDJPY"},
                new SymbolPair {Symbol = "NZDUSD"}

            };

        ILogger logger;

        MT4 mt4;

        List<Ticket> orderTicketList = new List<Ticket>();

        class MyMT4 : MT4
        {
            public MyMT4()
                : base()
            {

            }
        }

        public TradeController()
        {
            this.mt4 = new MyMT4();
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

        public static SymbolPair[] GetSymbolPairs()
        {
            return TradeController.symbolPairs;
        }

        public Ticket OrderSend(SymbolPair symbol, int cmd, double volume, double price, int slippage, double stoploss, double takeprofit, string comment, int magic, MT4DateTime expiration, MT4Color arrow_color)
        {
            int number = mt4.OrderSend(symbol.Symbol, cmd, volume, price, slippage, stoploss, takeprofit, comment, magic, expiration, arrow_color);


            if(number != -1)
            {
                Ticket ticket = new Ticket() { Number = number, Cmd = cmd == 0 ? "Ask" : "Bid", Symbol = symbol, date = DateTime.Now, Rate = cmd == 0 ? this.GetAsk(symbol) : this.GetBid(symbol), Status = Ticket.TicketStatus.STATUS_ORDERED};
                this.orderTicketList.Add(ticket);
                return ticket;
            }
            else
            {
                return null;
            }
        }

        public bool OrderClose(Ticket ticket, double lots, double price, int slippage, MT4Color arrow_color)
        {
            bool result = this.mt4.OrderClose(ticket.Number, lots, price, slippage, arrow_color);
            if (result)
            {
                ticket.Status = Ticket.TicketStatus.STATUS_CLOSED;
                this.orderTicketList.Remove(ticket);
                return true;
            }
            else
            {
                return false;
            }
        }

        public double GetBid(SymbolPair symbol)
        {
            return mt4.MarketInfo(symbol.Symbol,9);
        }

        public double GetAsk(SymbolPair symbol)
        {
            return mt4.MarketInfo(symbol.Symbol, 10);
        }

        public void SetLogger(ILogger logger)
        {
            this.logger = logger;
        }

        public List<Ticket> GetOrderTickets()
        {
            return new List<Ticket>(this.orderTicketList);
        }

        public Double GetOrderProfit(Ticket ticket)
        {
            mt4.OrderSelect(ticket.Number,1,0);
            return mt4.OrderProfit();
        }


    }

}