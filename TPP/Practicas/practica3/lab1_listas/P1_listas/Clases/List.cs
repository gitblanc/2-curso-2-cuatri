using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//@UO285176-implementación polimórfica
namespace Clases
{
    public class List
    {
        internal class Node
        {
            private Object _value;
            private Node _next;

            public Node(Object value, Node next)
            {
                _value = value;
                _next = next;
            }

            public Node(Object value)
            {
                _value = value;
                _next = null;
            }

            public Node GetNext
            {
                get { return _next; }
                set { _next = value; }
            }

            public Object GetValue
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

        /// <summary>
        /// Método que busca un elemento en la lista
        /// </summary>
        public Object GetElement(int value)
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
            return null;
        }

        /// <summary>
        /// Método que devuelve un boolean en función de si el elemento buscado está o no en la lista
        /// </summary>
        public bool Contains(Object element)
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
        public void Add(Object element)
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
        public bool Remove(Object element)
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
                    if (actualNode.GetNext != null && actualNode.GetNext.GetValue == element)
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
    }
}
