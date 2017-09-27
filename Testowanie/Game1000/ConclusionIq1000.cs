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
            ConclusionAboutGame conclusionAboutGame = new ConclusionAboutGame(3, 1,Date.simpleCards);
             var tmp= conclusionAboutGame.GetStates();
            int AmountCards = 0;
            HashSet<Karta> usedcards = new HashSet<Karta>();
            foreach (var item in tmp.cards.SelectMany(X=>X))
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
        public void ConclusionGetState_CasseRestryction()
        {
            ConclusionAboutGame conclusionAboutGame = new ConclusionAboutGame(3, 1,Date.simpleCards8);
            conclusionAboutGame.SetEndAction();
            conclusionAboutGame.PlayerConclusion[0].TheMostFigureInColor[0] = Karta.K2;
            conclusionAboutGame.PlayerConclusion[2].TheMostFigureInColor[3] = Karta.K2;
            var tmp = conclusionAboutGame.GetStates();
            Assert.IsFalse(tmp.cards[0].Any(X => ObsugaKart.Kolor(X) == Karta.trelf));
            Assert.IsFalse(tmp.cards[2].Any(X => ObsugaKart.Kolor(X) == Karta.pik));


        }
    }
}
