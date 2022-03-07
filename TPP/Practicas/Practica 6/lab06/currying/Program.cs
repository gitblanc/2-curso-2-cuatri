using System;
using System.Collections.Generic;
using System.Linq;

namespace TPP.Laboratory.Functional.Lab06
{

    static class Program
    {

        // \x.\y.x+y
        static int Addition(int a, int b)
        {
            return a + b;
        }

        // \x.\y.x+y
        static Func<int, int> CurryedAdd(int a)
        {
            return b => b + a;
        }

        static IEnumerable<TCD> Map<TD, TCD>(this IEnumerable<TD> collection, Func<TD, TCD> f)
        {
            TCD[] result = new TCD[collection.Count()];
            int i = 0;
            foreach (TD d in collection)
            {
                result[i] = f(d); //aplicas la funcion que pasas como parámetro a cada elemento
                i++;
            }
            return result;
        }

        static IEnumerable<T> Show<T>(this IEnumerable<T> collection)
        {
            foreach (T element in collection)
            {
                Console.Write($"{element} ");
            }
            Console.WriteLine();
            return collection;//chaining -> imprime el resultado y lo devuelve para seguir operando con el 
        }

        public static IEnumerable<T> Filter<T>(this IEnumerable<T> collection, Predicate<T> p)
        {
            T[] result = new T[collection.Count()];
            int i = 0;
            foreach (T elem in collection)
            {
                if (p(elem))
                {
                    result[i] = elem;
                    i++;
                }
            }
            Array.Resize(ref result, i);
            if (result.Length == 0)
                return default(T[]);
            return result;
        }

        static Predicate<int> GreaterThan(int a) {
            return b => b > a;
        }

        static void Main()
        {
            Console.WriteLine(Addition(1, 2));
            Console.WriteLine(CurryedAdd(1)(2));
            var increment = CurryedAdd(1);//funcion que a lo que le pases devuelve su valor mas uno

            int[] values1 = new int[] { -3, -2, -1, 0, 1, 2, 3 };
            values1.Map(x => Addition(1, x)).Show();
            Console.WriteLine();
            values1.Map(CurryedAdd(1)).Show();
            values1.Map(increment).Show();

            var random = new Random();
            var aleatorios = new int[10];
            aleatorios.Map(_ => random.Next(-100, 100)).Show();//generar números aleatorios usando map, la _ es pq es una variable que no se va a usar

            aleatorios.Map(_ => random.Next(0,10)).Filter(GreaterThan(5)).Show();//filtrar con carry




        }



    }
}
