using System;
using System.IO;
using System.Runtime.InteropServices;



namespace Practica9
{

    class Program
    {
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

        /*
         * This program processes Bitcoin value information obtained from the
         * url https://api.kraken.com/0/public/OHLC?pair=xbteur&interval=5.
         */
        static void Main(string[] args)
        {
            const int maxNumberOfThreads = 50;
            const long valor = 7000;
            var data = Utils.GetBitcoinData();
            ShowLine(Console.Out, "Number of threads", "Ticks", "Result");
            //foreach (var d in data)
            //    Console.WriteLine(d);
            for (int threads = 1; threads <= maxNumberOfThreads; threads++) {//se prueba secuencial y multihilo
                Master master = new Master(data, threads, valor);
                long before = 0;
                QueryPerformanceCounter(out before);
                double result = master.ComputeModulus();
                long after = 0;
                QueryPerformanceCounter(out after);
                ShowLine(Console.Out, threads, (after - before), result);
                GC.Collect(); // Garbage collection
                GC.WaitForFullGCComplete();
            }
        }

        private const string CSV_SEPARATOR = "-";

        static void ShowLine(TextWriter stream, string numberOfThreadsTitle, string ticksTitle, string resultTitle)
        {
            stream.WriteLine("{0}{3}{1}{3}{2}{3}", numberOfThreadsTitle, ticksTitle, resultTitle, CSV_SEPARATOR);
        }

        static void ShowLine(TextWriter stream, int numberOfThreads, long ticks, double result)
        {
            stream.WriteLine("threads:{0} {3} ticks: {1:N0} {3} result:{2:N2}", numberOfThreads, ticks, result, CSV_SEPARATOR);
        }
    }
}
