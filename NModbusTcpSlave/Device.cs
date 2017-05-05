
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace NModbusTcpSlave
{
    public class Device
    {
        private static Timer timer;



        public void Start()
        {
            timer = new Timer(12 * 1000);
        //    timer.Elapsed += (sender, args) =>
            timer.Start();
        }
    }
}
