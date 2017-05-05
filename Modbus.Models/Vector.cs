using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modbus.Models
{
    public class Vector : Base
    {
        private bool b0;

        public bool B0
        {
            get { return b0; }
            set { b0 = value;
                OnPropertyChanged();
            }
        }

        private bool b1;

        public bool B1
        {
            get { return b1; }
            set
            {
                b1 = value;
                OnPropertyChanged();
            }
        }

        private bool b2;

        public bool B2
        {
            get { return b2; }
            set
            {
                b2 = value;
                OnPropertyChanged();
            }
        }

        private bool b3;

        public bool B3
        {
            get { return b3; }
            set
            {
                b3 = value;
                OnPropertyChanged();
            }
        }

        private bool b4;

        public bool B4
        {
            get { return b4; }
            set
            {
                b4 = value;
                OnPropertyChanged();
            }
        }
    }
}
