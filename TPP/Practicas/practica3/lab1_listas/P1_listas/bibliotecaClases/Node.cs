//@UO285176
namespace BibliotecaClases
{
    public class Node
    {
        private int _value;
        private Node _next;

        public Node(int value, Node next) {
            _value = value;
            _next = next;
        }

        public Node(int value)
        {
            _value = value;
            _next = null;
        }

        public Node GetNext
        {
            get { return _next; }
            set { _next = value; }
        }

        public int GetValue
        {
            get { return _value; }
            set { _value = value; }
        }

        public override String ToString() {
            return "" + _value;
        }
    }
}