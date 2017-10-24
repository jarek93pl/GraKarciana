using ClassLibrary1;
using GraKarciana;
using Karty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAplication
{
    class Program
    {
        /// <summary>
        /// aplikacja słóży do optymalizacji pod wzgledem wydajności
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ConclusionAboutGame cm = new ConclusionAboutGame(3, 1, Date.simpleCards8);
            cm.TransferedCard(ObsugaKart.StwórzKarte(Karta.Dama, Karta.kier), 0);
            cm.TransferedCard(ObsugaKart.StwórzKarte(Karta.Król, Karta.kier), 2);
            cm.MoveContext = MoveContext1000.ChoseCards;
            var a = IQ1000Game.GetIqState(cm);
        }
    }
}
