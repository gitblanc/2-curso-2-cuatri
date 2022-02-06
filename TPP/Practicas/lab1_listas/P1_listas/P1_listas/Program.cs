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
            //Se imprimirá que no haya elementos
            list.PrintElements();
            //Se añaden 5 elementos -> Add()
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);
            list.PrintElements();

            //Se buscan x elementos -> GetElement()
            //existe
            int elem = list.GetElement(4);
            if (elem == -1) {
                Console.WriteLine();
                Console.WriteLine("El elemento deseado no existe");
            }
            else {
                Console.WriteLine(elem);
            }

            //no existe
            int elem2 = list.GetElement(0);
            if (elem2 == -1)
            {
                Console.WriteLine();
                Console.WriteLine("El elemento deseado no existe");
            }
            else
            {
                Console.WriteLine(elem2);
            }

            //Se eliminan todos los elementos -> Remove()
            list.Remove(2);
            list.PrintElements();
            list.Remove(1);
            list.PrintElements();
            list.Remove(5);
            list.PrintElements();
            list.Remove(3);
            list.PrintElements();
            list.Remove(4);
            list.PrintElements();
            ////se elimina un elemento que no existe estando la lista vacía
            list.Remove(34);
            //se reañaden 3 elementos
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.PrintElements();
            //se intenta eliminar uno que no está en la lista
            list.Remove(340000);
        }
    }
}
