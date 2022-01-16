
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronometerAS
{
    public class Chronometer : IChronometer
    {
        private Stopwatch stopWatch;

        // collection of laps asdf asdf
        private List<string> laps;

        public Chronometer()
        {
            this.stopWatch = new Stopwatch();
            this.laps = new List<string>(); 
        }
        public string GetTime
        {
            get
            {
                return this.stopWatch.Elapsed.ToString(@"mm\:ss\.ffff");
            }
        }

        public List<string> Laps => this.laps;
        public string Lap()
        {
            string result = GetTime;
            this.laps.Add(result);
            return result;
        }

        public void Reset()
        {
           this.stopWatch.Reset();
            this.laps.Clear();  
        }

        public void Start()
        {
            this.stopWatch.Start();
        }

        public void Stop()
        {
            this.stopWatch.Stop();
        }
    }
}
