
using System;

namespace TPP.Laboratory.ObjectOrientation.Lab02 {

    /// <summary>
    /// Default argument values
    /// </summary>
    class DefaultArgumentValues {


        /// <summary>
        /// This method filters all the people with:
        /// - A specific first name
        /// - A specific surname name
        /// - A part of an ID number
        /// Comparison should not be case-sensitive
        /// Returns the people fulfilling the criteria
        /// </summary>
        static Person[] Filter(Person[] people, string firstName = null, string surname = null, string idNumberContains = null) {
            Person[] filteredPeople = new Person[people.Length];
            int cont = 0;
            foreach (Person p in people) {
                if ((firstName != null && firstName == p.FirstName)
                    || (surname != null && surname == p.Surname)
                    || (idNumberContains != null && p.IDNumber.Contains(idNumberContains)))
                {
                    filteredPeople[cont] = p;
                    cont++;
                }
            }
            Array.Resize(ref filteredPeople, cont);//se eliminan espacios en blanco
            return filteredPeople;
        }


        /// <summary>
        /// Creates a random list of people 
        /// </summary>
        /// <returns></returns>
        static Person[] CreatePeople() {
            string[] firstNames = { "María", "Juan", "Pepe", "Luis", "Carlos", "Miguel", "Cristina" };
            string[] surnames = { "Díaz", "Pérez", "Hevia", "García", "Rodríguez", "Pérez", "Sánchez" };
            int numberOfPeople = 100;

            Person[] printout = new Person[numberOfPeople];
            Random random = new Random();
            for (int i = 0; i < numberOfPeople; i++)
                printout[i] = new Person(
                    firstNames[random.Next(0, firstNames.Length)],
                    surnames[random.Next(0, surnames.Length)],
                    random.Next(9000000, 90000000) + "-" + (char)random.Next('A', 'Z')
                    );
            return printout;
        }

        /// <summary>
        /// Shows a list of people in the console
        /// </summary>
        static void Show(Person[] people) {
            foreach (Person person in people) {
                Console.WriteLine(person);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Complete the following code
        /// </summary>
        static void Main() {
            Person[] people = CreatePeople();
            Console.WriteLine("People named María:");
            Show(Filter(people, "María"));

            Console.WriteLine("People whose surname is Pérez:");
            Show(Filter(people, surname: "Pérez"));

            Console.WriteLine("People whose ID number contains A");
            Show(Filter(people, idNumberContains: "María"));//

        }
    }

}
