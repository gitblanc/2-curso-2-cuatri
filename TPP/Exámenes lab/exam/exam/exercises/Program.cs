using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TPP.Exam.PI2.Restaurant;


namespace TPP.Exam.PI2
{
    class Program
    {
        static void Main(string[] args)
        {
            Exercise1();
            Exercise2();
            Exercise3();
        }

        static void Exercise1()
        {
            
        }

        static void Exercise2()
        {
            Random r = new Random(DateTime.Now.Millisecond);

        }

        static void Exercise3()
        {
            //Query 1: Use 'WellFormedMenus'
            //unhealthy menus (not varied enough): If 2 or more dishes have Potato as MainIngredient (Starter, MainCourse, MainCourse-SideDish and Dessert)
            var menus = Menu.WellFormedMenus();


            //Query 2: Use 'RandomMenus'
            //For all the possible pairs of Random menus that are well formed and share the Starter, create a new menu with that Starter, the MainCourse of the first menu and Dessert of the second one
            //Well formed menus are those with a SideDish or Soup as starter, MainCourse as main and Dessert as dessert
            menus = Menu.RandomMenus();

        }


    }
}
