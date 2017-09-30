using Karty;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Game1000
{
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class Iq1000
    {
        [TestMethod,Timeout(5000)]
        public void GetBidSum()
        {
            IQ1000Game iq = new IQ1000Game(50, 0.8f);
            ConclusionAboutGame cm = new ConclusionAboutGame(3, 1, Date.simpleCards8);
            int w = iq.CalculateBidAmount(cm);
            Assert.IsTrue(w < 200 && w > 160);
        }
    }
}
