using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPP.Exam.PI2.Restaurant
{
    public class Dessert : Dish
    {
        public Dessert(string name, string mainIng)
        {
            this.name = name;
            this.mainIngredient = mainIng;
        }
    }
}
