using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPP.Laboratory.Exam
{
    class Node<T>
    {
        public Node<T> Next { get; set; }
        public T Info { get; set; }

        internal Node(T info)
        {
            Next = null;
            Info = info;
        }
    }
}
