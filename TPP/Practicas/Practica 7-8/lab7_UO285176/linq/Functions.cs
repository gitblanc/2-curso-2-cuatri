using System.Linq;
using System.Collections.Generic;
using System;

namespace linq
{

    static public class Functions
    {

        public static IEnumerable<TResult> Map<TElement, TResult>(this IEnumerable<TElement> collection, Func<TElement, TResult> function)
        {
            foreach (TElement d in collection)
            {
                yield return function(d);
            }
        }

        public static void ForEach<TElement>(this IEnumerable<TElement> collection, Action<TElement> function)
        {
            foreach (TElement d in collection)
            {
                function(d);
            }
        }

        public static T Find<T>(this IEnumerable<T> collection, Predicate<T> p)
        {
            foreach (T elem in collection)
            {
                if (p(elem))
                    return elem;
            }
            return default;
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

        public static TCD Reduce<TD, TCD>(this IEnumerable<TD> collection, Func<TCD, TD, TCD> f, TCD defecto = default)
        {
            foreach (TD t in collection)
            {
                defecto = f(defecto, t);
            }
            return defecto;
        }

    }
}
