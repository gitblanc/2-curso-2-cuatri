using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Clases
{
    [TestClass]
    public class TestEnumerator
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
        public void Test1()
        {
            List<int> list = new List<int>();
            int uno = 1;
            list.Add(uno);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);
            Assert.AreEqual(1, list.GetElement(0));
            Assert.AreEqual(2, list.GetElement(1));
            Assert.AreEqual(3, list.GetElement(2));
            Assert.AreEqual(4, list.GetElement(3));
            Assert.AreEqual(5, list.GetElement(4));
            Assert.IsTrue(list.Contains(uno));
            int i = 0;
            foreach (var elem in list)
            {
                Assert.AreEqual(list.GetElement(i),elem);
                i++;
            }
        }

        [TestMethod]
        public void TestAdd()
        {
            List<People> list = new List<People>();
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
            Assert.IsTrue(list.Contains(p1));
            int i = 0;
            foreach (var elem in list)
            {
                Assert.AreEqual(list.GetElement(i), elem);
                i++;
            }
        }
    }
}

