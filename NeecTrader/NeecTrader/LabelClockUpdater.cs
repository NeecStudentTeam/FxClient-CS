using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NeecTrader
{
    public class LabelClockUpdater : Updater
    {

        Label label;

        public LabelClockUpdater(Label label) : base(1000)
        {
            this.label = label;
        }

        override protected bool Update()
        {
            this.label.Dispatcher.BeginInvoke(
                new Action(() =>
                {
                    this.label.Content = DateTime.Now.ToString();
                })
            );
            return true;
        }
    }
}
