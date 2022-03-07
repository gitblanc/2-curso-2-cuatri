using System;
using System.Collections.Generic;
using System.Text;
using paradigma.funcional;

namespace delegates
{
    static class Main1
    {
        static void Main(string[] args)
        {
            Person[] people = Factory.CreatePeople();
            Angle[] angles = Factory.CreateAngles();
            foreach (Person p in people)
            {
                Console.WriteLine(p.ToString());
            }
            foreach (Angle a in angles)
            {
                Console.WriteLine(a.ToString());
            }
            //---------------FIND------------------
            Console.WriteLine("----------------------------------------");//personas
            Console.WriteLine(people.Find(x => x.Surname.Equals("Pérez")));//devuelve el primero que cumple
            var z = people.Find(x => x.FirstName.Equals("No existe"));
            Console.WriteLine((z == null) ? "null" : "expected null");//valor por defecto
            Console.WriteLine("----------------------------------------");//angulos
            Console.WriteLine(angles.Find(x => x.Sine().Equals(1)));//devuelve el primero que cumple
            Console.WriteLine(angles.Find(x => x.Cosine().Equals(1)));//devuelve el primero que cumple
            var a0 = angles.Find(x => x.Degrees.Equals("No existe"));
            Console.WriteLine((a0 == null) ? "null" : "expected null");//valor por defecto

            //---------------FILTER------------------
            IEnumerable<Person> filtro = people.Filter(x => x.FirstName.Equals("María"));
            Show(filtro);
            IEnumerable<Angle> filtroAngle = angles.Filter(x => x.Cosine().Equals(1));
            Show(filtroAngle);

            //---------------REDUCE------------------

        }

        static void Show<T>(this IEnumerable<T> collection)
        {
            foreach (T d in collection)
            {
                Console.WriteLine($"{d} ");
            }
            Console.WriteLine();
        }

    }
}
