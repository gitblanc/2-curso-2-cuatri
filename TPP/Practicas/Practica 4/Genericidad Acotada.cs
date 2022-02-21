using System;

namespace genericMethods
{
    class Program
    {
        //INFERENCIA DE TIPOS: no pones lo que es la variable, pero se detecta sautomáticamente
        static void Swap<T>(ref T a, ref T b) //los parámetros no pueden ser cualquier cosa. T es más restrictivo. No hace falta hacer casts
        {
            T aux = a;
            a = b;
            b = aux;
        }

        static IComparable Max1(IComparable a, IComparable b)//Object es lo que implementa la interfaz
        {
            return a.CompareTo(b) > 0 ? a : b;
        }

        static IComparable<T> Max2<T>(IComparable<T> a, IComparable<T> b)//Genericidad siendo el T más específico. IComparable<T> hereda de object
        {
            return a.CompareTo((T)b) > 0 ? a : b;
        }

        static T Max3<T>(IComparable<T> a, IComparable<T> b)
        {
            return (T)(a.CompareTo((T) b) > 0 ? a : b);
        }

        static T Max4<T>(T a, T b) where T : IComparable //azúcar sintáctico
        {
            return a.CompareTo(b) > 0 ? a : b;
        }

        static void Main(string[] args)
        {
            var x = 1;
            var y = 7;
            Swap(ref x, ref y);
            Console.WriteLine($"x = {x}, y = {y}");

            int m1 = (int) Max1((IComparable)x,(IComparable) y); //conviertes a entero un Icomparable de object == object Más serio
            Console.WriteLine($"m1 = {m1}");
            int m2 = (int)Max2(x, y);//conviertes a entero un Icomparable<T>. Menos serio
            Console.WriteLine($"m2 = {m2}");
            int m3 = Max3(x, y);//ya no hace falta el casteo
            Console.WriteLine($"m3 = {m3}");
            int m4 = Max4(x, y);//la mejor forma (sin casteos)
            Console.WriteLine($"m4 = {m4}");
        }


    }
}
