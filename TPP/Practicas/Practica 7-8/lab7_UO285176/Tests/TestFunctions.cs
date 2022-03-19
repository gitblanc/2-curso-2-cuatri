using Lista;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class TestFunctions
    {
        Person[] p;
        Angle[] a;
        List<Person> people;
        List<Angle> angles;

        [TestInitialize]
        public void InitializeAll()
        {
            p = Factory.CreatePeople();
            a = Factory.CreateAngles();
            people = new List<Person>();
            angles = new List<Angle>();
            foreach (Person ps in p)
            {
                people.Add(ps);
            }
            foreach (Angle al in a)
            {
                angles.Add(al);
            }
        }


        [TestMethod]
        public void FindTest()
        {
            var per = people.Find(x => x.IDNumber.EndsWith("A"));
            var cos = angles.Find(x => x.Degrees.Equals(90));
            Assert.AreEqual(p[0], per);
            Assert.AreEqual(a[90], cos);
        }

        [TestMethod]
        public void FilterTest()
        {
            var per = people.Filter(x => x.IDNumber.EndsWith("A"));
            var cos = angles.Filter(x => x.Degrees.Equals(90));
            Assert.AreEqual(people.GetElement(3).ToString(), per.GetEnumerator().MoveNext().ToString());
        }

        [TestMethod]
        public void ReduceTest()
        {
            
        }

        [TestMethod]
        public void InvertTest()
        {
           
        }

        [TestMethod]
        public void MapTest()
        {
            
        }

        [TestMethod]
        public void ForEachTest()
        {
           
        }

        public double funcion(double x1, Angle a)
        {
            return x1 + a.Degrees;
        }
    }

}

