using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPP.Exam.PI2.Restaurant
{
    public class Menu
    {
        protected Dish starter;
        protected Dish main_dish;
        protected Dish dessert;


        public Menu(Dish starter, Dish main, Dish desserts)
        {
            this.starter = starter;
            this.main_dish = main;
            this.dessert = desserts;
        }
        //// NEW CODE
        public Menu()
        {
            this.starter = null;
            this.main_dish = null;
            this.dessert = null;
        }
        public Dish Starter { set { this.starter = value; } get { return this.starter; } }
        public Dish Main { set { this.main_dish = value; } get { return this.main_dish; } }
        public Dish Dessert { set { this.dessert = value; } get { return this.dessert; } }

        public override bool Equals(object obj)
        {
            if(obj is Menu)
            {
                Menu other = (Menu)obj;
                this.ToString().Equals(other.ToString());
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash = 0;
            if (this.starter != null)
                hash += this.starter.GetHashCode();
            if (this.main_dish != null)
                hash += this.main_dish.GetHashCode();
            if (this.dessert != null)
                hash += this.dessert.GetHashCode();
            return hash;
        }
        public override string ToString()
        {
            return String.Format("{0}#{1}#{2}", this.starter != null ? this.starter.Name : "",
                this.main_dish != null ? this.main_dish.Name : "",
                this.dessert != null ? this.dessert.Name : "");
        }

        public static IEnumerable<Dish> InitializeDishes()
        {
            Dish soup1 = new Soup("Tomato");
            Dish soup2 = new Soup("Lentil");
            Dish soup3 = new Soup("Potato");
            Dish soup4 = new Soup("Baked Potato");
            Dish soup5 = new Soup("Noodle");
            Dish soup6 = new Soup("Cream of Broccoli");
            Dish soup7 = new Soup("Mushroom and Potato");
            Dish soup8 = new Soup("Ham and Bean");
            Dish soup9 = new Soup("Chicken Noodle");
            Dish soup10 = new Soup("Pumpin Noodle");

            Dish side1 = new SideDish("Jalapeno Snack", "Jalapeno");
            Dish side2 = new SideDish("Double Tomato Bruschetta", "Tomato");
            Dish side3 = new SideDish("Guacamole", "Avocado");
            Dish side4 = new SideDish("Patatas Bravas", "Potato");
            Dish side5 = new SideDish("Blueberries", "Blueberries");
            Dish side6 = new SideDish("Caprese Salad", "Tomato");
            Dish side7 = new SideDish("Carrot Sticks", "Carrot");
            Dish side8 = new SideDish("Cottage Cheese Salad", "Cottage Cheese");
            Dish side9 = new SideDish("Baked Sweet Potato Chips", "Potato");
            Dish side10 = new SideDish("Smashed Potatoes", "Potato");

            Dish main1 = new MainCourse("Pork Chops with Salad", "Pork", side6 as SideDish);
            Dish main2 = new MainCourse("Pasta Bolognese", "Pasta", new SideDish("Tomato Sauce", "Tomato"));
            Dish main3 = new MainCourse("Chicken with Potatoes", "Chicken and Potato", side4 as SideDish);
            Dish main4 = new MainCourse("Grilled Scallops with Guacamole", "Scallops", side3 as SideDish);
            Dish main5 = new MainCourse("Pork with berries", "Pork", side5 as SideDish);
            Dish main6 = new MainCourse("Paella with Chorizo", "Rice", new SideDish("Chorizo", "Chorizo"));
            Dish main7 = new MainCourse("Salmon with Chips", "Salmo and Potato", side9 as SideDish);
            Dish main8 = new MainCourse("Chicken with Jalapenos", "Chicken", side1 as SideDish);
            Dish main9 = new MainCourse("Grilled Steak with Carrots", "Steak", side7 as SideDish);
            Dish main10 = new MainCourse("Rissotto with Mushrooms", "Rice", new SideDish("", ""));

            Dish dessert1 = new Dessert("Chocolate Brownies", "Chocolate");
            Dish dessert2 = new Dessert("Carrot Cake", "Carrot");
            Dish dessert3 = new Dessert("Mud Cake", "Chocolate");
            Dish dessert4 = new Dessert("Mixed Berry Pie", "Blueberries");
            Dish dessert5 = new Dessert("Sweet Potato Tea Cake", "Potato");
            Dish dessert6 = new Dessert("Banana split", "Banana");
            Dish dessert7 = new Dessert("Peanut Butter Cookies", "Peanut Butter");
            Dish dessert8 = new Dessert("Potato-Dough Donuts", "Potato");
            Dish dessert9 = new Dessert("Chocolate Chip Cookies", "Chocolate");
            Dish dessert10 = new Dessert("Apple Pie", "Apple");

            IList<Dish> dishes = new List<Dish>();
            dishes.Add(soup1); dishes.Add(soup2); dishes.Add(soup3); dishes.Add(soup4); dishes.Add(soup5);
            dishes.Add(soup6); dishes.Add(soup7); dishes.Add(soup8); dishes.Add(soup9); dishes.Add(soup10);

            dishes.Add(side1); dishes.Add(side2); dishes.Add(side3); dishes.Add(side4); dishes.Add(side5);
            dishes.Add(side6); dishes.Add(side7); dishes.Add(side8); dishes.Add(side9); dishes.Add(side10);

            dishes.Add(main1); dishes.Add(main2); dishes.Add(main3); dishes.Add(main4); dishes.Add(main5);
            dishes.Add(main6); dishes.Add(main7); dishes.Add(main8); dishes.Add(main9); dishes.Add(main10);

            dishes.Add(dessert1); dishes.Add(dessert2); dishes.Add(dessert3); dishes.Add(dessert4); dishes.Add(dessert5);
            dishes.Add(dessert6); dishes.Add(dessert7); dishes.Add(dessert8); dishes.Add(dessert9); dishes.Add(dessert10);
            return dishes;
        }

        public static IEnumerable<Menu> WellFormedMenus()
        {
            var dishes = Menu.InitializeDishes();
            var count = dishes.Count();
            //A random seed produces same random sequence, and hence, same menusNotChecked List. Do not change this method
            Random r = new Random(12345); 
            List<Menu> menusNotChecked = new List<Menu>();
            for(int i = 0; i < 35; i++)
            {
                
                menusNotChecked.Add(new Menu(dishes.ElementAt(r.Next(0, 20)), dishes.ElementAt(r.Next(20, 30)), dishes.ElementAt(r.Next(30, count))));
            }
            return menusNotChecked;
        }

        public static IEnumerable<Menu> RandomMenus()
        {
            var dishes = Menu.InitializeDishes();
            var count = dishes.Count();
            //A random seed produces same random sequence, and hence, same menusNotChecked List. Do not change this method
            Random r = new Random(1235);
            List<Menu> menusNotChecked = new List<Menu>();
            for (int i = 0; i < 200; i++)
            {

                menusNotChecked.Add(new Menu(dishes.ElementAt(r.Next(count)), dishes.ElementAt(r.Next(count)), dishes.ElementAt(r.Next(count))));
            }
            return menusNotChecked;
        }
    }
}
