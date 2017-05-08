using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modbus.Models;
using Modbus.Device;
using System.Diagnostics;
using System.Net.Sockets;

namespace Modbus.Services
{
    public class BinaryService : BaseService, IBinaryService
    {
        public BinaryService(string hostname, int port, byte slaveId)
            : base(hostname, port, slaveId)
        {
        }

        public Vector Get()
        {
            Trace.WriteLine($"Connecting to {hostname}:{port}");

            ushort startAddress = 2200;
            ushort numRegisters = 8;

            using (var client = new TcpClient(hostname, port))
            {
                ModbusIpMaster master = ModbusIpMaster.CreateIp(client);

                var inputs = master.ReadCoils(slaveId, startAddress, numRegisters);

                var vector = new Vector
                {
                    B0 = inputs[0],
                    B1 = inputs[1],
                    B2 = inputs[2],
                    B3 = inputs[3],
                };

                return vector;
            }
        }

        public async Task<Vector> GetAsync()
        {
            Trace.WriteLine($"Connecting to {hostname}:{port}");

            ushort startAddress = 2200;
            ushort numRegisters = 8;

            using (var client = new TcpClient(hostname, port))
            {
                ModbusIpMaster master = ModbusIpMaster.CreateIp(client);

                var inputs = await master.ReadCoilsAsync(slaveId, startAddress, numRegisters);

                var vector = new Vector
                {
                    B0 = inputs[0],
                    B1 = inputs[1],
                    B2 = inputs[2],
                    B3 = inputs[3],
                };

                return vector;
            }
        }

        public void Set(Vector value)
        {
            Trace.WriteLine($"Connecting to {hostname}:{port}");

            ushort startAddress = 2200;

            var inputs = new bool[] { value.B0, value.B1, value.B2, value.B3 };

            using (var client = new TcpClient(hostname, port))
            {
                ModbusIpMaster master = ModbusIpMaster.CreateIp(client);

                master.WriteMultipleCoils(slaveId, startAddress, inputs);
            }

        }

        public async Task SetAsync(Vector value)
        {
            Trace.WriteLine($"Connecting to {hostname}:{port}");

            ushort startAddress = 2200;

            var inputs = new bool[] { value.B0, value.B1, value.B2, value.B3 };

            using (var client = new TcpClient(hostname, port))
            {
                ModbusIpMaster master = ModbusIpMaster.CreateIp(client);

                await master.WriteMultipleCoilsAsync(slaveId, startAddress, inputs);
            }

        }
    }
}
