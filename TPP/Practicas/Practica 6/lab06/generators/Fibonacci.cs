using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TPP.Laboratory.Functional.Lab06
{

    static class Fibonacci
    {

        static internal IEnumerable<int> InfiniteFibonacci()
        {
            int first = 1, second = 1;
            while (true)
            {
                yield return first;//al depurar empieza desde aquí
                int addition = first + second;
                first = second;
                second = addition;
            }
        }

        internal static int LimitedFibonacci(int n)
        {
            return n <= 2 ? 1 : LimitedFibonacci(n - 2) + LimitedFibonacci(n - 1);
        }

        static internal IEnumerable<int> FibonacciLazy(int source, int target)
        {
            return InfiniteFibonacci().Skip(source - 1).Take(target - source + 1);//saltas los source primeros y te quedas con los siguientes
        }

        //PARA CASA:
        //EagerFibonacci

        static internal IEnumerable<int> FibonacciEager(int source, int target)
        {
            IList<int> result = new List<int>();
            for (int i = 0; i <= target; i++) {
                result.Add(LimitedFibonacci(i));
            }
            return result.Skip(source);
        }

    }

}
