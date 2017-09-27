using GraKarciana;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    internal class Date
    {

        public static Karta[] simpleCards = new Karta[] { Karta.K9, Karta.Dupek, Karta.Dama, Karta.K10, Karta.Król, Karta.As, ObsugaKart.StwórzKarte(Karta.K9, Karta.karo) };
        public static Karta[] simpleCards8 = new Karta[] { Karta.K9, Karta.Dupek, Karta.Dama, Karta.K10, Karta.Król, Karta.As, ObsugaKart.StwórzKarte(Karta.K9, Karta.karo), ObsugaKart.StwórzKarte(Karta.K10, Karta.karo) };

    }
}
