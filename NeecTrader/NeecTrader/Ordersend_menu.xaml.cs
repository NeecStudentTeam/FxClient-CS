using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace NeecTrader
{
    /// <summary>
    /// Ordersend_menu.xaml の相互作用ロジック
    /// </summary>
    public partial class OrderSend_menu : Window
    {

        TradeController trade;



        public OrderSend_menu()
        {
            InitializeComponent();

            DataContext = new TestBindObject() { A = 10 };

            //トレードコントローラインスタンス化。
            this.trade = new TradeController(true);

            Symbol[] Symbols = TradeController.GetSymbols();

            this.SymbolPair.SelectedIndex = 0;
            this.Lot.SelectedIndex = 0;

            this.SymbolPair.ItemsSource = Symbols;

        }


        //注文の種類コンボボックス変更イベント
        private void OrderCmd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            // 選択されている項目を取得
            string selectedItem = ((ComboBoxItem)OrderCmd.SelectedItem).Content.ToString();

            if (selectedItem == "成功注文")
            {
                
                Success.Visibility = Visibility.Visible;
                Limit.Visibility = Visibility.Collapsed;
            }
            else if (selectedItem == "指値または逆指値注文")
            {
                Limit.Visibility = Visibility.Visible;
                Success.Visibility = Visibility.Collapsed;
            }


        }

        //成功売り注文ボタンクリックイベント
        private void Bid_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("成功売り注文するンゴ");
        }

        //成功買い注文ボタンクリックイベント
        private void Ask_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("成功買い注文するンゴ");
        }

        //指値発注ボタンクリックイベント
        private void OrderSend_Limit_Click(object sender, RoutedEventArgs e)
        {
            var bindingData = DataContext as TestBindObject;
            //MessageBox.Show("a");
            if (bindingData != null)
            {
                MessageBox.Show("内部値は : " + bindingData.A.ToString());
            }
        }

        //シンボルペアコンボボックスの値変更イベント
        private void SymbolPair_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
