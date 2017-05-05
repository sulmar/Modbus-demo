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
        private readonly IAnalogService AnalogService;

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

            this.AnalogService = new TempService(hostname, port, slaveId);

            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = TimeSpan.FromSeconds(2);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Task.Run(()=>GetAsync());
        }

        public MeasureViewModel(IAnalogService analogService)
        {
            this.AnalogService = analogService;
        }

        public void Get()
        {
            Measure = AnalogService.Get();
        }

        public async Task GetAsync()
        {
            Measure = await AnalogService.GetAsync();
        }

    }
}
