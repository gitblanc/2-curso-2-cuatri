using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPP.Laboratory.Exam
{
    public class List<T> : IEnumerable<T>
    {
        internal Node<T> head;
        public int NumberOfElements { get; protected set; }
        //TODO: Implement a GetCount method for calculating the value in every get call

        public List()
        {
            //Initializes the list 'head'
            head = null;
            NumberOfElements = 0;
        }

        public void Mix(IEnumerable<T> v)
        {
            this.Mix(v, x => x);
        }

        public void Mix<K>(IEnumerable<K> v, Func<K, T> selector)
        {
            Random r = new Random();
            foreach(var e in v)
            {
                Node<T> start = this.head;
                Node<T> prev = null;
                int position = r.Next(this.NumberOfElements + 1);
                while(position > 0)
                {
                    prev = start;
                    start = start.Next;
                    position -= 1;
                }
                Node<T> newElement = new Node<T>(selector(e));
                if (prev == null)
                    this.head = newElement;
                else
                    prev.Next = newElement;

                newElement.Next = start;
            }
        }

        public List(IEnumerable<T> list)
        {
            head = null;
            NumberOfElements = 0;
            foreach (var element in list)
                this.Add(element);
        }

        public virtual void Add(T newElement)
        {
            if (head == null)
                head = new Node<T>(newElement);
            else
            {
                Node<T> current = head;
                while (current.Next != null)
                    current = current.Next;
                current.Next = new Node<T>(newElement);
            }
            NumberOfElements++;
        }

        public virtual void Add(T newElement, int position)
        {
            //Do nothing
        }

        public bool Remove(T element)
        {
            Node<T> current = head;
            Node<T> previous = null;
            while(!current.Info.Equals(element))
            {
                previous = current;
                current = current.Next;
                if (current == null)
                    return false;
            }
            if(current == head)
            {
                head = head.Next;
                NumberOfElements--;
                return true;
            }
            previous.Next = current.Next;
            NumberOfElements--;
            return true;
                
        }

        private void RemoveAt(int position)
        {
            if (position > this.NumberOfElements)
                throw new ArgumentOutOfRangeException("There is no element at position " + position);
            else
            {
                Node<T> currentPos = this.head;
                Node<T> previousPos = null;
                for (int i = 0; i < position; i++)
                {
                    previousPos = currentPos;
                    currentPos = currentPos.Next;
                }
                if(previousPos != null)
                    previousPos.Next = currentPos.Next;
                NumberOfElements--;
                //currentPos node could be garbage collected
            }
        }
        public bool Contains(T element)
        {
            if (this.head == null)
                return false;

            bool contained = false;
            Node<T> current = this.head;
            do
            {
                if (current.Info.Equals(element))
                    return true;
                current = current.Next;
            } while (current != null);
            return contained;
        }

        public void DeleteIfFollowing(Func<T, T, bool> p)
        {
            if (this.head == null)
                return;
            Node<T> current = this.head;
            if (current.Next == null)
                return;
            while(current.Next != null)
            {
                bool res = p(current.Info, current.Next.Info);
                if (res)
                    current.Next = current.Next.Next;
                else
                    current = current.Next;
            }
        }

        public virtual T GetElement(int position)
        {
            if (position > this.NumberOfElements)
                throw new ArgumentOutOfRangeException("There is no element at position " + position);
            else
            {
                Node<T> currentPos = this.head;
                Node<T> previousPos = null;
                for (int i = 0; i < position; i++)
                {
                    previousPos = currentPos;
                    currentPos = currentPos.Next;
                }
                //if (previousPos != null)
                //    previousPos.Next = currentPos.Next;
                //else
                //    this.head = currentPos.Next;
                //NumberOfElements--;
                //currentPos node is returned and deleted from the list
                return currentPos.Info;
            }
        }

        public IEnumerable<TCodomain> Map<TCodomain>(Func<T, TCodomain> func)
        {
            foreach (T element in this)
                yield return func(element);
            yield break;
        }

        public T Find(Predicate<T> pred)
        {
            foreach (T element in this)
                if (pred(element))
                    return element;
            return default(T);
        }

        public IEnumerable<T> Filter(Predicate<T> pred)
        {
            foreach (T element in this)
                if (pred(element))
                    yield return element;
            yield break;
        }

        public TResult Reduce<TResult>(Func<TResult, T, TResult> function, TResult seed = default(TResult))
        {
            foreach (T element in this)
                seed = function(seed, element);
            return seed;
        }

        public void ForEach(Action<T> action)
        {
            foreach (T element in this)
                action(element);
        }

        public IEnumerable<T> Invert()
        {
            return System.Linq.Enumerable.Reverse(this);
        }


        public IEnumerator<T> GetEnumerator()
        {
            return new ListEnumerator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new ListEnumerator(this);
        }



        //Dividir
        /* first approach
        public Tuple<List<T>, List<T>> Dividir()
        {
            List<T> l1 = new List<T>();
            List<T> l2 = new List<T>();
            int half = this.NumberOfElements / 2;
            int count = 0;
            Node<T> current = this.head;
            while(count < this.NumberOfElements)
            {
                if (count < half)
                    l1.Add(current.Info);
                else
                    l2.Add(current.Info);
                current = current.Next;
                count++;
            }
            return new Tuple<List<T>, List<T>>(l1, l2);
        }
        */

        //Final implementation for the second version. It uses a closure
        public Tuple<List<T>, List<T>> Dividir()
        {
            int count = 0;
            return this.Dividir((data) => count++ < this.NumberOfElements / 2 ? true : false);
        }

        public Tuple<List<T>, List<T>> Dividir(Predicate<T> function)
        {
            List<T> l1 = new List<T>();
            List<T> l2 = new List<T>();
            Node<T> current = this.head;
            while (current.Next != null)
            {
                if (function(current.Info))
                    l1.Add(current.Info);
                else
                    l2.Add(current.Info);
                current = current.Next;
            }
            return new Tuple<List<T>, List<T>>(l1, l2);
        }



        //************************ Enumerator ***************************************//

        internal class ListEnumerator : IEnumerator<T>
        {
            List<T> list;
            Node<T> current;
            public ListEnumerator(List<T> list)
            {
                this.list = list;
                current = new Node<T>(default(T));
                current.Next = this.list.head;
            }

            public T Current
            {
                get 
                {
                    return this.current.Info;
                }
            }

            public void Dispose()
            {
                
            }

            object System.Collections.IEnumerator.Current
            {
                get 
                {
                    return this.current.Info;
                }
            }

            public bool MoveNext()
            {
                if (current.Next == null)
                    return false;
                else
                { 
                    current = current.Next;
                    return true;
                }
                
            }

            public void Reset()
            {
                current = new Node<T>(default(T));
                current.Next = this.list.head;
            }
        }
        
    }
}
