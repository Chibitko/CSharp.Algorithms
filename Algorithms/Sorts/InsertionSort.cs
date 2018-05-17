using System;
using System.Collections.Generic;

namespace Algorithms.Sorts
{
    public class InsertionSort<T> : SortBase<T>
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
            for (int i = 1, n = items.Count; i < n; i++)
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