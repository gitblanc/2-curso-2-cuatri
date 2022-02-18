﻿using System;
using System.Text;
using System.Diagnostics;
using Clases;

namespace Stack
{

    public class Stack
    {
        private uint _numberOfElements;
        private List List { get; }
        private bool _isEmpty;
        private bool _isFull;

        public bool IsEmpty
        {
            get { return List.IsEmpty(); }
            set { _isEmpty = value; }
        }

        public bool IsFull
        {
            get { return List.Size() == NumberOfElements; }
            set { _isFull = value; }
        }
        public uint NumberOfElements
        {
            get { return _numberOfElements; }
            set { _numberOfElements = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Stack(uint number)
        {

            NumberOfElements = number;
            IsEmpty = false;
            IsFull = false;
            List = new List();
            Invariant();
        }

        public bool Push(Object obj)
        {
            //Preconditions:
            if (NumberOfElements == 0)
                throw new InvalidOperationException("The maximum number of elements is equal to zero");
            if (IsFull)
                throw new InvalidOperationException("The stack is full");
            if (obj == null)
                throw new ArgumentNullException("The parameter was null");
            Invariant();
            if (NumberOfElements < List.Size())
            {
                List.Add(obj);
            }
            else
            {
                IsFull = true;
            }
            Invariant();
            //Postconditions
            if (IsEmpty)
                throw new InvalidOperationException("The stack shouldn't be empty");
            return List.Contains(obj);
        }

        public bool Pop()
        {
            //Preconditions:
            if (IsEmpty)
                throw new InvalidOperationException("The stack shouldn't be empty");
            Invariant();
            Object elem = List.GetElement(List.Size() - 1);
            Invariant();
            List.Remove(elem);
            //Postconditions
            if(IsFull)
                throw new InvalidOperationException("The stack shouldn't be full");
            if(List == null)
                throw new InvalidOperationException("The list shouldn't be null");
            return !List.Contains(elem);//si lo contiene, no se ha borrado y viceversa

        }



        private void Invariant()
        {
            Debug.Assert(IsFull && IsEmpty, "Stack is both full and empty");//la pila no esta llena y vacía
            Debug.Assert(NumberOfElements > 0 && IsEmpty, "Stack shouldn't be empty");//está vacío y el numero de elementos es > 0
            Debug.Assert(NumberOfElements == 0 && IsFull, "Stack should be empty");
        }

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
