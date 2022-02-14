using System;
using System.Text;
using System.Diagnostics;

namespace TPP.Laboratory.ObjectOrientation.Lab03
{

    public class Stack
    {

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