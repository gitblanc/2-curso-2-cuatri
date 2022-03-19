using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace model
{

    public class Department
    {

        public Department(string name, IEnumerable<Employee> employees)
        {
            Name = name;
            Employees = employees;
        }

        public string Name { get; private set; }

        public IEnumerable<Employee> Employees { get; private set; }

        public override string ToString()
        {
            return string.Format("[Department: {0}]", Name);
        }
    }
}
