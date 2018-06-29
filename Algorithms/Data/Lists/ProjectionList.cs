using System;
using System.Collections;
using System.Collections.Generic;

namespace Algorithms.Data.Lists
{
    internal sealed class ProjectionList<T> : IList<T>
    {
        private readonly IList<T> m_items;
        private readonly int m_start;
        private readonly int m_count;

        public ProjectionList(IList<T> items, int start, int count)
        {
            m_items = items;
            m_start = start;
            m_count = count;
        }

        public int Count => m_count;

        public T this[int index]
        {
            get
            {
                if (index >= m_count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }
                return m_items[m_start + index];
            }
            set
            {
                if (index >= m_count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }
                m_items[m_start + index] = value;
            }
        }

        public bool IsReadOnly => false;

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < m_count; i++)
            {
                yield return m_items[m_start + i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public int IndexOf(T item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }
    }
}