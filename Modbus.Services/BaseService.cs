using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modbus.Services
{
    public abstract class BaseService
    {
        protected readonly string hostname;
        protected readonly int port;
        protected readonly byte slaveId;

        public BaseService(string hostname, int port, byte slaveId)
        {
            this.hostname = hostname;
            this.port = port;
            this.slaveId = slaveId;
        }
    }
}
