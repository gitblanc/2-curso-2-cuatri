using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPP.Exam.PI2.Restaurant
{
    public class SideDish : Dish
    {
        public SideDish(string name, string mainIng)
        {
            this.name = name;
            this.mainIngredient = mainIng;
        }

        public override string AlsoContains { get { return string.Empty; } }
    }
}
