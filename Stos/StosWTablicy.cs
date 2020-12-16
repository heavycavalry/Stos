using System;
using System.Collections;
using System.Collections.Generic;

namespace Stos
{
    public class StosWTablicy<T> : IStos<T>, IEnumerable<T>
    {

        private class EnumeratorStosu : IEnumerator<T>
        {
            private StosWTablicy<T> stos;
            private int position = -1;

            internal EnumeratorStosu(StosWTablicy<T> stos) => this.stos = stos;
            public T Current => stos.tab[position];
            object IEnumerator.Current => Current;
            public void Dispose() { }

            public bool MoveNext()
            {
                if (position < stos.Count - 1)
                {
                    position++;
                    return true;
                }
                else
                    return false;
            }

            public void Reset() => position = -1;
        }


        private T[] tab;
        private int szczyt = -1;

        public IEnumerator<T> GetEnumerator()
        {
            return new EnumeratorStosu(this);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new EnumeratorStosu(this);
        }

        public IEnumerable<T> TopToBottom
        {
            get
            { 
                for (int i = Count - 1; i >= 0; i--) yield return this[i];
            }
        }
        public IEnumerable<T> FromLastOne(int n)
        {
            int startIndex;
            if (n > Count)
            {
                startIndex = 0;
            }
            else
            {
                startIndex = Count - n;
            }

            for (int i = Count - 1; i >= startIndex; i--) yield return this[i];
        }

        public System.Collections.ObjectModel.ReadOnlyCollection<T> ToArrayReadOnly()
        {
            return Array.AsReadOnly(tab);
        }

        public StosWTablicy(int size = 10)
        {
            tab = new T[size];
            szczyt = -1;
        }

        public T Peek => IsEmpty ? throw new StosEmptyException() : tab[szczyt];

        public int Count => szczyt + 1;

        public bool IsEmpty => szczyt == -1;

        public void Clear() => szczyt = -1;

        public T Pop()
        {
            if (IsEmpty)
                throw new StosEmptyException();

            szczyt--;
            return tab[szczyt + 1];
        }

        public void Push(T value)
        {
            if (szczyt == tab.Length - 1)
            {
                Array.Resize(ref tab, tab.Length * 2); //2krotne powiekszenie tablicy
            }

            szczyt++;
            tab[szczyt] = value;
        }

        public void TrimExcess()
        {

            int newSize = Convert.ToInt32(Math.Ceiling(Count * (10.0 / 9.0)));
            Array.Resize(ref tab, newSize);
        }

        public T[] ToArray()
        {
            //return tab;  //bardzo źle - reguły hermetyzacji

            //poprawnie:
            T[] temp = new T[szczyt + 1];
            for (int i = 0; i < temp.Length; i++)
                temp[i] = tab[i];
            return temp;
        }

     

        public T this[int index]
        {
            get
            {
                if (index > Count - 1)
                    throw new IndexOutOfRangeException();
                return tab[index];
            }
        }
    }
}
