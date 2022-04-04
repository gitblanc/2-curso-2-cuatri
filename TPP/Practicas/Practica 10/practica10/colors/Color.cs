using System;

namespace TPP.Laboratory.Concurrency.Lab10
{

    public class Color
    {

        private ConsoleColor color;

        public Color(ConsoleColor color)
        {
            this.color = color;
        }

        virtual public void Show()
        {
            lock (Console.Out)//lock se lo pones a uyn objeto (cualquier cosa que se pase por referencia)
            {
                ConsoleColor previousColor = Console.ForegroundColor;
                Console.ForegroundColor = this.color;//Aquí se accede a ForeroundColor
                Console.Write("{0}\t", this.color);//aquí se accede a Console.out
                Console.ForegroundColor = previousColor;
            }

        }

    }
}
