using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPP.Laboratory.Exam
{
    public static class Functions
    {
        public static IEnumerable<TCodomain> Map<TDomain, TCodomain>(this IEnumerable<TDomain> collection, Func<TDomain, TCodomain> func)
        {
            foreach (TDomain element in collection)
                yield return func(element);
            yield break;
        }

        public static T Find<T>(this IEnumerable<T> collection, Predicate<T> pred)
        {
            foreach (T element in collection)
                if (pred(element))
                    return element;
            throw new InvalidOperationException("Element not found");
        }

        public static IEnumerable<T> Filter<T>(this IEnumerable<T> collection, Predicate<T> pred)
        {
            foreach(T element in collection)
                if (pred(element))
                    yield return element;
            yield break;
        }

        public static TResult Reduce<T, TResult>(this IEnumerable<T> collection, Func<TResult, T, TResult> function, TResult seed = default(TResult))
        {
            foreach(T element in collection)
                seed = function(seed, element);
            return seed;
        }

        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (T element in collection)
                action(element);
        }
    }
}
