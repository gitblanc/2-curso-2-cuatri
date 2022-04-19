using System;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TPP.Laboratory.Concurrency.Lab11
{

    class Program
    {
        //Transforma cada una de las imágenes y las va guardando en carpetas
        static void Main()
        {
            Stopwatch chrono = new Stopwatch();
            string[] files = Directory.GetFiles(@"..\..\..\..\pics", "*.jpg");
            string newDirectory = @"..\..\..\..\pics\rotated";
            Directory.CreateDirectory(newDirectory);


            chrono.Start();
            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                using (Bitmap bitmap = new Bitmap(file))
                {
                    //Console.WriteLine("Processing the file \"{0}\".", fileName);no hacer esto porque tarda más
                    bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    bitmap.Save(Path.Combine(newDirectory, fileName));
                }
            }
            chrono.Stop();
            Console.WriteLine("Elapsed time: {0:N} milliseconds.", chrono.ElapsedMilliseconds);
            //Segunda forma
            chrono.Reset();


            var threads = new HashSet<int>();//no es una estructura thread safe

            chrono.Start();
            Parallel.ForEach(files, file =>
            {
                lock (threads) { threads.Add(Thread.CurrentThread.ManagedThreadId); }
                string fileName = Path.GetFileName(file);
                using (Bitmap bitmap = new Bitmap(file))
                {
                    //Console.WriteLine("Processing the file \"{0}\".", fileName); no hacer esto pq tarda más
                    bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    bitmap.Save(Path.Combine(newDirectory, fileName));
                }
            });
            chrono.Stop();
            Console.WriteLine("Elapsed time: {0:N} milliseconds.", chrono.ElapsedMilliseconds);
            Console.WriteLine($"Using {threads.Count} threads");
        }
    }

}
