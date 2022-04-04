using System;
using System.Diagnostics;

namespace TPP.Laboratory.Concurrency.Lab10
{

    class Program {

        static void Main() {
            const int value = 100000;

            Summation summation = new Summation(value, 1);//una suma secuencial
            Stopwatch chrono = new Stopwatch();
            chrono.Start();
            summation.Compute();
            chrono.Stop();
            Console.WriteLine("Value: {0}. Elapsed milliseconds: {1}.", summation.Value, chrono.ElapsedMilliseconds);

            summation = new Summation(value, 1000);//una suma paralela
            chrono.Restart();
            summation.Compute();
            chrono.Stop();
            Console.WriteLine("Value: {0}. Elapsed milliseconds: {1}.", summation.Value, chrono.ElapsedMilliseconds);

            summation = new SummationLock(value, 1000);//una suma paralela
            chrono.Restart();
            summation.Compute();
            chrono.Stop();
            Console.WriteLine("Value: {0}. Elapsed milliseconds: {1}.", summation.Value, chrono.ElapsedMilliseconds);

            summation = new SummationInterLock(value, 1000);//una suma paralela
            chrono.Restart();
            summation.Compute();
            chrono.Stop();
            Console.WriteLine("Value: {0}. Elapsed milliseconds: {1}.", summation.Value, chrono.ElapsedMilliseconds);
        }

    }
}
