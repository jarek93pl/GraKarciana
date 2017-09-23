using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    [TestClass]
    public class TikTacToeTest
    {
        [TestMethod]
        public unsafe void Move()
        {
            TikTacToe tikTacToe = TikTacToe.GetClear();
            tikTacToe.Table[6] = 1;
            tikTacToe.Table[8] = 1;
            tikTacToe.Table[4] = 2;
            tikTacToe.PlayerIndex = 1;

            var resultState = tikTacToe.GetStates().First();
            IqTestcs.Show(resultState.Item2);
            Assert.AreEqual(0, resultState.Item1);
            Assert.AreEqual(1, resultState.Item2.Table[0]);
            Assert.AreEqual(2, resultState.Item2.PlayerIndex);
        }

    }
}
