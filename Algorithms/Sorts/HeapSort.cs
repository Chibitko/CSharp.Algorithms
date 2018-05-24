using System;
using System.Collections.Generic;
using Algorithms.Data.Heaps;

namespace Algorithms.Sorts
{
    public class HeapSort<T> : SortBase<T>
    {
        private readonly IComparer<T> m_comparer;
        private readonly Action<IList<T>> m_sort;

        public HeapSort(HeapSortKind kind, IComparer<T> comparer)
        {
            m_comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
            switch (kind)
            {
                case HeapSortKind.HeapifyDown:
                    m_sort = HeapifyDown;
                    break;
                case HeapSortKind.HeapifyUp:
                    m_sort = HeapifyUp;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(kind));
            }
        }

        public HeapSort(HeapSortKind kind)
            : this(kind, Comparer<T>.Default)
        {
        }

        public HeapSort(IComparer<T> comparer)
            : this(HeapSortKind.HeapifyDown, comparer)
        {
        }

        public HeapSort()
            : this(HeapSortKind.HeapifyDown)
        {
        }

        public override void Sort(IList<T> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }
            if (items.Count == 0)
            {
                return;
            }
            m_sort(items);
        }

        private void HeapifyDown(IList<T> items)
        {
            // Build the heap in list so that largest value is at the root.
            BinaryHeap<T>.HeapifyDown(items, m_comparer);
            // The following loop maintains the invariants that a[0:end] is a heap and every element
            // beyond end is greater than everything before it(so a[end: count] is in sorted order.
            var end = items.Count - 1;
            while (end > 0)
            {
                // a[0] is the root and largest value.The swap moves it in front of the sorted elements.
                items.Swap(end, 0);
                // The heap size is reduced by one.
                end--;
                // Tthe swap ruined the heap property, so restore it.
                BinaryHeap<T>.SiftDown(items, 0, end, m_comparer);
            }
        }

        private void HeapifyUp(IList<T> items)
        {
            // Build the heap in list so that largest value is at the root.
            BinaryHeap<T>.HeapifyUp(items, m_comparer);
            // The following loop maintains the invariants that a[0:end] is a heap and every element
            // beyond end is greater than everything before it(so a[end: count] is in sorted order.
            var end = items.Count - 1;
            while (end > 0)
            {
                // a[0] is the root and largest value.The swap moves it in front of the sorted elements.
                items.Swap(end, 0);
                // The heap size is reduced by one.
                end--;
                // Tthe swap ruined the heap property, so restore it.
                BinaryHeap<T>.SiftDown(items, 0, end, m_comparer);
            }
        }
    }
}