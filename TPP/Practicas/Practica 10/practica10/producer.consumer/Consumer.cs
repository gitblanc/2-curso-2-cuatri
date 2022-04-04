using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TPP.Laboratory.Concurrency.Lab10
{

    class Consumer
    {

        private Queue<Product> queue;

        public Consumer(Queue<Product> queue)
        {
            this.queue = queue;
        }

        public void Run()
        {
            Random random = new Random();
            while (true)
            {
                Console.WriteLine("- Dequeuing a product...");
                Product product = null;
                //lock(queue) {//dentro del lock hay que hacer cosas que tarden muy poco, hay que minimizar el tiempo del lock
                //    while (queue.Count == 0)
                //        Thread.Sleep(100);
                //    product = queue.Dequeue();
                //}
                while (product == null)
                {
                    lock (queue)//hay que minimizar las acciones del lock
                    {
                        if (queue.Count > 0)//si hay material
                            product = queue.Dequeue();
                    }
                    if (product == null)
                        Thread.Sleep(100);//esperamos fuera para no tener la cola ocupada
                }
                Console.WriteLine("- Dequeued {0}.", product);
                Thread.Sleep(random.Next(300, 700));
            }
        }

    }
}
