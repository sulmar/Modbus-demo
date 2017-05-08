using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modbus.Services
{
    public interface IAnalogService
    {
        float Get();

        Task<float> GetAsync();

        void Set(float value);

        Task SetAsync(float value);

    }
}
