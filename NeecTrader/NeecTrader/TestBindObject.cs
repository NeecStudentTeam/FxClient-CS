using System.ComponentModel;

namespace NeecTrader
{
    
    internal class TestBindObject : INotifyPropertyChanged
    {
        private int a;
        public int A
        {
            get { return a; }
            set
            {
                a = value;
                NotifyPropertyChanged("A");
            }
        }

        private void NotifyPropertyChanged(string parameter)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(parameter));
        }
        public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };
    }
}