using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NeecTrader
{
    class TextBoxLogger : ILogger
    {
        TextBox textBox;

        string logs;

        public TextBoxLogger(TextBox textBox)
        {
            this.textBox = textBox;
        }

        public void Write(string msg)
        {
            this.logs += msg;
            UpdateTextBlock();
        }

        public void WriteLine(string msg)
        {
            this.logs += msg + "\n";
            UpdateTextBlock();
        }

        private void UpdateTextBlock()
        {
            this.textBox.Text = this.logs;
        }
    }
}
