using System.Linq;
using System.Collections.Generic;
using System;

namespace TPP.Laboratory.Functional.Lab07 {

    static public class Functions {

        public static IEnumerable<TResult> Map<TElement, TResult>(this IEnumerable<TElement> collection, Func<TElement, TResult> function) {
            foreach (TElement d in collection) {
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

    }
}
