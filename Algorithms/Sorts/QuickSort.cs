using System;
using System.Collections.Generic;

namespace Algorithms.Sorts
{
    public class QuickSort<T> : SortBase<T>
    {
        private readonly IComparer<T> m_comparer;
        private readonly Action<IList<T>> m_sort;
        private readonly Lazy<InsertionSort<T>> m_insertionSort;

        public QuickSort(QuickSortKind kind, IComparer<T> comparer)
        {
            m_comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
            m_insertionSort = new Lazy<InsertionSort<T>>(() => new InsertionSort<T>(m_comparer));
            switch (kind)
            {
                case QuickSortKind.NonRecursive:
                    m_sort = NonRecursive;
                    break;
                case QuickSortKind.Recursive:
                    m_sort = Recursive;
                    break;
                case QuickSortKind.ImprovedRecursive:
                    m_sort = ImprovedRecursive;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(kind));
            }
        }

        public QuickSort(QuickSortKind kind)
            : this(kind, Comparer<T>.Default)
        {
        }

        public QuickSort(IComparer<T> comparer)
            : this(QuickSortKind.NonRecursive, comparer)
        {
        }

        public QuickSort()
            : this(QuickSortKind.NonRecursive)
        {
        }

        public override void Sort(IList<T> items)
        {
            m_sort(items);
        }

        private int Partion(IList<T> items, int leftBound, int rightBound)
        {
            var left = leftBound;
            var right = rightBound;
            var pivot = items[(leftBound + rightBound) / 2];
            while (left <= right)
            {
                while (m_comparer.Compare(items[left], pivot) < 0)
                {
                    left++;
                }
                while (m_comparer.Compare(items[right], pivot) > 0)
                {
                    right--;
                }
                if (left >= right)
                {
                    break;
                }
                var tmp = items[left];
                items[left] = items[right];
                items[right] = tmp;
                left++;
                right--;
            }
            return right;
        }

        private void Recursive(IList<T> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }
            if (items.Count == 0)
            {
                return;
            }
            Recursive(items, 0, items.Count - 1);
        }

        private void Recursive(IList<T> items, int leftBound, int rightBound)
        {
            if (leftBound >= rightBound)
            {
                return;
            }
            var partion = Partion(items, leftBound, rightBound);
            Recursive(items, leftBound, partion);
            Recursive(items, partion + 1, rightBound);
        }

        private void NonRecursive(IList<T> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }
            if (items.Count == 0)
            {
                return;
            }
            var stack = new Stack<(int, int)>();
            stack.Push((0, items.Count - 1));
            while (stack.Count > 0)
            {
                var (l, r) = stack.Pop();
                if (l >= r)
                {
                    continue;
                }
                var p = Partion(items, l, r);
                if (p - l > r - p)
                {
                    stack.Push((l, p));
                    stack.Push((p + 1, r));
                }
                else
                {
                    stack.Push((p + 1, r));
                    stack.Push((l, p));
                }
            }
        }

        private void ImprovedRecursive(IList<T> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }
            if (items.Count == 0)
            {
                return;
            }
            ImprovedRecursive(items, 0, items.Count - 1);
        }

        private void ImprovedRecursive(IList<T> items, int leftBound, int rightBound)
        {
            if (rightBound - leftBound <= 10)
            {
                m_insertionSort.Value.Sort(items, leftBound, rightBound);
                return;
            }
            int mediane = items.IndexOfMediane(leftBound, (leftBound + rightBound) / 2, rightBound, m_comparer);
            items.Swap(mediane, (leftBound + rightBound) / 2);
            var partion = Partion(items, leftBound, rightBound);
            ImprovedRecursive(items, leftBound, partion);
            ImprovedRecursive(items, partion + 1, rightBound);
        }
    }
}