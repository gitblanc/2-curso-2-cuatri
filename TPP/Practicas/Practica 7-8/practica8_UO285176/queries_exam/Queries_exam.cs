using model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace queries_exam
{

    class Queries_exam
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
            Queries_exam query = new Queries_exam();
            query.Consulta1();
            query.Consulta2();
            query.Consulta3();
            query.Consulta4();
            query.Consulta5();
            query.Consulta6();
            query.Consulta7();
            query.Homework5();
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
            var employees = model.Employees.OrderBy(e => e.Age)//ordenados por edad
                .Where(e => e.Department.Name.ToLower().Equals("computer science"))//empleados pertenecientes al departamento computer science
                .Where(e => e.Office.Building.ToLower().Equals("faculty of science"))//despacho en la facultad de ciencias
                .Where(e => model.PhoneCalls.Where(c => c.SourceNumber.Equals(e.TelephoneNumber))
                .Any(c => c.Seconds >= 60)).Select(e => e.Name);
            Console.WriteLine("Employees:");
            Show(employees);
        }

        private void Homework2()
        {
            // Show the summation, in seconds, of the phone calls done by the employees of the Computer Science department
            var calls = model.PhoneCalls.Where(c => model.Employees
            .Where(e => e.Department.Name.ToLower().Equals("computer science"))
            .Any(e => e.TelephoneNumber.Equals(c.SourceNumber))).Aggregate(0, (a, b) => a + b.Seconds);
            Console.WriteLine("Suma Llamadas:");
            Console.WriteLine(calls);
        }

        private void Homework3()
        {
            // Show the phone calls done by each department, ordered by department names. 
            // Each line must show “Department = <Name>, Duration = <Seconds>”
            var calls = model.Departments
                .OrderBy(d => d.Name)
                .Select(d => $"Department={d.Name} -- Duration=" +
                d.Employees.SelectMany(e => model.PhoneCalls.Where(c => c.SourceNumber.Equals(e.TelephoneNumber)))
                .Aggregate(0, (a, c) => a + c.Seconds));
            Console.WriteLine("Department calls: ");
            Show(calls);
        }

        private void Homework4()
        {
            // Show the departments with the youngest employee, 
            // together with the name of the youngest employee and his/her age 
            // (more than one youngest employee may exist)

        }

        private void Homework5()
        {
            // Show the greatest summation of phone call durations, in seconds, 
            // of the employees in the same department, together with the name of the department 
            // (it can be assumed that there is only one department fulfilling that condition)
            var res = model.Employees
                .Join(model.PhoneCalls,
                e => e.TelephoneNumber,
                c => c.SourceNumber,
                (e, c) => new
                {
                    Department = e.Department,
                    Duration = c.Seconds
                }).GroupBy(e => e.Department, (a, b) => new
                { 
                    name = a.Name,
                    duration = b.Aggregate(0, (a,c) => a + c.Duration)
                });
            Show(res);

        }
        private void Consulta1()
        {
            // Modificar la consulta para mostrar los empleados cuyo nombre empieza por F.
            var resultado = model.Employees.Where(e => e.Name.StartsWith("F"))
                .Select(e => e.Name)
                ;
            Show(resultado);
            //El resultado esperado es: Felipe
        }

        private void Consulta2()
        {
            //Mostrar Nombre y fecha de nacimiento de los empleados de Cantabria con el formato:
            // Nombre=<Nombre>,Fecha=<Fecha>

            var res = model.Employees.Where(e => e.Province.ToLower().Equals("cantabria"))
                .Select(e => new
                {
                    Nombre = e.Name,
                    Fecha = e.DateOfBirth
                });
            Show(res);

            /*El resultado esperado es:
              Alvaro 19/10/1945 0:00:00
              Dario 16/12/1973 0:00:00
            */
        }



        // A partir de aquí, necesitaréis métodos presentes en: https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable?view=net-5.0

        private void Consulta3()
        {
            //Mostrar los nombres de los departamentos que tengan más de un empleado mayor de edad.
            var deps = model.Departments.Where(d => d.Employees.Where(e => e.Age >= 18)
                .Where(e => e.Office.Number.Equals("2.1")).Count() > 1).Select(d => d.Name);
            Show(deps);

            /*El resultado esperado es:
                Computer Science
                Medicine
            */


            //Posteriormente, modifica la consulta para que:
            //Muestre los nombres de los departamentos que tengan más de un empleado mayor de edad
            //y
            //que el despacho (Office.Number) COMIENCE por "2.1"

            //El resultado esperado es: Medicine
        }

        private void Consulta4()
        {
            //El nombre de los departamentos donde ningún empleado tenga despacho en el Building "Faculty of Science".
            //Resultado esperado: [Department: Mathematics]
            var deps = model.Departments.Where(d => d.Employees.Where(e => e.Office.Building.ToLower().Equals("faculty of science"))
                .Count() == 0).Select(d => $"Department [{d.Name}]");
            Show(deps);
            var deps2 = model.Departments.Where(d => !d.Employees.Any(e => e.Office.Building.ToLower().Equals("faculty of science")))
               .Select(d => $"Department [{d.Name}]");
            Show(deps2);
        }

        private void Consulta5()
        {
            // Mostrar las llamadas de teléfono de más de 5 segundos de duración para cada empleado que tenga más de 50 años
            //Cada línea debería mostrar el nombre del empleado y la duración de la llamada en segundos.
            //El resultado debe estar ordenado por duración de las llamadas (de más a menos).

            //var calls = model.Employees
            //    .Where(e => e.Age > 50)
            //    .Select(e => $"Nombre= {e.Name}, Total calls = " + model.PhoneCalls
            //    .Where(c => c.SourceNumber.Equals(e.TelephoneNumber))
            //    .Where(c => c.Seconds > 5).Aggregate(0, (a, b) => a + b.Seconds));

            var calls = model.PhoneCalls
                .Where(c => c.Seconds > 5)
                .OrderByDescending(c => c.Seconds)
                .Join(model.Employees.Where(e => e.Age > 50),
                    c => c.SourceNumber,
                    e => e.TelephoneNumber,
                    (c, e) => $"Nombre = {e.Name} Duration = {c.Seconds}");
            Show(calls);
        }



        private void Consulta6()
        {
            //Mostrar la llamada más larga para cada profesor, mostrando por pantalla: Nombre_profesor : duracion_llamada_mas_larga
            var calls = model.Employees
                .Where(e => model.PhoneCalls.Any(c => c.SourceNumber.Equals(e.TelephoneNumber)))
                .Select(e => model.PhoneCalls.Where(c => c.SourceNumber.Equals(e.TelephoneNumber))
                .Aggregate((a, b) => a.Seconds > b.Seconds ? a : b))
                .Join(model.Employees,
                c => c.SourceNumber,
                e => e.TelephoneNumber,
                (c, e) => $"Nombre = {e.Name} Duration = {c.Seconds}");
            Show(calls);
        }


        private void Consulta7()
        {
            // Mostrar, agrupados por provincia, el nombre de los empleados
            //Tanto la provincia como los empleados deben estar ordenados alfabéticamente

            var res = model.Employees
                .OrderBy(e => e.Name)
                .OrderBy(e => e.Province)
                .GroupBy(e => e.Province, (p, e) => new
                {
                    Provincia = p,
                    Empleados = e.Aggregate("", (a, b) => a + " " + b.Name)
                });
            Show(res);
            /*Resultado esperado:
                Alicante : Carlos
                Asturias : Bernardo Felipe
                Cantabria : Alvaro Dario               
                Granada : Eduardo

            */
        }


    }

}
