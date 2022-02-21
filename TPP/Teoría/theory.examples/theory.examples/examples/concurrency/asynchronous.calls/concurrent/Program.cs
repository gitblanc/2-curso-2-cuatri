using System;
using System.Threading.Tasks;

namespace TPP.Concurrency.Delegates
{
    // BeginInvoke is not supported in .Net Core
    // https://devblogs.microsoft.com/dotnet/migrating-delegate-begininvoke-calls-for-net-core/

    /// <summary>
    /// Example use of delegates in to pass messages asynchronously 
    /// </summary>
    class Program {

        private static async void GetImagesAsync()
        { 
            WebPage uniovi = new WebPage("http://www.uniovi.es");
            WebPage school = new WebPage("http://www.ingenieriainformatica.uniovi.es");

            Func<int> numberOfImages = uniovi.GetNumberOfImages;

            DateTime before = DateTime.Now;
            // * Asynchronous execution (a secondary thread is created)
            var asynchronousResult = uniovi.GetNumberOfImagesAsyncTask();

            // * Synchronous execution in the main thread 
            int numberOfImgsInSchool = school.GetNumberOfImages();
            
            // * Wait untill the secondary asynchronous task ends
            var numberOfImgsInUniovi = await asynchronousResult;
            DateTime after = DateTime.Now;

            Console.WriteLine("The University Web has {0:N0} images, and {1:N0} the School Web.",
                numberOfImgsInUniovi, numberOfImgsInSchool);
            Console.WriteLine("Elapsed millisenconds to compute both results: {0:N0}.",
                (after - before).Ticks / TimeSpan.TicksPerMillisecond);
        }

        private static void Main(string[] args)
        {
            GetImagesAsync();
        }
}
}
