using Lista;
using linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class TestListFunctions
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
            var per2 = people.Find(x => x.IDNumber.EndsWith("1344513"));//defecto
            var cos2 = angles.Find(x => x.Degrees.Equals(-09234785908479));//defecto
            Assert.AreEqual(p[0], per);
            Assert.AreEqual(a[90], cos);
            Assert.IsNull(per2);
            Assert.IsNull(cos2);
        }

        [TestMethod]
        public void FilterTest()
        {
            var per = people.Filter(x => x.IDNumber.EndsWith("A"));
            Person[] p1 = per.ToArray();
            Assert.AreEqual(p1[0], p[0]);
            Assert.AreEqual(p1[1], p[3]);
            var cos = angles.Filter(x => x.Degrees.Equals(90));
            Angle[] a1 = cos.ToArray();
            Assert.AreEqual(a1[0], a[90]);
        }

        [TestMethod]
        public void ReduceTest()
        {
            var r = angles.Reduce(funcion, 0.0);
            int suma = 0;
            for (int i = 0; i <= 360; i++)
            {
                suma += i;
            }
            Assert.AreEqual(suma, r);
        }

        [TestMethod]
        public void InvertTest()
        {
            var inverted = people.Invert();
            int i = 0;
            Assert.AreNotEqual(inverted.GetEnumerator().Current, people.GetElement(0));
            var reordered = inverted.Invert();
            i = 0;
            foreach (var e in reordered)
            {
                Assert.AreEqual(e, people.GetElement(i));
                i++;

            }
        }

        [TestMethod]
        public void MapTest()
        {
            var mapped = people.Map(x => x.FirstName + x.Surname);
            string[] mezcla = mapped.ToArray();
            int i = 0;
            foreach (String s in mezcla)
            {
                Assert.AreEqual(s, people.ElementAt(i).FirstName + people.ElementAt(i).Surname);
                i++;
            }
        }


        public double funcion(double x1, Angle a)
        {
            return x1 + a.Degrees;
        }
    }

}

