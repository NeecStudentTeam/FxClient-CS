using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.Specialized;

namespace NeecTrader
{
	/// <summary>
	/// チャート用アクセッサのようなもの
	/// チャートに表示する用データ
	/// </summary>
	public class ChartPoint:INotifyCollectionChanged
	{
		public event NotifyCollectionChangedEventHandler CollectionChanged;
		public double X { set; get; }
		public double Y { set; get; }

		public ChartPoint()
		{
			this.X = 0;
			this.Y = 0;
		}

		public ChartPoint(double x, double y)
		{
			this.X = x;
			this.Y = y;
		}
		private void collectionNotify(NotifyCollectionChangedAction act, object item, int index)
		{
			if (CollectionChanged != null)
			{
				CollectionChanged(this, new NotifyCollectionChangedEventArgs(act, item, index));
			}
		}
	}
}
