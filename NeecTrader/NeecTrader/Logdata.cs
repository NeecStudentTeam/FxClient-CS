using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeecTrader
{
    //ログ出力用クラス
    class Logdata
    {

        //新規注文でticket++;　int→ulongに変更(桁数増えたときにエラー出るから) 20桁 →10ケタ以上見たことないらしいからlongに変更
        public string ticket { get; set; }
        //取引の種類　BidかAsk
        public string ordertype { get; set; }
        //現状手入力想定string型 後々別途リストを作ってそこから引用　配列か何かで参照予定？　intに変更も考え中
        public string symbol { get; set; }
        //注文時のレート これはint確定かな→double型に変更(小数点の存在忘れてました)
        public double rate { get; set; }
        //現在のレート これもintでいいかな→double型に変更(小数点の存在忘れてました)
        //※追加済み　項目一つ増やして "hoge = now_rate - rate;" 等で注文時と現在のレートの比較もありじゃね？意見求 (動的処理)
        public double now_rate { get; set; }
        //pl = now_rate - rate;現在の損益表示(動的処理)
        private double _pl;

        public double pl
        {
            get
            {
                return now_rate - rate;
            }
            set
            {
                _pl = value;
            }

        }

        public int date { get; set; }

        

    }
}


//参考サイト
//[WPF]DataGridへのBindingに関する基本設計 http://oita.oika.me/2014/11/03/wpf-datagrid-binding/