using System;
using System.Collections.Generic;
using System.Linq;

namespace paradigma.funcional
{
    public static class Funciones
    {
        //predicate devuelve un booleano
        //action no devuelve nada



        public static T Find<T>(this IEnumerable<T> collection, Predicate<T> p)
        {
            foreach (T elem in collection)
            {
                if (p(elem))
                    return elem;
            }
            return default(T);
        }

        public static IEnumerable<T> Filter<T>(this IEnumerable<T> collection, Predicate<T> p)
        {
            T[] result = new T[collection.Count()];
            int i = 0;
            foreach (T elem in collection)
            {
                if (p(elem))
                {
                    result[i] = elem;
                    i++;
                }
            }
            Array.Resize(ref result, i);
            if (result.Length == 0)
                return default(T[]);
            return result;
        }

        public static TCD Reduce<TD, TCD>(this IEnumerable<TD> collection, Func<TCD, TD, TCD> f,TCD defecto = default(TCD))
        {
            foreach (TD t in collection)
            {
                defecto = f(defecto, t);
            }
            return defecto;
        }

    }
}
