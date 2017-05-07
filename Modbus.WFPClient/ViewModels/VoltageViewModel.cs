using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modbus.WFPClient.ViewModels
{
    public class VoltageViewModel : BaseViewModel
    {
        #region Output1

        private float _Output1;

        public float Output1
        {
            get { return _Output1; }
            set
            {
                _Output1 = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Output2

        private float _Output2;

        public float Output2
        {
            get { return _Output2; }
            set
            {
                _Output2 = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Output3

        private float _Output3;

        public float Output3
        {
            get { return _Output3; }
            set
            {
                _Output3 = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Output4

        private float _Output4;

        public float Output4
        {
            get { return _Output4; }
            set
            {
                _Output4 = value;
                OnPropertyChanged();
            }
        }

        #endregion

    }
}
