using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace task.exception
{
    class TaskException
    {
        static void CaptureException()
        {
            try
            {
                var task = Task.Run(() => { throw new ArgumentNullException(); });
                task.Wait();
            }
            catch (AggregateException e)
            {
                Console.WriteLine("Task throwed the following exception: " + e);
            }
            return;
        }

        static void ReThrowException()
        {
            try
            {
                var task = Task.Run(() => { throw new ArgumentNullException(); });
                task.Wait();
            }
            catch (AggregateException e)
            {
                Exception[] list = new Exception[] { e };
                throw new AggregateException("Exception rethrown as AggregateException", list);
            }
            return;
        }
        static void Main(string[] args)
        {
            CaptureException();
            try
            {
                ReThrowException();
            }
            catch (AggregateException e)
            {
                Console.WriteLine("Task throwed the following exception: " + e);
            }
            return;
        }
    }
}
