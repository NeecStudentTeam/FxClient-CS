using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MT4CScliant
{
    public abstract class Updater : IUpdater
    {
        Thread thread;

        int intervalMillisecond = 0;

        bool run = false;

        public Updater(int intervalMillisecond)
        {
            this.intervalMillisecond = intervalMillisecond;
        }

        protected abstract bool Update();

        public void Start(int intervalMillisecond)
        {
            this.intervalMillisecond = intervalMillisecond;
            this.Start();
        }

        public void Start()
        {
            this.run = true;

            thread = new Thread(new ThreadStart(() => {
                while(run)
                {
                    if(!this.Update())break;
                    Thread.Sleep(intervalMillisecond);
                }
            }));

            thread.Start();
        }

        public void Stop()
        {
            this.run = false;
        }
    }
}
