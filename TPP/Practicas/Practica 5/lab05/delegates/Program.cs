using System;
using System.Collections.Generic;
using System.Linq;

namespace delegates
{
    //  Metodos de extension: static class, static method, this
    static class Program
    {
        //forma antigua -> delegate double Function(double x);//tipo función

        //TDOMINIO, LO QUE LE METES.....TCODOMINIO, LO QUE DEVUELVE
        //dominio->tipo de los valores que le pasas al método o función.....
        //codominio->tipo de los valores que devuelve el método
        static IEnumerable<TCD> Map<TD,TCD>(this IEnumerable<TD> collection, Func<TD,TCD> f)
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

        private static double Square(double x)
        {
            return x * x;
        }

        static void Show<T>(this IEnumerable<T> collection)
        {
            foreach (T element in collection) {
                Console.Write($"{element} ");
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            double[] values = new double[] { -3, -2, -1, 0, 1, 2, 3 };
            string[] valuesString = new string[] { "Holi", "que", "aseh" };
            // values.Map(Square).Show();
            values.Map(x => Math.Sqrt(x)).Show();//forma anónima, función lambda
            values.Map(Math.Sqrt).Show();//forma anónima, función lambda de forma más abreviada
            valuesString.Map(x => x.ToLower() + "¿?").Show();//forma anónima, función lambda->no funciona de forma anónima
            //valuesString.Select;//lo mismo que el map
        }

        //[1,2,3].reduce(x,acc => x+acc, 0) -> equivalente a un foreach que suma todos los elementos
    }
}
