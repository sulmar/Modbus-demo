using Modbus.Device;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Modbus.Services
{
    public class VoltageService : BaseService, IAnalogOutputService
    {
        public VoltageService(string hostname, int port, byte slaveId)
            : base(hostname, port, slaveId)
        {
        }

        public void Set(float value)
        {
            Trace.WriteLine($"Connecting to {hostname}:{port}");

            ushort startAddress = 4100;

            using (var client = new TcpClient(hostname, port))
            {
                ModbusIpMaster master = ModbusIpMaster.CreateIp(client);

                var result = Converter.ConvertToUshort(value);

                ushort[] values = new ushort[2] { result[0], result[1] };

                master.WriteMultipleRegisters(slaveId, startAddress, values);
            }
        }

        public async Task SetAsync(float value)
        {
            Trace.WriteLine($"Connecting to {hostname}:{port}");

            ushort startAddress = 4100;

            using (var client = new TcpClient(hostname, port))
            {
                ModbusIpMaster master = ModbusIpMaster.CreateIp(client);

                var result = Converter.ConvertToUshort(value);

                ushort[] values = new ushort[2] { result[0], result[1] };

                await master.WriteMultipleRegistersAsync(slaveId, startAddress, values);
            }
        }
    }
}
