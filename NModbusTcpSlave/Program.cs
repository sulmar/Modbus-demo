using Modbus.Data;
using Modbus.Device;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NModbusTcpSlave
{
    class Program
    {
        static void Main(string[] args)
        {
            ModbusTcpMasterWriteRegisters();

            ModbusTcpMasterReadRegisters();

            ModbusSeriaRTUMasterReadRegisters();

            var port = int.Parse(ConfigurationManager.AppSettings["Port"]);
            var unitId = byte.Parse(ConfigurationManager.AppSettings["UnitId"]);

            SlaveTest(unitId, port);
            
        }

        public static void ModbusSeriaRTUMasterReadRegisters()
        {
            using (SerialPort port = new SerialPort("COM2"))
            {
                // configure serial port
                port.BaudRate = 9600;
                port.DataBits = 8;
                port.Parity = Parity.None;
                port.StopBits = StopBits.Two;
                port.Open();

                IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(port);

                byte slaveId = 1;
                ushort startAddress = 7010;
                ushort numRegisters = 2;

                var analogInputs = master.ReadInputRegisters(slaveId, startAddress, numRegisters);

                var result = Converter.ConvertToFloat(analogInputs[0], analogInputs[1]);

                // read five registers		
                ushort[] registers = master.ReadHoldingRegisters(slaveId, startAddress, numRegisters);

                for (int i = 0; i < numRegisters; i++)
                {
                    Console.WriteLine($"Register {startAddress + i}={registers[i]}");
                }
            }

            // output: 
            // Register 1=0
            // Register 2=0
            // Register 3=0
            // Register 4=0
            // Register 5=0
        }

        public static void ModbusTcpMasterWriteRegisters()
        {
            var hostname = ConfigurationManager.AppSettings["Hostname"];
            var port = int.Parse(ConfigurationManager.AppSettings["TcpPort"]);

            Console.WriteLine($"Connecting to {hostname}:{port}");

            using (var client = new TcpClient(hostname, port))
            {
                ModbusIpMaster master = ModbusIpMaster.CreateIp(client);

                // Moduł wyjść binarnych SM4
                byte slaveId = 2;
                ushort startAddress = 2200;
                ushort numRegisters = 8;

                bool value = true;


                //master.WriteSingleCoil(slaveId, startAddress, value);

                var inputs = master.ReadCoils(slaveId, startAddress, numRegisters);

                inputs[0] = true;
                inputs[2] = true;
                master.WriteMultipleCoils(slaveId, startAddress, inputs);



            }
        }

        public static void ModbusTcpMasterReadRegisters()
        {
            var hostname = ConfigurationManager.AppSettings["Hostname"];
            var port = int.Parse(ConfigurationManager.AppSettings["TcpPort"]);

            Console.WriteLine($"Connecting to {hostname}:{port}");

            using (var client = new TcpClient(hostname, port))
            {
                ModbusIpMaster master = ModbusIpMaster.CreateIp(client);

                // Cyfrowy miernik tablicowy N30H
                byte slaveId = 1;
                ushort startAddress = 7010;
                ushort numRegisters = 2;

                var analogInputs = master.ReadInputRegisters(slaveId, startAddress, numRegisters);

                var result = Converter.ConvertToFloat(analogInputs[0], analogInputs[1]);


                Console.WriteLine($"Display: {result}");

                // read five registers		
                ushort[] registers = master.ReadHoldingRegisters(slaveId, startAddress, numRegisters);

                for (int i = 0; i < numRegisters; i++)
                {
                    Console.WriteLine($"Register {startAddress + i}={registers[i]}");
                }
            }

           
        }


        private static System.Timers.Timer timer = new System.Timers.Timer(3 * 1000);

        private static void SlaveTest(byte unitId, int port)
        {
            var TcpHost = IPAddress.Parse("127.0.0.1");

            var slaveListener = new TcpListener(TcpHost, port);
            using (var slave = ModbusTcpSlave.CreateTcp(unitId, slaveListener))
            {
               // slave.DataStore = DataStoreFactory.CreateDefaultDataStore();
                slave.DataStore.DataStoreWrittenTo += DataStore_DataStoreWrittenTo;
                slave.DataStore.DataStoreReadFrom += DataStore_DataStoreReadFrom;

                

                Thread slaveThread = new Thread(slave.Listen);
                slaveThread.IsBackground = true;

                slaveThread.Start();
                Console.WriteLine($"Slave #{unitId} listening on port {port}!");

                Console.ReadKey();
                // slave.Listen();
            }





        }

        private static void DataStore_DataStoreReadFrom(object sender, DataStoreEventArgs e)
        {
            var dataStore = sender as DataStore;

            Console.WriteLine($"Read {e.ModbusDataType} from start address {e.StartAddress}");

            // slave.DataStore.CoilDiscretes[0] = true;
        }


        private static void DataStore_DataStoreWrittenTo(object sender, DataStoreEventArgs e)
        {
            var dataStore = sender as DataStore;

            Console.WriteLine($"Written to {e.ModbusDataType} at start address {e.StartAddress}");


            if (e.ModbusDataType == ModbusDataType.Coil)
            {
                var c = dataStore.CoilDiscretes[1];

                if (c==true)
                {
                    // Start

                    timer.Start();

                  //  dataStore.HoldingRegisters[1] = --dataStore.HoldingRegisters[1];
                }
                else
                {
                    /// Stop
                    /// 
                    timer.Stop();
                }
                



            }

            else if (e.ModbusDataType == ModbusDataType.HoldingRegister)
            {
                var t = dataStore.HoldingRegisters[1];

                // Set time
            }



        }

        private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

            
            // timer.Stop();
        }

        



    }
}
