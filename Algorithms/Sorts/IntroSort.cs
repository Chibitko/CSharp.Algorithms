using System;
using System.Collections.Generic;
using Algorithms.Data.Lists;

namespace Algorithms.Sorts
{
    public sealed class IntroSort<T> : QuickSort<T>
    {
        private readonly int? m_depthLimit;
        private readonly Lazy<HeapSort<T>> m_heapSort;

        public IntroSort(IComparer<T> comparer, int? depthLimit = null)
            : base(comparer)
        {
            m_depthLimit = depthLimit;
            m_heapSort = new Lazy<HeapSort<T>>(() => new HeapSort<T>(m_comparer));
        }

        public IntroSort(int? depthLimit = null)
            : this(Comparer<T>.Default, depthLimit)
        {
        }

        private HeapSort<T> HeapSort => m_heapSort.Value;

        public bool HeadSortUsed { get; set; }

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
            HeadSortUsed = false;
            Sort(items, 0, items.Count - 1, m_depthLimit ?? (int) (2.0 * Math.Log(items.Count, 2.0)));
        }

        private void Sort(IList<T> items, int leftBound, int rightBound, int depthLimit)
        {
            while (true)
            {
                if (rightBound - leftBound <= 10)
                {
                    InsertionSort.Sort(items, leftBound, rightBound);
                }
                else if (depthLimit == 0)
                {
                    HeadSortUsed = true;
                    HeapSort.Sort(new ProjectionList<T>(items, leftBound, rightBound - leftBound + 1));
                }
                else
                {
                    int mediane = items.IndexOfMediane(leftBound, (leftBound + rightBound) / 2, rightBound, m_comparer);
                    items.Swap(mediane, (leftBound + rightBound) / 2);
                    var partion = Partion(items, leftBound, rightBound);
                    Sort(items, leftBound, partion, depthLimit - 1);
                    leftBound = partion + 1;
                    depthLimit = depthLimit - 1;
                    continue;
                }
                break;
            }
        }
    }
}