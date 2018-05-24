using System.Collections;
using System.Collections.Generic;

namespace Algorithms.Data.Heaps
{
    public abstract class PriorityQueue<T> : IReadOnlyList<T>
    {
        public abstract int Count { get; }

        public abstract void Push(T item);

        public abstract T Pop();

        public abstract T Peak();

        public abstract IEnumerator<T> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public abstract T this[int index] { get; }
    }
}