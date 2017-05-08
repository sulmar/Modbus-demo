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
    public class TempService : IAnalogService
    {
        private readonly string hostname;
        private readonly int port;
        private readonly byte slaveId;

        public TempService(string hostname, int port, byte slaveId)
        {
            this.hostname = hostname;
            this.port = port;
            this.slaveId = slaveId;
        }

        public float Get()
        {
            Trace.WriteLine($"Connecting to {hostname}:{port}");

            using (var client = new TcpClient(hostname, port))
            {
                ModbusIpMaster master = ModbusIpMaster.CreateIp(client);

                // Cyfrowy miernik tablicowy N30H
               // byte slaveId = 1;
                ushort startAddress = 7010;
                ushort numRegisters = 2;

                var analogInputs = master.ReadInputRegisters(slaveId, startAddress, numRegisters);

                var result = Converter.ConvertToFloat(analogInputs[0], analogInputs[1]);

                Console.WriteLine($"Result {result}");

                return result;
            }
        }

        public async Task<float> GetAsync()
        {
            Trace.WriteLine($"Connecting to {hostname}:{port}");

            using (var client = new TcpClient(hostname, port))
            {
                ModbusIpMaster master = ModbusIpMaster.CreateIp(client);

                // Cyfrowy miernik tablicowy N30H
                // byte slaveId = 1;
                ushort startAddress = 7010;
                ushort numRegisters = 2;

                var analogInputs = await master.ReadInputRegistersAsync(slaveId, startAddress, numRegisters);

                var result = Converter.ConvertToFloat(analogInputs[0], analogInputs[1]);

                Console.WriteLine($"Result {result}");

                return result;
            }
        }

        public void Set(float value)
        {
            throw new NotSupportedException();
        }

        public Task SetAsync(float value)
        {
            throw new NotSupportedException();
        }
    }
}
