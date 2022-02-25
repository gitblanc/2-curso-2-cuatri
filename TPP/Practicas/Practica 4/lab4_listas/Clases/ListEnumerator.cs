using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    internal class ListEnumerator<T> : IEnumerator<T>
    {
        int index;//la posición del término
        int maxLength;//longitud máxima del enumerador
        List<T>.Node current;//nodo actual
        List<T>.Node nextNode;//siguiente nodo
        List<T>.Node _head;//la cabeza original

        public ListEnumerator(List<T>.Node head, int length)
        {
            _head = head;
            maxLength = length;
            Reset();
        }

        T IEnumerator<T>.Current
        {
            get { return current.GetValue; }
        }

        object IEnumerator.Current
        {
            get { return current.GetValue; }
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (index >= maxLength)
                return false;
            if (index == 0)
            {
                index++;
                return true;
            }
            if (nextNode.GetNext != null)
            {
                List<T>.Node temp = nextNode;
                nextNode = nextNode.GetNext;
                current = temp;
                index++;
                return true;
            }
            current = nextNode;
            nextNode = null;
            index++;
            return true;
        }

        public void Reset()
        {
            index = 0;
            current = _head;
            nextNode = _head.GetNext;
        }
    }
}
