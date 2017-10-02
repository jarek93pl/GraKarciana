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
        [TestMethod]
        public void ConclusionGetState_CasseAllCards()
        {
            ConclusionAboutGame conclusionAboutGame = new ConclusionAboutGame(3, 1, Date.simpleCards);
            var tmp = conclusionAboutGame.GetStates();
            int AmountCards = 0;
            HashSet<Karta> usedcards = new HashSet<Karta>();
            foreach (var item in tmp.cards.SelectMany(X => X))
            {
                AmountCards++;
                if (usedcards.Contains(item))
                {
                    throw new NotImplementedException("jakaś karta się powtaża");
                }
                usedcards.Add(item);
            }
            Assert.AreEqual(AmountCards, 24);
        }
        [TestMethod]
        public void ConclusionGetState_CasseAllCardsTransfered()
        {
            Karta transfer0 = ObsugaKart.StwórzKarte(Karta.Dama, Karta.karo);
            Karta transfer2 = ObsugaKart.StwórzKarte(Karta.Król, Karta.karo);
            ConclusionAboutGame conclusionAboutGame = new ConclusionAboutGame(3, 1, Date.simpleCards);
            conclusionAboutGame.TransferedCard(transfer0, 0);
            conclusionAboutGame.TransferedCard(transfer2, 2);
            conclusionAboutGame.MoveContext = MoveContext1000.ChoseCards;
            var tmp = conclusionAboutGame.GetStates();
            int AmountCards = 0;
            HashSet<Karta> usedcards = new HashSet<Karta>();
            foreach (var item in tmp.cards.SelectMany(X => X))
            {
                AmountCards++;
                if (usedcards.Contains(item))
                {
                    throw new NotImplementedException("jakaś karta się powtaża");
                }
                usedcards.Add(item);
            }
            Assert.AreEqual(AmountCards, 24);
            Assert.IsTrue(tmp.cards[0].Any(X => X == transfer0));
            Assert.IsTrue(tmp.cards[2].Any(X => X == transfer2));
        }
        [TestMethod]
        public void ConclusionGetState_CasseRestryction()
        {
            ConclusionAboutGame conclusionAboutGame = new ConclusionAboutGame(3, 1,Date.simpleCards8);
            conclusionAboutGame.PlayerConclusion[0].TheMostFigureInColor[0] = Karta.K2;
            conclusionAboutGame.PlayerConclusion[2].TheMostFigureInColor[3] = Karta.K2;
            conclusionAboutGame.MoveContext = MoveContext1000.ChoseCards;
            var tmp = conclusionAboutGame.GetStates();
            Assert.IsFalse(tmp.cards[0].Any(X => ObsugaKart.Kolor(X) == Karta.trelf));
            Assert.IsFalse(tmp.cards[2].Any(X => ObsugaKart.Kolor(X) == Karta.pik));


        }
    }
}
