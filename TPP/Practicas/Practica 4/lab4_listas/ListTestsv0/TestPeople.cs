using Microsoft.VisualStudio.TestTools.UnitTesting;
using Clases;

namespace ListTestsv0
{
    namespace ListTests
    {
        [TestClass]
        public class TestPeople
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
                List list = new List();
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
            }


            [TestMethod]
            public void TestRemove()
            {
                List list = new List();
                list.Add(p1);
                list.Add(p2);
                list.Add(p3);
                list.Add(p4);
                list.Add(p5);
                Assert.AreEqual(true, list.Remove(p2));
                Assert.AreEqual(4, list.Size());
                Assert.IsFalse(list.Contains(p2));
                Assert.AreEqual(true, list.Remove(p5));
                Assert.AreEqual(3, list.Size());
                Assert.IsFalse(list.Contains(p5));
                Assert.AreEqual(false, list.Remove(32));//se elimina algo que no existe
            }

            [TestMethod]
            public void ToStringTest()
            {
                List list = new List();
                Assert.AreEqual("[]", list.ToString());//si no hay ningun elemento
                list.Add(p1);
                list.Add(p2);
                list.Add(p3);
                list.Add(p4);
                list.Add(p5);
                Assert.AreEqual("[Juan Pérez, with 5646453 ID number Lucas Pérez, " +
                    "with 562345453 ID number Edu Pérez, with 12342345 ID number Carmen " +
                    "Pérez, with 12343 ID number Clara Díaz, with 1324312 ID number ]", list.ToString());
            }

            [TestMethod]
            public void GetElementTest()
            {
                List list = new List();
                Assert.IsNull(list.GetElement(3));//si no hay ningún elemento
                list.Add(p1);
                list.Add(p2);
                list.Add(p3);
                list.Add(p4);
                list.Add(p5);
                Assert.AreEqual(p4, list.GetElement(3));
                Assert.IsNull(list.GetElement(273645));
            }

        }
    }
}
    
