using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karty;
using System.Collections.Generic;
namespace UnitTestProject1
{
    [TestClass]
    public class MargeListTest
    {
        [TestMethod]
        public void MargeList()
        {
            List<int> a = new List<int>() { 5, 7, 8 };
            List<int> c = new List<int>() { 15, 47, 3,121 };
            List<int> d = new List<int>() { 7,5 };
            //MultiList<List<int>, int> marge =
            List < int > excepted = new List<int>();
            excepted.AddRange(a);
            excepted.AddRange(c);
            excepted.AddRange(d);
            MultiList<List<int>, int> date = new MultiList<List<int>, int>((X) => X, a, c, d);
            for (int i = 0; i < excepted.Count; i++)
            {
                Assert.IsTrue(excepted[i] == date[i]);
            }
        }

        [TestMethod]
        public void MargeList_Set()
        {
            List<int> a = new List<int>() { 5, 7, 8 };
            List<int> c = new List<int>() { 15, 47, 3, 121 };
            List<int> d = new List<int>() { 7, 5 };
            //MultiList<List<int>, int> marge =
            List<int> excepted = new List<int>();
            excepted.AddRange(a);
            excepted.AddRange(c);
            excepted.AddRange(d);
            MultiList<List<int>, int> date = new MultiList<List<int>, int>((X) => X, a, c, d);
            date[2] = 133;
            Assert.AreEqual(a[2], 133);
        }
    }
}
