using System.Collections.Generic;
using System;
using System.Linq;

namespace linq
{

    class Program
    {

        static void Main()
        {
            IEnumerable<int> integers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            integers.Map(x =>
            {
                Console.WriteLine(x);
                return x;
            }).Last();

            integers.ForEach(x => Console.WriteLine(x));
        }
    }

    //EQUIVALENCIAS
    //FIND-> firstOrDefault
    //FILTER-> where
    //REDUCE-> agregate
    //MAP-> select
}
