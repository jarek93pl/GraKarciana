using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Komputer.Matematyczne
{
    /// <summary>
    /// Błąd
    /// </summary>
    public class Pierwiastek
    {
        public static decimal Decimal(decimal a,int b)
        {
            decimal p1 = 0m;
            decimal p2 =Max(a,b) ;
            decimal rużnica;
            decimal Lidrzba;
            int e = 0;
            while (e<128)
            {
                e++;
                Lidrzba = 1;
                rużnica = (p2 - p1) / 2;


                if (rużnica == 0)
                    break;


                for (int i = 0; i < b; i++)
                {
                    Lidrzba *= (p1 + rużnica);
                
                }

                 if (Lidrzba > a)
                {
                    p2 -= rużnica;
                }
                else
                {
                    p1 += rużnica;
                }
            } 
            return p1;
                
        }
        public static decimal Max(decimal a, int lidrzna)
        {
            int l = Convert.ToInt32(a);
            int zwracana = 1;
            for (; Math.Pow(zwracana, lidrzna) <= l; zwracana++)
                ;
            return Convert.ToDecimal(zwracana);
        }

    }
    public class Porównywanie
    {

        public static int MIN(params int[] TABMIN)
        {
            int min = TABMIN[0];
            foreach (int w in TABMIN)
            {
                if (min > w)
                    min = w;
            }
            return min;
        }
        public static int Max(params int[] TABMIN)
        {
            int min = TABMIN[0];
            foreach (int w in TABMIN)
            {
                if (min < w)
                    min = w;
            }
            return min;
        }
    }

}
