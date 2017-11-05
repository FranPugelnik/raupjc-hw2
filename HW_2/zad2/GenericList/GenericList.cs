using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericList
{
    public class GenericList<X> : IGenericList<X>
    {
        private X[] _internalStorage;
        private int brojac;

        public GenericList()
        {
            _internalStorage = new X[4];
            brojac = 0;
        }

        public GenericList(int initialSize)
        {
            if (initialSize <= 0) Console.WriteLine("vrijednost ne moze biti nula.");
            _internalStorage = new X[initialSize];
            brojac = 0;

        }

        public void Add(X item)
        {
            if (brojac >= _internalStorage.Length)
            {
                X[] pomocni = new X[_internalStorage.Length * 2];
                Array.Copy(_internalStorage, pomocni, _internalStorage.Length);
                _internalStorage = pomocni;
            }
            _internalStorage[brojac] = item;
            brojac++;
        }

        public bool Remove(X item)
        {
            for (int i = 0; i < brojac; i++)
            {
                if (_internalStorage[i].Equals(item)) return RemoveAt(i);
            }
            return false;
        }

        public bool RemoveAt(int index)
        {

            if (index >= _internalStorage.Length || index < 0) throw new IndexOutOfRangeException();

            if (index > brojac) return false;

            for (int i = index; i < (_internalStorage.Length - 1); i++)
            {
                _internalStorage[i] = _internalStorage[i + 1];
            }
            _internalStorage[_internalStorage.Length - 1] = default(X);
            brojac--;
            return true;
        }

        public X GetElement(int index)
        {

            if (index < _internalStorage.Length && index >= 0)
            {
                return _internalStorage[index];
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }


        public int IndexOf(X item)
        {
            for (int i = 0; i < brojac; i++)
            {
                if (_internalStorage[i].Equals(item)) return i;
            }
            return -1;
        }


        public int Count { get => brojac; }

        public void Clear()
        {
            Array.Clear(_internalStorage, 0, _internalStorage.Length);
            brojac = 0;
        }

        public bool Contains(X item)
        {
            if (IndexOf(item) != -1)
            {
                return true;
            }
            return false;
        }

        public IEnumerator<X> GetEnumerator()
        {
            return new GenericListEnumerator<X>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }



    public class GenericListEnumerator<T> : IEnumerator<T>
    {
        private GenericList<T> _collection;
        private int currentIndex;
        private T currentBox;
        private GenericList<object> genericList;

        public GenericListEnumerator(GenericList<T> collection)
        {
            _collection = collection;
            currentIndex = -1;
            currentBox = default(T);
        }

        public bool MoveNext()
        {
            if (++currentIndex >= _collection.Count )  //????
            {
                return false;
            }
            else
            {
                currentBox = _collection.GetElement(currentIndex); //????
                return true;
            }
        }


        public void Reset()
        {
            currentIndex = -1;
        }

        void IDisposable.Dispose() { }

        public T Current
        {
            get { return currentBox; }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }


    }




    public interface IGenericList<X> : IEnumerable<X>
    {
        /// <summary >
        /// Adds an item to the collection .
        /// </ summary >
        void Add(X item);
        /// <summary >
        /// Removes the first occurrence of an item from the collection .
        /// If the item was not found , method does nothing and returns false .
        /// </ summary >
        bool Remove(X item);
        /// <summary >
        /// Removes the item at the given index in the collection .
        /// Throws IndexOutOfRange exception if index out of range .
        /// </ summary >
        bool RemoveAt(int index);
        /// <summary >
        /// Returns the item at the given index in the collection .
        /// Throws IndexOutOfRange exception if index out of range .
        /// </ summary >
        X GetElement(int index);
        /// <summary >
        /// Returns the index of the item in the collection .
        /// If item is not found in the collection , method returns -1.
        /// </ summary >
        int IndexOf(X item);
        /// <summary >
        /// Readonly property . Gets the number of items contained in the collection.
        /// </ summary >
        int Count { get; }
        /// <summary >
        /// Removes all items from the collection .
        /// </ summary >
        void Clear();
        /// <summary >
        /// Determines whether the collection contains a specific value .
        /// </ summary >
        bool Contains(X item);
    }
}
