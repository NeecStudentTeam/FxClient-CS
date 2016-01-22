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
        public OrderSend_menu()
        {
            InitializeComponent();

            this.tyuumon_syurui_combobox.SelectedIndex = 0;
            this.rot_combobox.SelectedIndex = 0;

        }



        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            // 選択されている項目を取得
            string selectedItem = ((ComboBoxItem)tyuumon_syurui_combobox.SelectedItem).Content.ToString();

            if (selectedItem == "成功注文")
            {
                seikou.Visibility = Visibility.Visible;
                gyakusasine.Visibility = Visibility.Collapsed;
            }
            else if (selectedItem == "指値または逆指値注文")
            {
                gyakusasine.Visibility = Visibility.Visible;
                seikou.Visibility = Visibility.Collapsed;
            }


        }

        private void Bid_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("成功売り注文するンゴ");
        }

        private void Ask_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("成功買い注文するンゴ");
        }

        private void OrderSend_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("発注するンゴ");
        }
    }
}
