﻿using Karty;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraKarciana;
using System.Diagnostics;
namespace ClassLibrary1.Game1000
{
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class Iq1000
    {
        [TestMethod,Timeout(150000)]
        public void GetBidSum()
        {
            IQ1000Game iq = new IQ1000Game(3, 0.5f);
            ConclusionAboutGame cm = new ConclusionAboutGame(3, 1, Date.simpleCards8);
            cm.TransferedCard(ObsugaKart.StwórzKarte(Karta.Dama, Karta.kier), 0);
            cm.TransferedCard(ObsugaKart.StwórzKarte(Karta.Król, Karta.kier), 2);
            cm.MoveContext = MoveContext1000.ChoseCards;
            int w = iq.CalculateBidAmount(cm);
            Debug.WriteLine($"wartość do licytacji {w}");
            Assert.IsTrue(w < 150 && w > 70);
        }

        [TestMethod]
        public void GetWorstCard()
        {
            IQ1000Game iq = new IQ1000Game(3, 0.5f);
            ConclusionAboutGame cm = new ConclusionAboutGame(3, 1, Date.simpleCards8);
            cm.MoveContext = MoveContext1000.ChoseCards;
            
            List<Karta> z= iq.GetWorstCard( new List<Karta>() { ObsugaKart.StwórzKarte(Karta.As, Karta.kier), ObsugaKart.StwórzKarte(Karta.K10, Karta.kier) },2);
        }
        [TestMethod, Timeout(500000)]
        public void UsingIq()
        {
            ConclusionAboutGame cm = new ConclusionAboutGame(3, 1, Date.simpleCards8);
            cm.TransferedCard(ObsugaKart.StwórzKarte(Karta.Dama, Karta.kier), 0);
            cm.TransferedCard(ObsugaKart.StwórzKarte(Karta.Król, Karta.kier), 2);
            cm.MoveContext = MoveContext1000.ChoseCards;
            var a=IQ1000Game.GetIqState(cm.GetStates());
            int Sum = a.Item2.scoreInCurentGame.Sum();
            int AmountCards = a.Item2.cards.Sum(X => X.Count);
            Assert.IsTrue(Sum >= 120);
            Assert.AreEqual(0, AmountCards);

        }
    }
}
