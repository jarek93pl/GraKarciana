using Karty;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    [TestClass]
    public class Iq1000State
    {
        [TestMethod]
        public void TestEquals()
        {
            StateGame1000 s = new StateGame1000(3);
            StateGame1000 z = new StateGame1000(3);
            StateGame1000 a = new StateGame1000(3);
            s.cards.First().AddRange(new GraKarciana.Karta[] { GraKarciana.Karta.As, GraKarciana.Karta.Dupek });
            z.cards.First().AddRange(new GraKarciana.Karta[] { GraKarciana.Karta.Dupek, GraKarciana.Karta.As });
            a.cards.First().AddRange(new GraKarciana.Karta[] { GraKarciana.Karta.Dupek, GraKarciana.Karta.As });
            a.Player = 1;
            Assert.AreNotEqual(a, z);
            Assert.AreEqual(s, z);
            Assert.AreNotEqual(a.GetHashCode(), z.GetHashCode());
            Assert.AreEqual(s.GetHashCode(), z.GetHashCode());
        }
        [TestMethod]
        public void GetMove()
        {
            StateGame1000 s = new StateGame1000(3);
            s.cards[1].AddRange(new GraKarciana.Karta[] { GraKarciana.Karta.As, GraKarciana.Karta.Dupek, GraKarciana.Karta.Dama });
            s.Player = 1;
            int CountBeforeAction = s.cards[1].Count;
            var result= s.GetMove(GraKarciana.Karta.Dama);
            Assert.AreEqual(CountBeforeAction, s.cards[1].Count);
            Assert.AreEqual(CountBeforeAction-1, result.Item2.cards[1].Count);
        }
        [TestMethod]
        public void RateGame()
        {
            StateGame1000 s = new StateGame1000(3);
            s.scoreInCurentGame[0] = 500;
            s.scoreInCurentGame[1] = 100;
            s.scoreInCurentGame[2] = 100;
            Assert.IsTrue(s.RateStates(0) > 0);
            Assert.IsTrue(s.RateStates(1) < 0);
        }
        [TestMethod]
        public void GetState()
        {

            StateGame1000 a = new StateGame1000(3);
            {
                a.cards.First().AddRange(new GraKarciana.Karta[] { GraKarciana.Karta.As, GraKarciana.Karta.Dupek, GraKarciana.Karta.Król });
                var z = a.GetStates();
                Assert.AreEqual(z.Count, 3);
                Assert.AreEqual(z.First().Item2.cards.First().Count, 2);
            }
            {
                a = a.SetTable(new GraKarciana.Karta[] { GraKarciana.Karta.Dama });
              
                var b = a.GetStates();
                Assert.AreEqual(b.Count, 2);
                Assert.AreEqual(b.First().Item2.cards.First().Count, 2);
            }
        }

        [TestMethod]
        public void LoadMove_CaseFirstCard()
        {
            StateGame1000 a = new StateGame1000(3);
            a.cards.First().AddRange(new GraKarciana.Karta[] { GraKarciana.Karta.As, GraKarciana.Karta.Dupek, GraKarciana.Karta.Dama });
            var z = a.GetMove(GraKarciana.Karta.As);
            Assert.IsFalse(z.Item2.cards.First().Contains(GraKarciana.Karta.As));
            Assert.IsFalse(z.Item1.Marriage);
        }
        [TestMethod]
        public void LoadMove_CaseMariage()
        {
            StateGame1000 a = new StateGame1000(3);
            a.cards.First().AddRange(new GraKarciana.Karta[] { GraKarciana.Karta.Dama, GraKarciana.Karta.As, GraKarciana.Karta.Król });
            var z = a.GetMove(GraKarciana.Karta.Dama);
            Assert.IsFalse(z.Item2.cards.First().Contains(GraKarciana.Karta.Dama));
            Assert.IsTrue(z.Item1.Marriage);
            Assert.AreEqual(z.Item2.scoreInCurentGame[0], 60);
        }
        [TestMethod]
        public void LoadMove_CaseClearTable()
        {
            StateGame1000 a = new StateGame1000(3);
            a.Player = 2;
            a.cards.First().AddRange(new GraKarciana.Karta[] { GraKarciana.Karta.Dama, GraKarciana.Karta.As, GraKarciana.Karta.Król });
            a=a.SetTable(new GraKarciana.Karta[] { GraKarciana.Karta.K9, GraKarciana.Karta.K10 });
            var z = a.GetMove(GraKarciana.Karta.As);
            Assert.AreEqual(z.Item2.Player, 2);
            Assert.AreEqual(z.Item2.scoreInCurentGame[2], 21);
            Assert.AreEqual(z.Item2.NumberCardInTable, 0);

        }
    }
}
