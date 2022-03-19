﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//@UO285176-implementación polimórfica
namespace Clases
{
    public class List<T> : IEnumerable<T>
    {
        internal class Node
        {
            private T _value;
            private Node _next;

            public Node(T value, Node next)
            {
                _value = value;
                _next = next;
            }

            public Node(T value)
            {
                _value = value;
                _next = null;
            }

            public Node GetNext
            {
                get { return _next; }
                set { _next = value; }
            }

            public T GetValue
            {
                get { return _value; }
                set { _value = value; }
            }

            public override string ToString()
            {
                return "" + _value;
            }
        }

        private Node _head;
        private int _length;

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public List()
        {
            _head = null;
            _length = 0;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new ListEnumerator<T>(_head, _length);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ListEnumerator<T>(_head, _length);
        }

        /// <summary>
        /// Método que busca un elemento en la lista
        /// </summary>
        public T GetElement(int value)
        {
            Node aux = _head;
            uint i = 0;
            while (aux != null)
            {
                if (i == value)
                {
                    return aux.GetValue;
                }
                aux = aux.GetNext;
                i++;
            }
            return default;
        }

        /// <summary>
        /// Método que devuelve un boolean en función de si el elemento buscado está o no en la lista
        /// </summary>
        public bool Contains(T element)
        {
            Node aux = _head;
            while (aux != null)
            {
                if (aux.GetValue.Equals(element))
                {
                    return true;
                }
                aux = aux.GetNext;
            }
            return false;
        }

        /// <summary>
        /// Muestra el tamaño de la lista
        /// </summary>
        public int Size()
        {
            return _length;
        }

        /// <summary>
        /// Método que devuelve si la lista está vacía
        /// </summary>
        public bool IsEmpty()
        {
            return Size() == 0;
        }

        /// <summary>
        /// Método que añade un nuevo nodop a la lista
        /// </summary>
        public void Add(T element)
        {
            Node newNode = new Node(element);
            Node actualNode = _head;

            if (IsEmpty())
            {
                _head = newNode;
            }
            else
            {
                while (actualNode.GetNext != null)
                {
                    actualNode = actualNode.GetNext;
                }
                actualNode.GetNext = new Node(element);

            }
            _length++;
        }

        /// <summary>
        /// Imprime todos los nodos de la lista
        /// </summary>
        public override string ToString()
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("[", _length);
            Node current = _head;
            while (current != null)
            {
                sb.Append(current.GetValue);
                sb.Append(" ");
                current = current.GetNext;
            }
            sb.Append("]");
            return sb.ToString();
        }

        /// <summary>
        /// Método que elimina un elemento de la lista
        /// 
        /// Nota: lo he comentado en exceso porque me parece un método bastante lioso
        /// </summary>
        public bool Remove(T element)
        {
            //Se hace una copia de la cabeza
            Node actualNode = _head;
            //Si la cabeza es un elemento
            if (actualNode != null)
            {
                //Si la cabeza es el elemento que queremos eliminar
                if (actualNode.GetValue.Equals(element))
                {
                    //Si el siguiente elemento no es null
                    if (actualNode.GetNext != null)
                    {
                        actualNode = actualNode.GetNext;
                    }
                    //Si el siguiente elemento fuese null
                    else
                    {
                        actualNode = null;
                    }
                    //La cabeza pasa a ser el nuevo nodo y la lista disminuye en tamaño
                    _head = actualNode;
                    _length--;
                    return true;
                }
                //Si la cabeza no es el elemento que queremos eliminar
                else
                {
                    while (actualNode.GetNext != null && !actualNode.GetNext.GetValue.Equals(element))
                    {
                        //Se toma el siguiente elemento
                        actualNode = actualNode.GetNext;
                    }
                    if (actualNode.GetNext != null && actualNode.GetNext.GetValue.Equals(element))
                    {
                        //Se copia el siguiente elemento al deseado en borrar
                        actualNode.GetNext = actualNode.GetNext.GetNext;
                        //Se borra
                        actualNode = null;
                        //Se disminuye el tamaño de la lista
                        _length--;
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("El elemento " + element.ToString() + " no se ha podido encontrar en la lista");
                        Console.WriteLine();
                        return false;
                    }
                }
            }
            else
            {
                Console.WriteLine("La lista está vacía");
                Console.WriteLine();
                return false;
            }
        }

    //    /// <summary>
    //    /// Método que a partir de una colección de elementos, nos devuelve el primero que cumpla un criterio dado,
    //    /// o su valor por defecto en caso de no existir.
    //    /// </summary>
    //    public static T Find<T>(this IEnumerable<T> collection, Predicate<T> p)
    //    {
    //        foreach (T elem in collection)
    //        {
    //            if (p(elem))
    //                return elem;
    //        }
    //        return default(T);
    //    }

    //    /// <summary>
    //    /// Método que a partir de una colección de elementos, nos devuelve todos aquellos que cumplan un criterio 
    //    /// dado (siendo éste parametrizable)
    //    /// </summary>
    //    public static IEnumerable<T> Filter<T>(this IEnumerable<T> collection, Predicate<T> p)
    //    {
    //        //Forma Eager
    //        //T[] result = new T[collection.Count()];
    //        //int i = 0;
    //        //foreach (T elem in collection)
    //        //{
    //        //    if (p(elem))
    //        //    {
    //        //        result[i] = elem;
    //        //        i++;
    //        //    }
    //        //}
    //        //Array.Resize(ref result, i);
    //        //if (result.Length == 0)
    //        //    return default(T[]);
    //        //return result;

    //        //Forma Lazy
    //        foreach (T elem in collection)
    //        {
    //            if (p(elem))
    //            {
    //                yield return elem;
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// Método que recibe una colección de elementos y una función que recibe un primer parámetro del tipo que queremos obtener 
    //    /// y un segundo parámetro del tipo de la colección. Su tipo devuelto es el propio del que queremos obtener.
    //    /// </summary>
    //    public static TCD Reduce<TD, TCD>(this IEnumerable<TD> collection, Func<TCD, TD, TCD> f, TCD defecto = default(TCD))
    //    {
    //        foreach (TD t in collection)
    //        {
    //            defecto = f(defecto, t);
    //        }
    //        return defecto;
    //    }

    //    /// <summary>
    //    /// Método que permite transformar los elementos de una lista y que devuelve una nueva lista con los elementos transformados.
    //    /// </summary>
    //    public static IEnumerable<TResult> Map<TElement, TResult>(this IEnumerable<TElement> collection, Func<TElement, TResult> function)
    //    {
    //        foreach (TElement d in collection)
    //        {
    //            yield return function(d);
    //        }
    //    }

    //    /// <summary>
    //    /// Método que modifica los elementos de una lista y no devuelve nada.
    //    /// </summary>
    //    public static void ForEach<TElement>(this IEnumerable<TElement> collection, Action<TElement> function)
    //    {
    //        foreach (TElement d in collection)
    //        {
    //            function(d);
    //        }
    //    }
    }
}
