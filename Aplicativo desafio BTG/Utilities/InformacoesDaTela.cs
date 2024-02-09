using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicativoDesafioBTG.Model
{
    static class InformacoesDaTela
    {
        public static double Largura()
        {
            return DeviceDisplay.Current.MainDisplayInfo.Width;
        }
        public static double Altura()
        {
            return DeviceDisplay.Current.MainDisplayInfo.Height;
        }
    }
}
