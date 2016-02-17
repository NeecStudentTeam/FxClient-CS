using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Controls.DataVisualization;
using System.Threading;
using System.Diagnostics;



namespace NeecTrader
{
	/// <summary>
	/// Chart描画用デリゲート
	/// </summary>
	/// <param name="chartDirection">チャートの座標</param>
	/// <returns></returns>
	delegate bool drawChart(ChartPoint chartDirection);

	public class FxChartUpdater : Updater
 	{
	
		//履歴登録用リスト
		ObservableCollection<ChartPoint> chart = new ObservableCollection<ChartPoint>();
		TradeController trade;
		Symbol currentSymbol = null;
		int ChartArea=0;
		Random r= new Random();

		public FxChartUpdater(ObservableCollection<ChartPoint> chart, TradeController trade)
			: base(1000)
		{
			this.chart = chart;
			this.trade = trade;

		}

		protected override bool Update()
		{
			//シンボルが選択されていないとfalseを返して終了
			if (currentSymbol == null) return false;


			//double bid = trade.GetBid(this.currentSymbol);
			//double ask = trade.GetAsk(this.currentSymbol);

			//------------------------------------------------
			// グラフを再描画する
			//------------------------------------------------

			//別スレッドからチャートにアクセスする為、処理をそのスレッドににお願いする
			//Application.Current		ドメインへのインスタンスを共有。
			//Dispatcher.BegenInvoke	パラメータにあるデリゲートを非同期に実行
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
			//askLineにaskを登録
			chart.Add(new ChartPoint(++ChartArea, trade.GetAsk(this.currentSymbol)));
//			chart.Add(new ChartPoint(++ChartArea, r.Next(6)+100+ChartArea));

			//---------
			//Bid
			//---------
			//bidLineにbidを登録

			
		}

		public void Start(Symbol symbol)
		{
			this.ChangeSymbol(symbol);
			this.Start();
		}

		public void ChangeSymbol(Symbol symbol)
		{
			currentSymbol = symbol;
		}
	}
}
