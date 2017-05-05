using Modbus.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NModbusTcpMaster
{
    class Program
    {
        static void Main(string[] args)
        {
            MasterTest();


            Console.WriteLine("Press any key to exit.");

            Console.ReadKey();
        }

        private static void MasterTest()
        {

            byte unitId = 1;
            int port = 502;

            var masterClient = new TcpClient("127.0.0.1", port);

            using (var master = ModbusIpMaster.CreateIp(masterClient))
            {
                master.Transport.Retries = 0;

                // set time
                master.WriteSingleRegister(0, 120);

                // start
                master.WriteSingleCoil(unitId, 0, true);

                bool[] coils = master.ReadCoils(unitId, 0, 10);


                var holding = master.ReadHoldingRegisters(0, 1);

            }
        }
    }
}
