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

        static public IEnumerable<int> FibonacciLazy(int source, int target)
        {
            return InfiniteFibonacci().Skip(source - 1).Take(target - source + 1);//saltas los sorce primeros y te quedas con los siguientes
        }

        //PARA CASA:
        //EagerFibonacci

    }

}
