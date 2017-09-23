using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Karty;
namespace UnitTestProject1
{
    [TestClass]
    public class TestingSwaper
    {
        [TestMethod]
        public void MargeList()
        {
            Swaper<MockRestrycion, int> swap = new Swaper<MockRestrycion, int>((X)=>X,(X,K)=>X.restrycion.Contains(K),10,10);
            MockRestrycion m0 = new MockRestrycion() { 1 }; m0.AddRestryction(0);
            MockRestrycion m1 = new MockRestrycion() { 3 }; m1.AddRestryction(1);
            MockRestrycion m2 = new MockRestrycion() { 2 }; m2.AddRestryction(2);
            MockRestrycion m3 = new MockRestrycion() { 0 }; m3.AddRestryction(3);
            List<MockRestrycion> ml = new List<MockRestrycion>() { m0, m1, m2, m3 };
            swap.Run(ml);
            Assert.AreEqual(0, m0.First());
            Assert.AreEqual(1, m1.First());
            Assert.AreEqual(2, m2.First());
            Assert.AreEqual(3, m3.First());

        }

    }
}
