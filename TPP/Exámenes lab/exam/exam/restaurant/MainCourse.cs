using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPP.Exam.PI2.Restaurant
{
    public class MainCourse : Dish
    {
        protected SideDish side;
        public MainCourse(string name, string mainIng, SideDish s = null)
        {
            this.name = name;
            this.mainIngredient = mainIng;
            this.side = s;
        }
        public override string Name
        {
            get { return this.name; }
        }

        public SideDish SideDish { get { return this.side; } }
        public override string AlsoContains
        {
            get {
                if (this.side == null) return String.Empty;
                return this.side.MainIngredient; }
        }
    }
}
