using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraKarciana;
using Karty;
namespace ClassLibrary1
{
    [TestClass]
    public class IqTestcs
    {
        [TestMethod]
        public unsafe void TestIq()
        {
            TikTacToe tikTacToe = TikTacToe.GetClear();
            tikTacToe.Table[0] = 1;
            tikTacToe.Table[2] = 1;
            tikTacToe.Table[4] = 2;
            tikTacToe.PlayerIndex = 2;
            RelatingIq<TikTacToe, int, int> iq = new RelatingIq<TikTacToe, int, int>(10,true);
            var result = iq.Run(tikTacToe);
            var resultState = result.Item2;
            Show(resultState);
            Assert.AreEqual(1, result.Item1);
            Assert.AreEqual(2, resultState.Table[1]);
        }
        [TestMethod]
        public unsafe void TestIqCash()
        {
            TikTacToe tikTacToe = TikTacToe.GetClear();
            RelatingIq<TikTacToe, int, int> iq = new RelatingIq<TikTacToe, int, int>(10,true,true);
            var result = iq.Run(tikTacToe);
            var resultState = result.Item2;
            Show(resultState);
        }
        [TestMethod]
        public unsafe void TestIqDown()
        {
            TikTacToe tikTacToe = TikTacToe.GetClear();
            tikTacToe.Table[6] = 1;
            tikTacToe.Table[8] = 1;
            tikTacToe.Table[4] = 2;
            tikTacToe.PlayerIndex = 2;
            RelatingIq<TikTacToe, int, int> iq = new RelatingIq<TikTacToe, int, int>(10,true);
            var result = iq.Run(tikTacToe);
            var resultState = result.Item2;
            Show(resultState);
            Assert.AreEqual(7, result.Item1);
            Assert.AreEqual(2, resultState.Table[7]);
        }
        [TestMethod]
        public unsafe void TestIqWin()
        {
            TikTacToe tikTacToe = TikTacToe.GetClear();
            tikTacToe.Table[6] = 2;
            tikTacToe.Table[8] = 2;
            tikTacToe.Table[4] = 1;
            tikTacToe.Table[5] = 1;
            tikTacToe.PlayerIndex = 2;
            RelatingIq<TikTacToe, int, int> iq = new RelatingIq<TikTacToe, int, int>(10);
            var result = iq.Run(tikTacToe);
            var resultState = result.Item2;
            Show(resultState);
            Assert.AreEqual(3, result.Item1);
            Assert.AreEqual(2, resultState.Table[3]);
        }
        [TestMethod]
        public unsafe void TacToWin()
        {
            TikTacToe tikTacToe = TikTacToe.GetClear();
            tikTacToe.Table[0] = 1;
            tikTacToe.Table[1] = 1;
            tikTacToe.Table[2] = 1;
            Assert.AreEqual(tikTacToe.Winer(), 1);

            tikTacToe = TikTacToe.GetClear();
            tikTacToe.Table[0] = 2;
            tikTacToe.Table[4] = 2;
            tikTacToe.Table[8] = 2;
            Assert.AreEqual(tikTacToe.Winer(), 2);


            tikTacToe = TikTacToe.GetClear();
            tikTacToe.Table[0] = 2;
            tikTacToe.Table[4] = 2;
            tikTacToe.Table[2] = 2;
            Assert.AreEqual(tikTacToe.Winer(), 0);
        }

        internal static unsafe void Show(TikTacToe t)
        {
            string z = "";
            for (int i = 0; i < 9; i++)
            {
                if (i % 3 == 0)
                {
                    z += Environment.NewLine;
                }
                z += t.Table[i];

            }
            System.Diagnostics.Debug.WriteLine(z);
        }
    }
}
