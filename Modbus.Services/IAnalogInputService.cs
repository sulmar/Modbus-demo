using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modbus.Services
{
    public interface IAnalogInputService
    {
        float Get();

        Task<float> GetAsync();
    }
}
