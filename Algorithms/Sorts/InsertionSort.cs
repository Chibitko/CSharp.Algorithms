using System;
using System.Collections.Generic;

namespace Algorithms.Sorts
{
    public sealed class InsertionSort<T> : SortBase<T>
    {
        private readonly IComparer<T> m_comparer;

        public InsertionSort(IComparer<T> comparer)
        {
            m_comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }

        public InsertionSort()
            : this(Comparer<T>.Default)
        {
        }

        public override void Sort(IList<T> items)
        {
            Sort(items, 0, items.Count - 1);
        }

        public void Sort(IList<T> items, int leftBound, int rightBound)
        {
            for (int i = leftBound + 1; i <= rightBound; i++)
            {
                var x = items[i];
                var j = i;
                while (j > 0 && m_comparer.Compare(items[j - 1], x) > 0)
                {
                    items[j] = items[j - 1];
                    j--;
                }
                items[j] = x;
            }
        }
    }
}