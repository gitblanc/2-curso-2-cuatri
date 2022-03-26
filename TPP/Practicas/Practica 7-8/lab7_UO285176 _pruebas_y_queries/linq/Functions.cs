using System.Linq;
using System.Collections.Generic;
using System;

namespace linq
{

    static public class Functions
    {
        ///// <summary>
        ///// Método que permite transformar los elementos de una lista y que devuelve una nueva lista con los elementos transformados.
        ///// </summary>
        public static IEnumerable<TResult> Map<TElement, TResult>(this IEnumerable<TElement> collection, Func<TElement, TResult> function)
        {
            foreach (TElement d in collection)
            {
                yield return function(d);
            }
        }

        ///// <summary>
        ///// Método que modifica los elementos de una lista y no devuelve nada.
        ///// </summary>
        public static void ForEach<TElement>(this IEnumerable<TElement> collection, Action<TElement> function)
        {
            foreach (TElement d in collection)
            {
                function(d);
            }
        }

        /// <summary>
        /// Método que a partir de una colección de elementos, nos devuelve el primero que cumpla un criterio dado,
        /// o su valor por defecto en caso de no existir.
        /// </summary>
        public static T Find<T>(this IEnumerable<T> collection, Predicate<T> p)
        {
            foreach (T elem in collection)
            {
                if (p(elem))
                    return elem;
            }
            return default;
        }

        ///// <summary>
        ///// Método que a partir de una colección de elementos, nos devuelve todos aquellos que cumplan un criterio 
        ///// dado (siendo éste parametrizable)
        ///// </summary>
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> collection, Predicate<T> p)
        {
            //---------FORMA EAGER--------
            //T[] result = new T[collection.Count()];
            //int i = 0;
            //foreach (T elem in collection)
            //{
            //    if (p(elem))
            //    {
            //        result[i] = elem;
            //        i++;
            //    }
            //}
            //Array.Resize(ref result, i);
            //if (result.Length == 0)
            //    return default(T[]);
            //return result;

            foreach (T elem in collection)
            {
                if (p(elem))
                {
                    yield return elem;
                }
            }
        }

        ///// <summary>
        ///// Método que recibe una colección de elementos y una función que recibe un primer parámetro del tipo que queremos obtener 
        ///// y un segundo parámetro del tipo de la colección. Su tipo devuelto es el propio del que queremos obtener.
        ///// </summary>
        public static TCD Reduce<TD, TCD>(this IEnumerable<TD> collection, Func<TCD, TD, TCD> f, TCD defecto = default)
        {
            foreach (TD t in collection)
            {
                defecto = f(defecto, t);
            }
            return defecto;
        }

        ///// <summary>
        ///// Ofrece el recorrido de izquierda a derecha de un IEnumerable
        ///// </summary>
        ///// <returns></returns>
        public static IEnumerable<TD> Invert<TD>(this IEnumerable<TD> collection)
        {
            for (int i = collection.Count() - 1; i >= 0; i--)
            {
                yield return collection.ElementAt(i);
            }
        }

        //EJS PARA EXAMEN-----
        public static IEnumerable<TD> RightPosition<TD>(this IEnumerable<TD> collection, Predicate<TD> f)
        {
            foreach (TD t in collection)
            {
                if (f(t))
                    yield return t;
            }
        }

        public static string puta(this string str, Predicate<string> f)
        {
            return "";
        }

    }
}
