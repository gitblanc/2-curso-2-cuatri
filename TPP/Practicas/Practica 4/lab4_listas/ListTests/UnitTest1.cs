using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Clases;

namespace ListTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAdd()
        {
            List list = new List();
            People p1 = new People("Juan","Pérez","5646453");
            People p2 = new People("Lucas", "Pérez", "562345453");
            People p3 = new People("Edu", "Pérez", "12342345");
            People p4 = new People("Carmen", "Pérez", "12343");
            People p5 = new People("Clara", "Díaz", "1324312");
            list.Add(p1);
            list.Add(p2);
            list.Add(p3);
            list.Add(p4);
            list.Add(p5);
            Assert.AreEqual(p1, list.GetElement(0));
            Assert.AreEqual(p2, list.GetElement(1));
            Assert.AreEqual(p3, list.GetElement(2));
            Assert.AreEqual(p4, list.GetElement(3));
            Assert.AreEqual(p5, list.GetElement(4));



        }

        [TestMethod]
        public void TestRemove()
        {
            List list = new List();
            People p1 = new People("Juan", "Pérez", "5646453");
            People p2 = new People("Lucas", "Pérez", "562345453");
            People p3 = new People("Edu", "Pérez", "12342345");
            People p4 = new People("Carmen", "Pérez", "12343");
            People p5 = new People("Clara", "Díaz", "1324312");
            list.Add(p1);
            list.Add(p2);
            list.Add(p3);
            list.Add(p4);
            list.Add(p5);
            list.Remove(p2);
            Assert.AreEqual(4, list.Size());
            Assert.IsFalse(list.Contains(p2));
            list.Remove(p5);
            Assert.AreEqual(3, list.Size());
            Assert.IsFalse(list.Contains(p5));
        }
    }
}
