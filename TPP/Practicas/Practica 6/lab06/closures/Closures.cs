using System;

namespace TPP.Laboratory.Functional.Lab06
{

    /// <summary>
    /// Try to guess the behavior of this program without running it
    /// </summary>
    class Closures//closures=object orientation
    {//funcion que tiene dependencias de variables que no están en su código

        /// <summary>
        /// Version with a single method
        /// </summary>
        static Func<int> Counter1()
        {
            int counter = 0;
            return () => ++counter;
        }

        static void Counter2(int initVal, out Func<int> increment, out Func<int> decrement, out Action<int> assign)
        {
            int counter = initVal;
            increment = () => counter++;
            decrement = () => counter--;
            assign = (value) => counter = value;
        }//con referencias out se devuelven varias cosas



        internal class Counter
        {
            internal Func<int> increment { get; set; }
            internal Func<int> decrement { get; set; }
            internal Action<int> assign { get; set; }

        }

        static Counter Counter3(int initVal)
        {
            int counter = initVal;
            return new Counter
            {
                assign = value => counter = value,
                increment = () => counter++,
                decrement = () => counter--,
            };
        }

        static (Action<int> assign, Func<int> increment, Func<int> decrement) Counter4(int initVal)
        {
            int counter = initVal;
            return (//hay que poner en orden los tipos a devolver
                value => counter = value,
                () => counter++,
                () => counter--
            );
        }

        static void Main()
        {
            //----------------------------------------------------------------------------Counter1
            Func<int> counter = Counter1();
            Console.WriteLine(counter());//1
            Console.WriteLine(counter());//2

            Func<int> anotherCounter = Counter1();
            Console.WriteLine(anotherCounter());//1
            Console.WriteLine(anotherCounter());//2

            Console.WriteLine(counter());//3
            Console.WriteLine(counter());//4
            Console.WriteLine();
            //----------------------------------------------------------------------------Counter2

            Counter2(10, out Func<int> increment, out Func<int> decrement, out Action<int> assign);
            Console.WriteLine(increment());//10
            Console.WriteLine(increment());//11

            assign(20);

            Console.WriteLine(decrement());//20
            Console.WriteLine(decrement());//19
            Console.WriteLine();                               //18

            //----------------------------------------------------------------------------Counter3
            var c = Counter3(100);
            Console.WriteLine(c.increment());//10
            Console.WriteLine(c.increment());//11

            c.assign(20);

            Console.WriteLine(c.decrement());//20
            Console.WriteLine(c.decrement());//19
            Console.WriteLine();                                 //18

            //----------------------------------------------------------------------------Counter4
            var var4 = Counter4(1000);
            Console.WriteLine(var4.increment());//10
            Console.WriteLine(var4.increment());//11

            var4.assign(2000);

            Console.WriteLine(var4.decrement());//20
            Console.WriteLine(var4.decrement());//19
            Console.WriteLine();                                 //18
        }
    }

}
