using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clases;

namespace P1_listas
{
    class Program
    {
        static void Main(string[] args)
        {
            List list = new List();
            //Se añaden 5 elementos -> Add()
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);
            Console.WriteLine(list.ToString());

            
        }
    }
}
