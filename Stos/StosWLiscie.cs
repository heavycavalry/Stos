using System;
using System.Collections;
using System.Dynamic;
using System.Xml;

namespace Stos
{
    public class StosWLiscie<T> : IStos<T>
    {

        private class Node
        {
            public T value;
            public Node next;
            public Node(T value, Node next)
            {
                this.value = value;
                this.next = next;
            }
        }
        private Node szczyt = null;

        public T Peek => IsEmpty ? throw new StosEmptyException() : szczyt.value;

        public int Count { get; private set; } = 0;

        public bool IsEmpty => Count == 0;

        public void Clear()
        {
            Count = 0;
            szczyt = null;
        }

        public T Pop()
        {

            if (IsEmpty)
                throw new StosEmptyException();
            Count -= 1;
            var result = szczyt.value;

            szczyt = szczyt.next;

            return result;

        }

        public void Push(T value)
        {
            Count += 1;
            szczyt = new Node(value, szczyt);
        }

        public T[] ToArray()
        {
            var current = szczyt;
            T[] temp = new T[Count];
            for (int i = temp.Length - 1; i >= 0; i--)
            {
                temp[i] = current.value;
                current = current.next;
            }
            return temp;
        }
    }
}