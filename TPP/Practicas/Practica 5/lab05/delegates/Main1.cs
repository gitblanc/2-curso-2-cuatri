using System;
using System.Collections.Generic;
using System.Text;
using paradigma.funcional;

namespace delegates
{
    class Main1
    {
        static void Main(string[] args)
        {
            Person[] people = Factory.CreatePeople();
            Angle[] angles = Factory.CreateAngles();

           Console.WriteLine(people.Find(x => x.Surname.Equals("Díaz")));
            
            
        }
        
    }
}
