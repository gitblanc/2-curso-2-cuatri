using Microsoft.VisualStudio.TestTools.UnitTesting;
using paradigma.funcional;
using System.Collections.Generic;

namespace tests
{
    [TestClass]
    public class FuncionesTests
    {
        Person[] people;
        Angle[] angles;

        [TestInitialize]
        public void InitializeAll()
        {
            people = Factory.CreatePeople();
            angles = Factory.CreateAngles();
        }


        [TestMethod]
        public void FindTest()
        {
            Assert.AreEqual(people[1].ToString(), people.Find(x => x.Surname.Equals("Pérez")).ToString());//apellido
            Assert.AreEqual(people[0].ToString(), people.Find(x => x.FirstName.Equals("María")).ToString());//nombre
            Assert.AreEqual(people[0].ToString(), people.Find(x => x.IDNumber.EndsWith("A")).ToString());//dni
            Assert.IsNull(people.Find(x => x.IDNumber.EndsWith("Ñ")));//default
        }

        [TestMethod]
        public void FilterTest()
        {
            Person[] filtroPeopleA = (Person[])people.Filter(x => x.Surname.Equals("Pérez"));
            Assert.AreEqual(people[1].ToString(), filtroPeopleA[0].ToString());
            Assert.AreEqual(people[5].ToString(), filtroPeopleA[1].ToString());//apellido
            Person[] filtroPeopleN = (Person[])people.Filter(x => x.FirstName.Equals("María"));
            Assert.AreEqual(people[0].ToString(), filtroPeopleN[0].ToString());
            Assert.AreEqual(people[7].ToString(), filtroPeopleN[1].ToString());//nombre
            Person[] filtroPeopleDNI = (Person[])people.Filter(x => x.IDNumber.EndsWith("A"));
            Assert.AreEqual(people[0].ToString(), filtroPeopleDNI[0].ToString());
            Assert.AreEqual(people[3].ToString(), filtroPeopleDNI[1].ToString());//dni
            Assert.IsNull(people.Filter(x => x.IDNumber.EndsWith("Ñ")));//default

        }

        [TestMethod]
        public void ReduceTest()
        {
            double result = angles.Reduce(funcion, 0.0);
            Assert.AreEqual(64980, result);
        }
         
        public double funcion(double x1, Angle a) {
            return x1 + a.Degrees;
        }
    }
}