using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace test.diccionario
{
    [TestClass]
    public class DictionaryTests
    {
        IDictionary<int, String> diccionario;
        String p1;
        String p2;
        String p3;
        String p4;

        [TestInitialize]
        public void InitializeDictionary()
        {
            diccionario = new Dictionary<int, String>();
            p1 = "Luis";
            p2 = "Carlos";
            p3 = "Juan";
            p4 = "Clara";
        }


        [TestMethod]
        public void TestAdd()
        {
            diccionario.Add(1, p1);
            diccionario.Add(2, p2);
            diccionario.Add(3, p3);
            diccionario.Add(4, p4);
            //diccionario.Add(1, p4); no se puede pq ya existe un elemento con esa clave
            Assert.IsTrue(diccionario.ContainsKey(1));
            Assert.IsTrue(diccionario.ContainsKey(2));
            Assert.IsTrue(diccionario.ContainsKey(3));
            Assert.IsTrue(diccionario.ContainsKey(4));
            Assert.IsFalse(diccionario.ContainsKey(192783456));
        }

        [TestMethod]
        public void TestNumPares()
        {
            diccionario.Add(1, p1);
            diccionario.Add(2, p2);
            diccionario.Add(3, p3);
            diccionario.Add(4, p4);
            Assert.AreEqual(4, diccionario.Count);
        }

        [TestMethod]
        public void TestObtainAndReWrite()
        {
            diccionario.Add(1, p1);
            diccionario.Add(2, p2);
            diccionario.Add(3, p3);
            diccionario.Add(4, p4);
            String elem = diccionario[4];//le pasas la clave, no la posición
            Assert.AreEqual(p4, elem);
            diccionario[3] = "Patata";
            Assert.AreEqual("Patata", diccionario[3]);
        }

        [TestMethod]
        public void TestContains()
        {
            diccionario.Add(1, p1);
            diccionario.Add(2, p2);
            diccionario.Add(3, p3);
            diccionario.Add(4, p4);
            Assert.IsTrue(diccionario.ContainsKey(1));
            Assert.IsTrue(diccionario.ContainsKey(2));
            Assert.IsTrue(diccionario.ContainsKey(3));
            Assert.IsTrue(diccionario.ContainsKey(4));
            Assert.IsFalse(diccionario.ContainsKey(192783456));
        }

        [TestMethod]
        public void TestRemoveByKey()
        {
            diccionario.Add(1, p1);
            diccionario.Add(2, p2);
            diccionario.Add(3, p3);
            diccionario.Add(4, p4);
            Assert.IsTrue(diccionario.Remove(3));
            Assert.IsFalse(diccionario.Remove(567));
            Assert.IsFalse(diccionario.ContainsKey(3));

        }

        [TestMethod]
        public void TestIteracion()
        {
            diccionario.Add(1, p1);
            diccionario.Add(2, p2);
            diccionario.Add(76, "Extra");
            diccionario.Add(3, p3);
            diccionario.Add(4, p4);
            int i = 0;
            foreach (KeyValuePair<int, String> elem in diccionario)
            {
                Assert.IsTrue(diccionario.ContainsKey(elem.Key));
                i++;
            }
        }
    }
}