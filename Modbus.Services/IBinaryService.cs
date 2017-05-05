using Modbus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modbus.Services
{
    public interface IBinaryService
    {
        Vector Get();

        void Set(Vector value);

        Task<Vector> GetAsync();

        Task SetAsync(Vector value);

    }
}
