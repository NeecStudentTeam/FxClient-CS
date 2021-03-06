﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NeecTrader
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {

        //ObservableCollection<Symbol> symbolList;
        ObservableCollection<TicketNumber> ticketNumberList;

        TradeController trade;

        public MainWindow()
        {
            InitializeComponent();

            //トレードコントローラインスタンス化。
            this.trade = new TradeController(true);


            Symbol[] Symbols = TradeController.GetSymbols();

            //アップデート
            foreach(Symbol symbol in Symbols)
            {
                trade.UpdateRateSymbol(symbol);
            }

            ticketNumberList = new ObservableCollection<TicketNumber> {
                new TicketNumber{check = true ,ticketnumber = 11111, time = new DateTime(2016,01,15), cmd = "Ask" , lot = 0.10, symbol = "USDJPY" , rate = 111.111,
                stoploss = 1111,takeprofit = 2222, nowrate = 222.222, profit = 9999},
                new TicketNumber{check= true ,ticketnumber = 11112, time = new DateTime(2016,01,15), cmd = "Bsk" , lot = 0.10, symbol = "EURJPY" , rate = 111.111,
                stoploss = 1111,takeprofit = 2222, nowrate = 222.222, profit = 9999},
                new TicketNumber{check = false ,ticketnumber = 11113, time = new DateTime(2016,01,15), cmd = "Ask" , lot = 0.10, symbol = "USDJPY" , rate = 111.111,
                stoploss = 1111,takeprofit = 2222, nowrate = 222.222, profit = 9999},
                 new TicketNumber{check = true ,ticketnumber = 11113, time = new DateTime(2016,01,15), cmd = "Ask" , lot = 0.10, symbol = "USDJPY" , rate = 111.111,
                stoploss = 1111,takeprofit = 2222, nowrate = 222.222, profit = 9999},
                 new TicketNumber{check = true ,ticketnumber = 11114, time = new DateTime(2016,01,15), cmd = "Ask" , lot = 0.10, symbol = "USDJPY" , rate = 111.111,
                stoploss = 1111,takeprofit = 2222, nowrate = 222.222, profit = 9999},
                 new TicketNumber{check = true ,ticketnumber = 11115, time = new DateTime(2016,01,15), cmd = "Ask" , lot = 0.10, symbol = "USDJPY" , rate = 111.111,
                stoploss = 1111,takeprofit = 2222, nowrate = 222.222, profit = 9999},
                 new TicketNumber{check = true ,ticketnumber = 11116, time = new DateTime(2016,01,15), cmd = "Ask" , lot = 0.10, symbol = "USDJPY" , rate = 111.111,
                stoploss = 1111,takeprofit = 2222, nowrate = 222.222, profit = 9999},
            };





            // DataGridに設定する
            this.Symbol.ItemsSource = Symbols;
            this.TicketNumber.ItemsSource = ticketNumberList;

            //口座情報　あとで
            this.AccountInformation.Text = "口座情報　とりあえず";

            


            
        }


        private void ask_ordersend_Click(object sender, RoutedEventArgs e)
        {
                      
            MessageBox.Show("買い注文するンゴ");
 

        }

        private void bid_ordersend_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("売り注文するンゴ");
        }

        private void orderclose_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("決済するンゴ");
        }

        private void ordersend_menu(object sender, RoutedEventArgs e)
        {
            OrderSend_menu dialog = new OrderSend_menu();

            dialog.Owner = this;
            //モーダルウィンドウ
            dialog.ShowDialog();

        }
        private void orderclose_menu(object sender, RoutedEventArgs e)
        {
            OrderClose_menu dialog = new OrderClose_menu();

            dialog.Owner = this;
            //モーダルウィンドウ
            dialog.ShowDialog();

        }


        
        private void chart_show(object sender, RoutedEventArgs e)
        {
            TabItem tab = new TabItem();

            //選択したセルの値を取得
            string symbol = Symbol.CurrentCell.Item.ToString();                
            //ヘッダー名
            tab.Header = symbol;
            tab.Name = symbol;
            //新しいタブアイテムを追加
            Chart.Items.Add(tab);


        }

        private void ChartClose(object sender, RoutedEventArgs e)
        {

            int i = Chart.SelectedIndex;

            //チャートにタブアイテムがあるか調べる
            if(Chart.HasItems == false){
                MessageBox.Show("チャートがありません");

            }
            else
            {
                Chart.Items.RemoveAt(i);

            }
           
        } 
        
    }
}