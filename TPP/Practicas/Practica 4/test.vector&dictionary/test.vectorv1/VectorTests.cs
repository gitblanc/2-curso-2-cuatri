using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace test.vectorv1
{
    [TestClass]
    public class VectorTests
    {
        IList<int> enteros;
        [TestInitialize]
        public void InitializaPeople()
        {
            enteros = new List<int>();
        }

        [TestMethod]
        public void TestAddElements()
        {
            enteros.Add(1);
            enteros.Add(2);
            enteros.Add(3);
            enteros.Add(4);
            Assert.IsTrue(enteros.Contains(1));
            Assert.IsTrue(enteros.Contains(2));
            Assert.IsTrue(enteros.Contains(3));
            Assert.IsTrue(enteros.Contains(4));
            Assert.IsFalse(enteros.Contains(5));
            Assert.IsFalse(enteros.Contains(-12568));
        }

        [TestMethod]
        public void TestKnowElements()
        {
            enteros.Add(1);
            enteros.Add(2);
            enteros.Add(3);
            enteros.Add(4);
            Assert.AreEqual(4, enteros.Count);
        }

        [TestMethod]
        public void TestObtainAndReWrite()
        {
            enteros.Add(1);
            enteros.Add(2);
            enteros.Add(3);
            enteros.Add(4);
            Assert.AreEqual(4, enteros[3]);
            enteros.Insert(3, 78);
            Assert.AreEqual(78, enteros[3]);

        }

        [TestMethod]
        public void TestIsOrNot()
        {
            enteros.Add(1);
            enteros.Add(2);
            enteros.Add(3);
            enteros.Add(4);
            Assert.IsTrue(enteros.Contains(4));
            Assert.IsFalse(enteros.Contains(982367896));

        }

        [TestMethod]
        public void TestIndex()
        {
            enteros.Add(1);
            enteros.Add(2);
            enteros.Add(3);
            enteros.Add(4);
            Assert.AreEqual(2, enteros.IndexOf(3));
            Assert.AreEqual(-1, enteros.IndexOf(78));
        }

        [TestMethod]
        public void TestRemoveFirst()
        {
            enteros.Add(1);
            enteros.Add(2);
            enteros.Add(3);
            enteros.Add(4);
            enteros.Add(4);
            Assert.IsTrue(enteros.Remove(4));
            Console.WriteLine(enteros[3].ToString());
        }

        [TestMethod]
        public void TestIteracion()
        {
            enteros.Add(1);
            enteros.Add(2);
            enteros.Add(3);
            enteros.Add(4);
            enteros.Add(4);
            int cont = 0;
            foreach (int elem in enteros)
            {
                Assert.AreEqual(elem, enteros[cont]);
                cont++;
            }
        }
    }
}
