using System;
using System.Text;
using System.Diagnostics;
using Clases;

namespace Stack
{

    public class Stack
    {
        private uint _numberOfElements;
        private List List { get; }
        public uint NumberOfElements
        {
            get { return _numberOfElements; }
            set { _numberOfElements = value; }
        }


        public Stack(uint number)
        {
            //Invariant();
            NumberOfElements = number;
        }

        public bool Push(Object obj)
        {
            List.Add(obj);
            return List.Contains(obj);
        }

        public bool Pop()
        {
            Object elem = List.GetElement(List.Size() - 1);
            return List.Remove(elem);
        }

        private bool IsEmpty()
        {
            return List.IsEmpty();
        }

        private bool IsFull()
        {
            return List.Size() == NumberOfElements;
        }

        //private void Invariant()
        //{
        //    Debug.Assert()
        //}

        public static void Main()
        {
            Debug.Assert(false, "Message of the assertion not fulfilled ");
            //invariante -> se cumple siempr, checkeas el estado del objeto
            //que no esté vacía y llena
            //o está vacío y el numero de elementos es > 0
            //está lleno y el número de elementos es 0
            //llamar a las invariantes al principio y final de cada método y al final del constructor


            //no es obligatorio poner pre/postcondiciones en los métodos privados, pero sí en los públicos
            //precondiciones: maxNumber no sea = 0
            //precondiciones se hace con if

            //constructor: postcondiciones: checkear que no está vacío
            //push: precondiciones: que no esté llena, que el objeto no sea null
            //postcondiciones: que no esté vacía
            //pop: precondiciones: que no esté vacía
            //postcondiciones: que no esté llena, que no sea null
            //isEmpty(), isFull no tiene precondiciones


        }


    }

}
