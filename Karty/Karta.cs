using System;
using System.Collections.Generic;
using System.Linq;

namespace GraKarciana
{
    
    [Flags]
    public enum Karta : byte { trelf = 0, karo = 1, kier = 2, pik = 3, K2 = 0, K3 = 4, K4 = 8, K5 = 12, K6 = 16, K7 = 20, K8 = 24, K9 = 28, K10 = 32, Dupek = 36, Dama = 40, Król = 44, As = 48 };
    public static class ObsugaKart
    {
  
        const int MaskaKarty = ~3;
        public static Karta PobierzKarte(this Karta k)
        {
            return (Karta) ((int) k & MaskaKarty);
        }
        public static bool Kolor(this Karta k, Karta Kolor)
        {
            return (k & Karta.pik) == Kolor;
        }
        public static Karta Kolor(this Karta k)
        {
            return (k & Karta.pik);
        }
        public static List<Karta> WylousjMałąTalie()
        {
            List<Karta> zw = new List<Karta>();
            for (int i = 28; i < 52; i++)
            {
                zw.Add((Karta)i);
            }
            return zw;
        }
        public static List<Karta> WylousjTalie()
        {
            List<Karta> zw = new List<Karta>();
            for (int i = 0; i < 52; i++)
            {
                zw.Add((Karta)i);
            }
            return zw;
        }
    }
}