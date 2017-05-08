using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modbus.Services
{
    public class VoltageService : IAnalogOutputService
    {
        public void Set(float value)
        {
            throw new NotImplementedException();
        }

        public Task SetAsync(float value)
        {
            throw new NotImplementedException();
        }
    }
}
