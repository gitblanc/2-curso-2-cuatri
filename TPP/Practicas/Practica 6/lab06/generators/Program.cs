using System;
using System.Diagnostics;
using System.Threading;

namespace TPP.Laboratory.Functional.Lab06 {

    class Program {
        static void Main() {
            Stopwatch stopWatch = new Stopwatch();
            int i = 0;
            foreach (int value in Fibonacci.InfiniteFibonacci()) {
                Console.Write(value + " ");
                if (++i == 10)
                    break;
            }
            Console.WriteLine();

            i = 0;
            foreach (int value in Fibonacci.InfiniteFibonacci()) {
                Console.Write(value + " ");
                if (++i == 10)
                    break;
            }
            Console.WriteLine();

            //-----------Lazy-----------
            stopWatch.Start(); 
            foreach (int value in Fibonacci.FibonacciLazy(10,1000))//forma lazy
            {
                Console.Write(value + " ");
            }
            Console.WriteLine();
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}:{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("Lazy => " + elapsedTime);

            //-----------Eager-----------
            stopWatch.Start();
            foreach (int value in Fibonacci.FibonacciEager(10, 45))//forma eager
            {
                Console.Write(value + " ");
            }
            Console.WriteLine();
            stopWatch.Stop();
            ts = stopWatch.Elapsed;
            elapsedTime = string.Format("{0:00}:{1:00}:{2:00}:{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("Eager => " + elapsedTime);
        }
    }
}
