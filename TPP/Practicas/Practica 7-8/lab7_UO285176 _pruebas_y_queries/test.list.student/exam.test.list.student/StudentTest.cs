using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TPP.Laboratory.Exam.Test.List;
using Lista;

namespace TPP.Laboratory.Exam.Test.Student
{
    [TestClass]
    public class StudentTest : TestList
    {
        public override dynamic GetList<T>()
        {
            //You must return an instance of your List
            List<T> l = new();
            return l;
        }
    }
}
