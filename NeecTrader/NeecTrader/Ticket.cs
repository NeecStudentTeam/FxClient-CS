using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MT4CScliant
{
    // 表示する個々のデータ（データバインド可能）
    public class Ticket : INotifyPropertyChanged
    {
        //Ticketプロパティ
        int _number;
        public int Number
        {
            get { return _number; }
            set { _number = value; OnPropertyChanged("Number"); }
        }

        //Symbolプロパティ
        SymbolPair _symbol;
        public SymbolPair Symbol
        {
            get { return _symbol; }
            set { _symbol = value; OnPropertyChanged("symbol"); }
        }

        //Cmdプロパティ
        string _cmd;
        public string Cmd
        {
            get { return _cmd; }
            set { _cmd = value; OnPropertyChanged("Cmd"); }
        }

        //Rateプロパティ
        double _rate;
        public double Rate
        {
            get { return _rate; }
            set { _rate = value; OnPropertyChanged("Rate"); }
        }
        

        // INotifyPropertyChangedインターフェースの実装
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string pName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(pName));
        }

        //Statusプロパティ
        TicketStatus _status;
        public TicketStatus Status
        {
            get { return _status; }
            set { _status = value; OnPropertyChanged("Status"); }
        }

        //Profitプロパティ
        Double _profit;
        public Double Profit
        {
            get { return _profit; }
            set { _profit = value; OnPropertyChanged("Profit"); }
        }


        //Dateプロパティ
        DateTime _date;
        public DateTime date
        {
            get { return _date; }
            set { _date = value; OnPropertyChanged("Date"); }
        }


        public enum TicketStatus
        {
            STATUS_ORDERED,
            STATUS_CLOSED
        }
    }

    // 表示するデータのコレクション（データバインド可能）
    public class TicketNumberData : ObservableCollection<Ticket>
    {
        public TicketNumberData()
        {
            /*
            // 初期データ
            this.Add(new TicketNumber() { Ticket = "1", Symbol = "Red" ,Cmd = "Ask",Rate = 111111});
            this.Add(new TicketNumber() { Ticket = "2", Symbol = "Green",Cmd = "Bid" ,Rate =22222 });
            */
        }
    }
}
