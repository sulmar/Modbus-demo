using Modbus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modbus.Services
{
    public interface IBinaryInputService
    {
        Vector Get();

        Task<Vector> GetAsync();
    }
}
