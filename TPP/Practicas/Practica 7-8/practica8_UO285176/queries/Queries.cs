using model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace queries
{

    class Query
    {

        private Model model = new Model();

        private static void Show<T>(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Number of items in the collection: {0}.", collection.Count());
        }

        static void Main(string[] args)
        {
            Query query = new Query();
            query.Homework1();
            query.Homework2();
            query.Homework4();
        }

        private void Query1()
        {
            // Modify this query to show the names of the employees older than 50 years
            var employees = model.Employees.
                Where(e => e.Age > 50).
                Select(e => e.Name);//consultas
            Console.WriteLine("Employees:");
            Show(employees);
        }

        private class NameAndEmail
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public override string ToString()
            {
                return $"Name { Name} and Email {Email}";
            }
        }
        private void Query2()
        {
            // Show the name and email of the employees who work in Asturias
            var employees = model.Employees.
                Where(e => e.Province.ToLower().Equals("asturias")).//el toLower es para ahorrarnos problemas
                Select(e => new NameAndEmail
                {
                    Name = e.Name,
                    Email = e.Email

                });
            ;
            Show(employees);

        }

        private void Query2b()//tipos anónimos
        {
            // Show the name and email of the employees who work in Asturias
            var employees = model.Employees.
                Where(e => e.Province.ToLower().Equals("asturias")).//el toLower es para ahorrarnos problemas
                Select(e => new
                {
                    e.Name,
                    e.Email

                });
            ;
            Show(employees);
        }

        private void Query2c()//tuplas
        {
            // Show the name and email of the employees who work in Asturias
            var employees = model.Employees.
                Where(e => e.Province.ToLower().Equals("asturias")).//el toLower es para ahorrarnos problemas
                Select(e => (e.Name, e.Email));
            Show(employees);
        }
        // Notice: from now on, check out http://msdn.microsoft.com/en-us/library/9eekhta0.aspx

        private void Query3()
        {
            // Show the names of the departments with more than one employee 18 years old and beyond; 
            // the department should also have any office number starting with "2.1"
            var departments = model.Departments.Where(d => d.Employees.Count(e => e.Age >= 18) > 1).
                Where(d => d.Employees.Any(e => e.Office.Number.StartsWith("2.1"))).
                Select(d => d.Name);
            Console.WriteLine("Depatments: ");
            Show(departments);
        }

        private void Query4a()
        {
            // Show the phone calls of each employee. 
            // Each line should show the name of the employee and the phone call duration in seconds.
            var phonecalls = model.PhoneCalls.
                Where(c => model.Employees.Any(e => e.TelephoneNumber.Equals(c.SourceNumber))).
                Select(c => new
                {
                    Employee = model.Employees.First(e => e.TelephoneNumber.Equals(c.SourceNumber)),
                    Duration = c.Seconds
                });
            Console.WriteLine("Calls: ");
            Show(phonecalls);
        }

        private void Query4b()
        {
            // Show the phone calls of each employee. 
            // Each line should show the name of the employee and the phone call duration in seconds.
            var phonecalls = model.Employees.
                SelectMany(e => model.PhoneCalls.Where(c => c.SourceNumber.Equals(e.TelephoneNumber)))
                .Select(c => new
                {
                    Employee = model.Employees.First(e => e.TelephoneNumber.Equals(c.SourceNumber)),
                    Duration = c.Seconds
                }); ;
            //SELECTMANY hace el select y baja un nivel(listadelistas->lista)


            Console.WriteLine("Calls: ");
            Show(phonecalls);
        }

        private void Query4c()
        {
            // Show the phone calls of each employee. 
            // Each line should show the name of the employee and the phone call duration in seconds.
            var phonecalls = model.Employees.Join(model.PhoneCalls,
                e => e.TelephoneNumber, c => c.SourceNumber,
                (e, c) => new
                {
                    Employee = e.Name,
                    Duration = c.Seconds
                });
            Console.WriteLine("Calls: ");
            Show(phonecalls);
        }

        private void Query5()
        {
            // Show, grouped by each province, the name of the employees 
            // (both province and employees must be lexicographically ordered)
            var employeesByProvince = model.Employees
                .OrderBy(e => e.Name)//ordenar según nombre
                .GroupBy(e => e.Province).OrderBy(g => g.Key);//ordenar según clave
            Console.WriteLine("Employees by province: ");
            foreach (var provinceEmployees in employeesByProvince)
            {
                Console.WriteLine(provinceEmployees.Key);//devuelve la provincia
                foreach (var p in provinceEmployees)
                {
                    Console.WriteLine($"    {p.Name}");
                }
            }
        }

        private void Query6()
        {
            //Mostrar las llamadas ordenadas por duración y ranking(La de más duración la primera)
            //La función ZIP() devuelve una lista de tuplas, usar solo cuando las listas tengan el mismo tamaño
            //int i = 0;//closure-> cláusula, se podría hacer con esto, pero no es purod
            var calls = model.PhoneCalls
                .OrderByDescending(c => c.Seconds)
                .Zip(Enumerable.Range(1, model.PhoneCalls.Count() + 1),
                (c, i) => $"Rank {i}---{c.Seconds} s  {c.SourceNumber} -> {c.DestinationNumber}");
            //para ver que funciona .Select(c => c.Seconds);
            Console.WriteLine("Calls ranking: ");
            Show(calls);
        }

        /************ Homework **********************************/

        private void Homework1()
        {
            // Show, ordered by age, the names of the employees in the Computer Science department, 
            // who have an office in the Faculty of Science, 
            // and who have done phone calls longer than one minute
            var name = model.Employees.Where(e => e.Department.Name.ToLower().Equals("computer science"))
                .Where(e => e.Office.Building.ToLower().Equals("faculty of science"))
                .Where(e => model.PhoneCalls.Where(c => c.SourceNumber.Equals(e.TelephoneNumber))
                .Any(c => c.Seconds >= 60))
                .OrderBy(e => e.Age).Select(e => e.Name);
            Console.WriteLine("Employees: ");
            Show(name);//sale Bernardo
        }

        private void Homework2()
        {
            // Show the summation, in seconds, of the phone calls done by the employees of the Computer Science department
            var phonecalls = model.Employees.Where(e => e.Department.Name.ToLower().Equals("computer science"))
                .Join(model.PhoneCalls,
                e => e.TelephoneNumber, c => c.SourceNumber,
                (e, c) => new
                {
                    Employee = e.Name,
                    Duration = c.Seconds //+ c.Seconds
                });
            Console.WriteLine("Calls: ");
            Show(phonecalls);
        }

        private void Homework3()
        {
            // Show the phone calls done by each department, ordered by department names. 
            // Each line must show “Department = <Name>, Duration = <Seconds>”
        }

        private void Homework4()
        {
            // Show the departments with the youngest employee, 
            // together with the name of the youngest employee and his/her age 
            // (more than one youngest employee may exist)
            var departamentos = model.Employees.OrderBy(e => e.Age).GroupBy(e => e.Department);
            Console.WriteLine("Departments: ");
            Show(departamentos);
        }

        private void Homework5()
        {
            // Show the greatest summation of phone call durations, in seconds, 
            // of the employees in the same department, together with the name of the department 
            // (it can be assumed that there is only one department fulfilling that condition)
        }


    }

}
