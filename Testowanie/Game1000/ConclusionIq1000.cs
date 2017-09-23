using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Karty;
using GraKarciana;
namespace ClassLibrary1.Game1000
{
    [TestClass]
    public class ConclusionIq1000
    {
        static Karta[] simpleCards = new Karta[] { Karta.K9, Karta.Dupek, Karta.Dama, Karta.K10, Karta.Król, Karta.As, ObsugaKart.StwórzKarte(Karta.K9, Karta.karo) };
        [TestMethod]
        public void ConclusionGetState()
        {
            ConclusionAboutGame conclusionAboutGame = new ConclusionAboutGame(3, 1,simpleCards);
            conclusionAboutGame.GetStates();

        }
    }
}
