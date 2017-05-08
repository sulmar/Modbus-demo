using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modbus.Services
{
    public static class Converter
    {
        public static float ConvertToFloat(int lRegister, int hRegister)
        {
            byte l0 = (byte)lRegister,
             l1 = (byte)(lRegister >> 8);

            byte h0 = (byte)hRegister,
             h1 = (byte)(hRegister >> 8);

            byte[] newArray = new[] { h0, h1, l0, l1 };

            var result = BitConverter.ToSingle(newArray, 0);

            return result;
        }

        public static int[] ConvertToInt(float value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            int[] result = new int[2] { BitConverter.ToInt16(bytes, 0), BitConverter.ToInt16(bytes, 2) };

            return result;
        }
    }
}
