using Microsoft.VisualStudio.TestTools.UnitTesting;
using Clases;

namespace ListTestsv0
{
    namespace ListTests
    {
        [TestClass]
        public class TestInt
        {
            [TestMethod]
            public void TestAdd2()
            {
                List list = new List();
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
            }
        }
    }
}
