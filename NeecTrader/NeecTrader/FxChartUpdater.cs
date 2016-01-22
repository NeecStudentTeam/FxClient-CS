using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms.DataVisualization.Charting;

namespace NeecTrader
{
    public class FxChartUpdater : Updater
    {

        const int MAX_HISTORY = 100;
        Queue<double> countHistoryAsk = new Queue<double>();
        Queue<double> countHistoryBid = new Queue<double>();

        System.Windows.Forms.DataVisualization.Charting.Chart chart;
        TradeController trade;
        Symbol currentSymbol = null;

        public FxChartUpdater(System.Windows.Forms.DataVisualization.Charting.Chart chart, TradeController trade) : base(1000)
        {
            this.chart = chart;
            this.trade = trade;
            InitChart();
        }

        protected override bool Update()
        {
            if (currentSymbol == null) return false;


            double bid = trade.GetBid(this.currentSymbol);
            double ask = trade.GetAsk(this.currentSymbol);


            //---------------------------------
            // 履歴に登録
            //---------------------------------

            countHistoryAsk.Enqueue(ask);
            countHistoryBid.Enqueue(bid);


            //------------------------------------------------
            // 履歴の最大数を超えていたら、古いものを削除する
            //------------------------------------------------
            while (countHistoryAsk.Count > MAX_HISTORY)
            {
                countHistoryAsk.Dequeue();
            }
            while (countHistoryBid.Count > MAX_HISTORY)
            {
                countHistoryBid.Dequeue();
            }


            //------------------------------------------------
            // グラフを再描画する
            //------------------------------------------------

            //別スレッドからチャートにアクセスする為、処理をそのスレッドににお願いする
            Application.Current.Dispatcher.BeginInvoke(
                //お願いする処理のDelegateをラムダ式で作成
                new Action(() =>
                {
                    //Chartを描画
                    this.ShowChart();
                }
            ));


            return true;
        }


        //***************************************************************************
        /// <summary> チャートを描画する
        /// </summary>
        /// <param name="chart"></param>
        //***************************************************************************

        private void ShowChart()
        {

            //-----------------------
            // チャートに値をセット
            //-----------------------

            //---------
            //Ask
            //---------

            chart.Series[0].Points.Clear();

            //現在のレートの履歴の一番大きいレートを取得
            double rateMax = countHistoryAsk.Max();
            //現在のレートの履歴の一番小さいレートを取得する為の変数。counteHistory.Min()だと初期値（0）が入ってしまうため。
            double rateMin = rateMax;

            //レートの履歴の数だけ実行
            foreach (double value in countHistoryAsk)
            {

                // データをチャートに追加
                chart.Series[0].Points.Add(new DataPoint(0, value));

                //値が0より大きく（初期値ではない）、尚且つ現在のrateMinより小さい時、rateMinを更新する。
                if (value > 0 && value < rateMin)
                {
                    //rateMinを更新
                    rateMin = value;
                }
            }

            //---------
            //Bid
            //---------

            chart.Series[1].Points.Clear();

            //現在のレートの履歴の一番大きいレートを取得
            rateMax = countHistoryBid.Max() > rateMax ? countHistoryBid.Max() : rateMax;
            //現在のレートの履歴の一番小さいレートを取得する為の変数。counteHistory.Min()だと初期値（0）が入ってしまうため。
            rateMin = rateMax;

            //レートの履歴の数だけ実行
            foreach (double value in countHistoryBid)
            {

                // データをチャートに追加
                chart.Series[1].Points.Add(new DataPoint(0, value));

                //値が0より大きく（初期値ではない）、尚且つ現在のrateMinより小さい時、rateMinを更新する。
                if (value > 0 && value < rateMin)
                {
                    //rateMinを更新
                    rateMin = value;
                }
            }

            //maxとminのレートが正しいか。正しい場合、チャートのY軸の範囲の設定をする
            if (rateMax > 0 && rateMin >= 0 && rateMin <= rateMax)
            {
                //上に余白が出来るように、1.0001をかけた物を設定
                chart.ChartAreas[0].AxisY.Maximum = rateMax * 1.01;
                //下に余白が出来るように、0.9999をかけた物を設定
                chart.ChartAreas[0].AxisY.Minimum = rateMin * 0.99;
            }

        }


        private void InitChart()
        {


            // ChartArea追加
            chart.ChartAreas.Add("ChartArea1");

            //列を適当なのを追加
            chart.Series.Add("Ask");
            chart.Series.Add("Bid");

            // チャート全体の背景色を設定
            chart.BackColor = Color.Black;
            chart.ChartAreas[0].BackColor = Color.Transparent;

            // チャート表示エリア周囲の余白をカットする
            chart.ChartAreas[0].InnerPlotPosition.Auto = false;
            chart.ChartAreas[0].InnerPlotPosition.Width = 100;  // 100%
            chart.ChartAreas[0].InnerPlotPosition.Height = 90;  // 90%(横軸のメモリラベル印字分の余裕を設ける)
            chart.ChartAreas[0].InnerPlotPosition.X = 8;        //X軸線8本
            chart.ChartAreas[0].InnerPlotPosition.Y = 0;        //Y軸線0本


            // X,Y軸情報のセット関数を定義
            Action<Axis> setAxis = (axisInfo) =>
            {
                // 軸のメモリラベルのフォントサイズ上限値を制限　⇒　上限8px
                axisInfo.LabelAutoFitMaxFontSize = 8;

                // 軸のメモリラベルの文字色をセット ⇒　白
                axisInfo.LabelStyle.ForeColor = Color.White;

                // 軸タイトルの文字色をセット(今回はTitle未使用なので関係ないが...)　⇒　白
                axisInfo.TitleForeColor = Color.White;

                // 軸の色をセット
                axisInfo.MajorGrid.Enabled = true;
                axisInfo.MajorGrid.LineColor = ColorTranslator.FromHtml("#008242");
                axisInfo.MinorGrid.Enabled = false;
                axisInfo.MinorGrid.LineColor = ColorTranslator.FromHtml("#008242");
            };


            // X,Y軸の表示方法を定義
            setAxis(chart.ChartAreas[0].AxisY);
            setAxis(chart.ChartAreas[0].AxisX);
            chart.ChartAreas[0].AxisX.MinorGrid.Enabled = true;
            chart.ChartAreas[0].AxisY.Maximum = 100;    // 縦軸の最大値を100にする
            chart.ChartAreas[0].AxisY.Minimum = 0;

            //グラフのアンチエイリアス処理の無効化
            chart.AntiAliasing = AntiAliasingStyles.None;

            // 折れ線グラフとして表示
            chart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            chart.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            // 線の色を指定
            chart.Series[0].Color = ColorTranslator.FromHtml("#ff7f50");
            chart.Series[1].Color = ColorTranslator.FromHtml("#4169e1");



            // 凡例を非表示,各値に数値を表示しない
            chart.Series[0].IsVisibleInLegend = false;
            chart.Series[0].IsValueShownAsLabel = true; //default = false

            chart.Series[1].IsVisibleInLegend = false;
            chart.Series[1].IsValueShownAsLabel = true; //default = false

            // チャートに表示させる値の履歴を全て0クリア
            while (countHistoryAsk.Count <= MAX_HISTORY)
            {
                countHistoryAsk.Enqueue(0);
            }

            // チャートに表示させる値の履歴を全て0クリア
            while (countHistoryBid.Count <= MAX_HISTORY)
            {
                countHistoryBid.Enqueue(0);
            }
        }
            

        public void Clear()
        {
            countHistoryAsk.Clear();
            while (countHistoryAsk.Count <= MAX_HISTORY)
            {
                countHistoryAsk.Enqueue(0);
            }

            countHistoryBid.Clear();
            while (countHistoryBid.Count <= MAX_HISTORY)
            {
                countHistoryBid.Enqueue(0);
            }
        }

        public void Start(Symbol symbol)
        {
            this.ChangeSymbol(symbol);
            this.Start();
        }

        public void ChangeSymbol(Symbol symbol)
        {
            currentSymbol = symbol;
            Clear();
        }
    }
}
