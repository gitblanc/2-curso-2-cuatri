using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPP.Exam.PI2.Restaurant
{
    public class Soup : Dish
    {
        public Soup(string mainIng)
        {
            this.name = String.Format("{0} soup", mainIng);
            this.mainIngredient = mainIng;
        }

        public override string Name
        {
            get { return this.name; }
        }

        public override string AlsoContains
        {
            get { return "Water"; }
        }
    }
}
