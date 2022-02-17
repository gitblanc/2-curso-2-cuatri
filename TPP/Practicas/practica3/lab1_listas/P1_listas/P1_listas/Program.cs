using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConjuntoV1;

namespace P1_listas
{
    class Program
    {
        static void Main(string[] args)
        {
            Conjunto c = new Conjunto();
            Conjunto c2 = new Conjunto();
            c += 1;
            c += 2;
            c += 3;

            c2 += 2;
            c2 += 3;
            c2 += 4;

            c |= c2;//se unen los conjuntos

        }
    }
}
