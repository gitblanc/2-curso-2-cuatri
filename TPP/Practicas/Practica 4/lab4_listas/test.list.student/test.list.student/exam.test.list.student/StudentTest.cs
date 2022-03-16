using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TPP.Laboratory.Exam.Test.List;

namespace TPP.Laboratory.Exam.Test.Student
{
    [TestClass]
    public class StudentTest : TestList
    {
        public override dynamic GetList<T>()
        {
            //You must return an instance of your List
            throw new NotImplementedException("'StudentTest.GetList' must return an instance of your generic list");
        }
    }
}
