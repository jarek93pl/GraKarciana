using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraKarciana;
namespace ClassLibrary1
{
    [TestClass]
    public class Helper
    {
        [TestMethod]
        public void MaxInList()
        {
            //          0   1   2 3
            int[] t0 = { 0, 4, 8, 1 };
            int[] t1 = { 12, 4, 8, 1 };
            int[] t2 = { 0, 4, 8, 21 };
            Assert.AreEqual(2, t0.FindMaxIndex());
            Assert.AreEqual(0, t1.FindMaxIndex());
            Assert.AreEqual(3, t2.FindMaxIndex());
        }
    }
}
