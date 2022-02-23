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
        T actual;//actual term
        int maxLength;//longitud máxima del enumerador

        public ListEnumerator(int length)
        {
            maxLength = length;
            Reset();
        }

        T IEnumerator<T>.Current
        {
            get { return actual; }
        }

        object IEnumerator.Current
        {
            get { return actual; }
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            index++;
            return (index < maxLength);
        }

        public void Reset()
        {
            index = 0;
        }
    }
}
