using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraKarciana
{
    public class  ObsugaTysiąc
    {
        static int[] PunktyKart1000 = { 0, 10, 2, 3, 4, 11 };
        static int[] PunktyMeldunków= {60,80,100,40 };
        public static int WartościMeldunków(Karta k)
        {
            return PunktyMeldunków[(int)k];
        }
        public static int PunktacjaTysiąca(Karta k)
        {
            int karta = (int)k;
            karta -= (int)Karta.K9;
            karta /= 4;
            return PunktyKart1000[karta];
        }
    }
}
