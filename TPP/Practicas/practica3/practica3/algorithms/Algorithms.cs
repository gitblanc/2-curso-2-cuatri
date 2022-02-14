
using System;

namespace TPP.Laboratory.ObjectOrientation.Lab03
{

    static class Algorithms
    {

        // Exercise: Implement an IndexOf method that finds the first position (index) 
        // of an object in an array of objects. It should be valid for Person, Angle and future classes.
        public static int? IndexOf(this object[] elements, object target)//el interrogante implica null o entero
        {
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i].Equals(target))
                {
                    return i;
                }
            }
            //return -1;
            return null;//hay que poner que devuelva int?
        }

        public static int? IndexOf2(this object[] elements, object target, IEqualityPredicate predicate = null)//el interrogante implica null o entero
                                                                                                               //predicado cualquier funcion que devuelve un booleano
        {
            for (int i = 0; i < elements.Length; i++)
            {
                if (Compare(elements[i], target, predicate))
                {
                    return i;
                }
            }
            //return -1;
            return null;//hay que poner que devuelva int?
        }

        private static bool Compare(object element, object target, IEqualityPredicate predicate)
        {
            if (predicate == null)
            {
                return element.Equals(target);
            }
            return predicate.Compare(element, target);
        }

        static Person[] CreatePeople()
        {
            string[] firstnames = { "María", "Juan", "Pepe", "Luis", "Carlos", "Miguel", "Cristina" };
            string[] surnames = { "Díaz", "Pérez", "Hevia", "García", "Rodríguez", "Pérez", "Sánchez" };
            int numberOfPeople = 100;

            Person[] printOut = new Person[numberOfPeople];
            Random random = new Random();
            for (int i = 0; i < numberOfPeople; i++)
                printOut[i] = new Person(
                    firstnames[random.Next(0, firstnames.Length)],
                    surnames[random.Next(0, surnames.Length)],
                    random.Next(9000000, 90000000) + "-" + (char)random.Next('A', 'Z')
                    );
            return printOut;
        }

        static Angle[] CreateAngles()
        {
            Angle[] angles = new Angle[(int)(Math.PI * 2 * 10)];
            int i = 0;
            while (i < angles.Length)
            {
                angles[i] = new Angle(i / 10.0);
                i++;
            }
            return angles;
        }

        static void Main()
        {
            var people = CreatePeople();
            var person0 = people[75];
            var indexPerson = people.IndexOf(person0);
            if (indexPerson.HasValue)
            {
                Console.WriteLine("index person = {0}", indexPerson);//forma 1
                Console.WriteLine($"index person = {indexPerson.Value} = {people[indexPerson.Value]}");//forma 2, interpolación
            }
            else
            {
                Console.WriteLine("El valor es null");
            }

            //angle no tiene implementado el equals
            var angles = CreateAngles();
            var angle0 = angles[14];
            var indexAngle = angles.IndexOf(angle0);
            if (indexAngle.HasValue)
            {
                Console.WriteLine("index angle = {0}", indexAngle);//forma 1
                Console.WriteLine($"index angle = {angle0.Radians} = {angles[indexAngle.Value]}");//forma 2, interpolación
            }
            else
            {
                Console.WriteLine("El valor es null");
            }

            var indexPerson2 = people.IndexOf2(person0, predicate: new SameFirstName());
            if (indexPerson.HasValue)
            {
                Console.WriteLine("index person = {0}", indexPerson);//forma 1
                Console.WriteLine($"index person = {indexPerson.Value} = {people[indexPerson2.Value]}");//forma 2, interpolación
            }
            else
            {
                Console.WriteLine("El valor es null");
            }

            var indexAngle2 = angles.IndexOf2(angle0, predicate: new SameCuadrant());
            if (indexAngle.HasValue)
            {
                Console.WriteLine("index angle = {0}", indexAngle);//forma 1
                Console.WriteLine($"index angle = {indexAngle2.Value} = {angles[indexAngle2.Value]}");//forma 2, interpolación
            }
            else
            {
                Console.WriteLine("El valor es null");
            }
        }

    }

    internal class SameCuadrant : IEqualityPredicate
    {
        public bool Compare(object element, object target)
        {
            Angle a1 = element as Angle;
            if (a1 == null)
            {
                return false;
            }
            Angle a2 = target as Angle;
            if (a2 == null)
            {
                return false;
            }
            return Quadrant(a1.Radians) == Quadrant(a2.Radians);
        }

        private uint Quadrant(double angle)
        {
            if (angle > 2 * Math.PI)//si diese más de una vuelta
            {
                return Quadrant(angle % 2 * Math.PI);
            }
            if (angle < 0)//si fuese negativo
            {
                return Quadrant(angle + 2 * Math.PI);
            }
            if (angle <= Math.PI / 2) {
                return 1;
            }
            if (angle <= Math.PI )
            {
                return 2;
            }
            if (angle <= 3*Math.PI / 2)
            {
                return 3;
            }
            return 4;
        }
    }

    internal class SameFirstName : IEqualityPredicate
    {
        public bool Compare(object element, object target)//ha de devolver bool Importante
        {
            //COMPROBAR QUE SON PERSONAS
            Person p1 = element as Person;
            if (p1 == null)
            {
                return false;
            }
            Person p2 = target as Person;
            if (p2 == null)
            {
                return false;
            }

            return p1.FirstName == p2.FirstName;//se devuelve la comparación de los apellidos
        }
    }

    internal interface IEqualityPredicate
    {
        bool Compare(object element, object target);
    }
}
