using System;
using linq;
using Lista;
//using System.Collections.Generic;
using System.Linq;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            //IEnumerable<int> integers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //integers.Map(x =>
            //{
            //    Console.WriteLine(x);
            //    return x;
            //}).Last();

            //integers.ForEach(x => Console.WriteLine(x));

            Lista.List<int> integersL = new Lista.List<int>();
            for (int i = 0; i < 50; i++)
            {
                integersL.Add(i);
            }
            //integersL.ForEach(x => Console.Write(x + " "));
            //Console.WriteLine();
            //var map = integersL.Map(x => x.Equals(3));
            //map.ForEach(x => Console.Write(x + " "));
            //int x = integersL.Reduce(x => funcion(x,2));
            //var x = integersL.Filter(x => x < 10);
            //x.ForEach(x => Console.Write(x + " "));
            //var s = integersL.Find(x => x == 98769);
            //var s2 = integersL.Find(x => x == 9);
            //Console.WriteLine(s);
            var z = integersL.Invert();
            z.ForEach(x => Console.WriteLine(x));
        }

        public double funcion(int x1, int a)
        {
            return x1 + a;
        }
    }
}
