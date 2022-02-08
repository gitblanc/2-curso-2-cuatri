using System;

namespace parameters2
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = 1;
            int y = 2;
            Swap(ref x, ref y);
            Console.WriteLine("x = {0}\ny = {1}", x, y);

            AskData(out string firstName, out string surname, out string idNumber);//referencia solo escritura
            Console.WriteLine("firstName = {0}\nsurname = {1}idNumber = {2}\n", firstName, surname, idNumber);
        }

        //referencias
        static void Swap(ref int a, ref int b)
        {
            int aux = a;
            a = b;
            b = aux;
        }

        static void AskData(out string firstName, out string surname, out string idNumber)
        {
            Console.Write("First name: ");
            firstName = Console.In.ReadLine();

            Console.Write("Surname: ");
            surname = Console.In.ReadLine();

            Console.Write("Id number: ");
            idNumber = Console.In.ReadLine();
        }
    }
}
