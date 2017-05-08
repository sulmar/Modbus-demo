using Modbus.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modbus.WFPClient.ViewModels
{
    public class MeasureViewModel : BaseViewModel
    {
        private readonly IAnalogInputService AnalogInputService;

        #region Measure

        private float _Measure;

        public float Measure
        {
            get { return _Measure; }
            set
            {
                _Measure = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public MeasureViewModel()
        {
            var hostname = ConfigurationManager.AppSettings["Hostname"];
            var port = int.Parse(ConfigurationManager.AppSettings["TcpPort"]);
            byte slaveId = 1;

            this.AnalogInputService = new TemperatureService(hostname, port, slaveId);

            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = TimeSpan.FromSeconds(2);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Task.Run(()=>GetAsync());
        }

        public MeasureViewModel(IAnalogInputService analogInputService)
        {
            this.AnalogInputService = analogInputService;
        }

        public void Get()
        {
            Measure = AnalogInputService.Get();
        }

        public async Task GetAsync()
        {
            Measure = await AnalogInputService.GetAsync();
        }

    }
}
