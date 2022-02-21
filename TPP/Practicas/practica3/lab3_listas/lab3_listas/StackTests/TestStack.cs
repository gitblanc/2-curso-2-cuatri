using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stackv1;
using Clases;

namespace StackTests
{
    [TestClass]
    public class TestStack
    {
        [TestMethod]
        public void TestPush()
        {
            Stack st = new Stack(6);
            st.Push(1);
            st.Push(2);
            st.Push(3);
            st.Push(4);
            st.Push(5);
            st.Push(6);
            //st.Push(7); salta excepción
        }

        [TestMethod]
        public void TestPop()
        {
            Stack st = new Stack(6);
            st.Push(1);
            st.Push(2);
            st.Push(3);
            st.Push(4);
            st.Push(5);
            st.Push(6);
            object elem = st.Pop();
            Assert.IsNotNull(elem);
            Assert.AreEqual(6, elem);
        }
    }
}