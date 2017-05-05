using Modbus.Models;
using Modbus.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modbus.WFPClient.ViewModels
{
    public class BinaryViewModel : BaseViewModel
    {
        private readonly IBinaryService Service;

        private Vector _Vector;
        public Vector Vector
        {
            get
            {
                return _Vector;
            }

            set
            {
                _Vector = value;
                OnPropertyChanged();
            }
        }

        public BinaryViewModel()
        {
            this.Vector = new Vector();

            var hostname = ConfigurationManager.AppSettings["Hostname"];
            var port = int.Parse(ConfigurationManager.AppSettings["TcpPort"]);
            byte slaveId = 2;

            this.Service = new BinaryService(hostname, port, slaveId);

            Task.Run(() => Get());
        }

        public async void Get()
        {
            Vector = await this.Service.GetAsync();
        }

        public async Task Set()
        {
            await this.Service.SetAsync(Vector);
        }

    }
}
