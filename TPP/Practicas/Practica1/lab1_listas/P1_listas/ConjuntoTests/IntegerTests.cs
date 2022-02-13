using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConjuntoV1;

namespace ConjuntoTests
{
    [TestClass]
    public class IntegerTests
    {
        [TestMethod]
        public void TestAdd()
        {
            Conjunto c = new();
            c.Add(1);
            c.Add(2);
            c.Add(3);
            c.Add(4);
            c.Add(5);
            Assert.AreEqual(1, c.GetElement(0));
            Assert.AreEqual(2, c.GetElement(1));
            Assert.AreEqual(3, c.GetElement(2));
            Assert.AreEqual(4, c.GetElement(3));
            Assert.AreEqual(5, c.GetElement(4));
            Assert.AreEqual(5, c.Size());
            Assert.IsTrue(c.Contains(4));
            c.Add(1);//no se va a añadir
            Assert.AreEqual(5, c.Size());
        }

        [TestMethod]
        public void TestRemove()
        {
            Conjunto c = new();
            c.Add(1);
            c.Add(2);
            c.Add(3);
            c.Add(4);
            c.Add(5);
            Assert.AreEqual(true, c.Remove(2));
            Assert.AreEqual(4, c.Size());
            Assert.IsFalse(c.Contains(2));
            Assert.AreEqual(true, c.Remove(5));
            Assert.AreEqual(3, c.Size());
            Assert.IsFalse(c.Contains(5));
            Assert.AreEqual(false, c.Remove(32));//se elimina algo que no existe
        }

        [TestMethod]
        public void TestOperatorPlus()
        {
            Conjunto c = new();
            c += 1;
            c += 2;
            c += 3;
            c += 4;
            c += 5;
            Assert.AreEqual(1, c.GetElement(0));
            Assert.AreEqual(2, c.GetElement(1));
            Assert.AreEqual(3, c.GetElement(2));
            Assert.AreEqual(4, c.GetElement(3));
            Assert.AreEqual(5, c.GetElement(4));
            Assert.AreEqual(5, c.Size());
            Assert.IsTrue(c.Contains(4));
            c+=1;//no se va a añadir
            Assert.AreEqual(5, c.Size());
        }

        [TestMethod]
        public void TestOperatorLess()
        {
            Conjunto c = new();
            c += 1;
            c += 2;
            c += 3;
            c += 4;
            c += 5;
            Assert.AreEqual(1, c.GetElement(0));
            Assert.AreEqual(2, c.GetElement(1));
            Assert.AreEqual(3, c.GetElement(2));
            Assert.AreEqual(4, c.GetElement(3));
            Assert.AreEqual(5, c.GetElement(4));
            Assert.AreEqual(5, c.Size());
            Assert.IsTrue(c.Contains(4));
            c -= 2;
            Assert.AreEqual("[1 3 4 5 ]", c.ToString());
            Assert.AreEqual(4, c.Size());
            Assert.IsFalse(c.Contains(2));
            c -= 5;
            Assert.AreEqual("[1 3 4 ]", c.ToString());
            Assert.AreEqual(3, c.Size());
            Assert.IsFalse(c.Contains(5));
            c -= 32;
            Assert.AreEqual("[1 3 4 ]", c.ToString());//se elimina algo que no existe
            c -= 3;
            Assert.AreEqual("[1 4 ]", c.ToString());
            Assert.AreEqual(2, c.Size());
            Assert.IsFalse(c.Contains(2));
            c -= 4;
            Assert.AreEqual("[1 ]", c.ToString());
            Assert.AreEqual(1, c.Size());
            Assert.IsFalse(c.Contains(5));
            c -= 1;
            Assert.AreEqual("[]", c.ToString());
            Assert.AreEqual(0, c.Size());
            Assert.IsFalse(c.Contains(5));
            c -= 1238465;
            Assert.AreEqual("[]", c.ToString());//se elimina un elemento cuando está vacía
        }

        [TestMethod]
        public void TestOperatorUnion()
        {
            Conjunto c = new();
            Conjunto c2 = new();
            c += 1;
            c += 2;
            c += 3;

            c2 += 2;
            c2 += 3;
            c2 += 4;

            c |= c2;//se unen los conjuntos
            Assert.AreEqual("[1 2 3 4 ]", c.ToString());
        }

        [TestMethod]
        public void TestOperatorIntersection()
        {
            Conjunto c = new();
            Conjunto c2 = new();
            c += 1;
            c += 2;
            c += 3;

            c2 += 2;
            c2 += 3;
            c2 += 4;

            c &= c2;//se unen los conjuntos
            Assert.AreEqual("[2 3 ]", c.ToString());
        }

        [TestMethod]
        public void TestOperatorDifference()
        {
            Conjunto c = new();
            Conjunto c2 = new();
            c += 1;
            c += 2;
            c += 3;

            c2 += 2;
            c2 += 3;
            c2 += 4;

            c -= c2;//se unen los conjuntos
            Assert.AreEqual("[1 ]", c.ToString());
        }
    }
}