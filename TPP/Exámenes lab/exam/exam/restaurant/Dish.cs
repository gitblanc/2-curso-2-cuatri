using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPP.Exam.PI2.Restaurant
{
    public abstract class Dish
    {
        protected string name;
        protected string mainIngredient;

        public virtual String Name { get { return this.name; } }
        public String MainIngredient { get { return this.mainIngredient; } }
        public virtual String AlsoContains { get { return string.Empty; } }

        public override string ToString()
        {
            if(!AlsoContains.Equals(string.Empty))
                return string.Format("{0} - Ingredients: {1} and {2}", Name, MainIngredient, AlsoContains);
            return string.Format("{0} - Ingredients: only {1}", Name, MainIngredient);
        }
    }
}
