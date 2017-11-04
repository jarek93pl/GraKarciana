using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace Komputer.Kompresja
{
    public class Data
    {
        public static uint NaUInt(DateTime CzasUpłyniety)
        {
            uint zwracana = 0;
            zwracana += Convert.ToUInt32(CzasUpłyniety.Year - 1970);
            zwracana *= 12;
            zwracana += Convert.ToUInt32(CzasUpłyniety.Month - 1);
            zwracana *= 31;
            zwracana += Convert.ToUInt32(CzasUpłyniety.Day - 1);
            zwracana *= 24;
            zwracana += Convert.ToUInt32(CzasUpłyniety.Hour);
            zwracana *= 60;
            zwracana += Convert.ToUInt32(CzasUpłyniety.Minute);
            zwracana *= 60;
            zwracana += Convert.ToUInt32(CzasUpłyniety.Second);
            return zwracana;



        }
        public static DateTime NaDate(uint XXXX)
        {
            uint Dana = XXXX;
            UInt32[] Czasy = new UInt32[6];

            Czasy[0] = Dana % 60;

            Dana /= 60;
            Czasy[1] = Dana % 60;

            Dana /= 60;
            Czasy[2] = Dana % 24;

            Dana /= 24;
            Czasy[3] = (Dana % 31) + 1;

            Dana /= 31;
            Czasy[4] = (Dana % 12) + 1;

            Dana /= 12;
            Czasy[5] = (Dana + 1970);
            return DateTime.Parse(string.Format("{0}-{1}-{2} {3}:{4}:{5}", Czasy[5], Czasy[4], Czasy[3], Czasy[2], Czasy[1], Czasy[0]));

        }
    }
}