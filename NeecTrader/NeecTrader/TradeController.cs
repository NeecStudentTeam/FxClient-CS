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

        List<Ticket> orderTicketList = new List<Ticket>();

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
                new Symbol {symbol = "USDCHF"},
                new Symbol {symbol = "GBPUSD"},
                new Symbol {symbol = "EURUSD"},
                new Symbol {symbol = "USDJPY"},
                new Symbol {symbol = "USDCAD"},
                new Symbol {symbol = "AUDUSD"},
                new Symbol {symbol = "EURGBP"},
                new Symbol {symbol = "EURAUD"},
                new Symbol {symbol = "EURCHF"},
                new Symbol {symbol = "EURJPY"},
                new Symbol {symbol = "GBPJPY"},
                new Symbol {symbol = "GBPCHF"},
                new Symbol {symbol = "CADJPY"},
                new Symbol {symbol = "GBPJPY"},
                new Symbol {symbol = "AUDNZD"},
                new Symbol {symbol = "AUDCAD"},
                new Symbol {symbol = "AUDCHF"},
                new Symbol {symbol = "AUDJPY"},
                new Symbol {symbol = "CHFJPY"},
                new Symbol {symbol = "EURNZD"},
                new Symbol {symbol = "EURCAD"},
                new Symbol {symbol = "CADCHF"},
                new Symbol {symbol = "NZDJPY"},
                new Symbol {symbol = "NZDUSD"}

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
            int number = mt4.OrderSend(symbol.symbol, cmd, volume, price, slippage, stoploss, takeprofit, comment, magic, expiration, arrow_color);


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

        public double GetBid(Symbol symbol)
        {
            return mt4.MarketInfo(symbol.symbol, 9);
        }

        public double GetAsk(Symbol symbol)
        {
            return mt4.MarketInfo(symbol.symbol, 10);
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