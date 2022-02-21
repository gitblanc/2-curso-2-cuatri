using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConjuntoV1;
using Clases;

namespace ConjuntoTests
{
    [TestClass]
    public class PersonTest
    {
        private People p1;
        private People p2;
        private People p3;
        private People p4;
        private People p5;

        [TestInitialize]
        public void InitializaPeople()
        {
            p1 = new People("Juan", "Pérez", "5646453");
            p2 = new People("Lucas", "Pérez", "562345453");
            p3 = new People("Edu", "Pérez", "12342345");
            p4 = new People("Carmen", "Pérez", "12343");
            p5 = new People("Clara", "Díaz", "1324312");
        }

        [TestMethod]
        public void TestAdd()
        {
            Conjunto c = new();
            c.Add(p1);
            c.Add(p2);
            c.Add(p3);
            c.Add(p4);
            c.Add(p5);
            Assert.AreEqual(p1, c.GetElement(0));
            Assert.AreEqual(p2, c.GetElement(1));
            Assert.AreEqual(p3, c.GetElement(2));
            Assert.AreEqual(p4, c.GetElement(3));
            Assert.AreEqual(p5, c.GetElement(4));
            Assert.AreEqual(5, c.Size());
            Assert.IsTrue(c.Contains(p4));
            c.Add(p1);//no se va a añadir
            Assert.AreEqual(5, c.Size());
        }

        [TestMethod]
        public void TestRemove()
        {
            Conjunto c = new();
            c.Add(p1);
            c.Add(p2);
            c.Add(p3);
            c.Add(p4);
            c.Add(p5);
            Assert.AreEqual(true, c.Remove(p2));
            Assert.AreEqual(4, c.Size());
            Assert.IsFalse(c.Contains(p2));
            Assert.AreEqual(true, c.Remove(p5));
            Assert.AreEqual(3, c.Size());
            Assert.IsFalse(c.Contains(p5));
            Assert.AreEqual(false, c.Remove(32));//se elimina algo que no existe
        }

        [TestMethod]
        public void TestOperatorPlus()
        {
            Conjunto c = new();
            c += p1;
            c += p2;
            c += p3;
            c += p4;
            c += p5;
            Assert.AreEqual(p1, c.GetElement(0));
            Assert.AreEqual(p2, c.GetElement(1));
            Assert.AreEqual(p3, c.GetElement(2));
            Assert.AreEqual(p4, c.GetElement(3));
            Assert.AreEqual(p5, c.GetElement(4));
            Assert.AreEqual(5, c.Size());
            Assert.IsTrue(c.Contains(p4));
            c += p1;//no se va a añadir
            Assert.AreEqual(5, c.Size());
        }

        [TestMethod]
        public void TestOperatorLess()
        {
            Conjunto c = new();
            c += p1;
            c += p2;
            c += p3;
            c += p4;
            c += p5;
            Assert.AreEqual(p1, c.GetElement(0));
            Assert.AreEqual(p2, c.GetElement(1));
            Assert.AreEqual(p3, c.GetElement(2));
            Assert.AreEqual(p4, c.GetElement(3));
            Assert.AreEqual(p5, c.GetElement(4));
            Assert.AreEqual(5, c.Size());
            Assert.IsTrue(c.Contains(p4));
            c -= p2;
            Assert.AreEqual(4, c.Size());
            Assert.IsFalse(c.Contains(p2));
            c -= p5;
            Assert.AreEqual(3, c.Size());
            Assert.IsFalse(c.Contains(p5));
            c -= 32;//se elimina algo que no existe
            c -= p3;
            Assert.AreEqual(2, c.Size());
            Assert.IsFalse(c.Contains(p3));
            c -= p4;
            Assert.AreEqual(1, c.Size());
            Assert.IsFalse(c.Contains(p4));
            c -= p1;
            Assert.AreEqual("[]", c.ToString());
            Assert.AreEqual(0, c.Size());
            Assert.IsFalse(c.Contains(p1));
            c -= 1238465;
            Assert.AreEqual("[]", c.ToString());//se elimina un elemento cuando está vacía
        }

        [TestMethod]
        public void TestOperatorUnion()
        {
            Conjunto c = new();
            Conjunto c2 = new();
            c += p1;
            c += p2;
            c += p3;

            c2 += p2;
            c2 += p3;
            c2 += p4;

            c |= c2;//se unen los conjuntos
            Assert.AreEqual("[Juan Pérez, with 5646453 ID number Lucas" +
                " Pérez, with 562345453 ID number Edu Pérez, with " +
                "12342345 ID number Carmen Pérez, with 12343 ID " +
                "number ]", c.ToString());
        }

        [TestMethod]
        public void TestOperatorIntersection()
        {
            Conjunto c = new();
            Conjunto c2 = new();
            c += p1;
            c += p2;
            c += p3;

            c2 += p2;
            c2 += p3;
            c2 += p4;

            c &= c2;//se unen los conjuntos
            Assert.AreEqual("[Lucas Pérez, with 562345453 ID number Edu Pérez" +
                ", with 12342345 ID number ]", c.ToString());
        }

        [TestMethod]
        public void TestOperatorDifference()
        {
            Conjunto c = new();
            Conjunto c2 = new();
            c += p1;
            c += p2;
            c += p3;

            c2 += p2;
            c2 += p3;
            c2 += p4;

            c -= c2;//se unen los conjuntos
            Assert.AreEqual("[Juan Pérez, with 5646453 ID number ]",
                c.ToString());
        }
    }
}
