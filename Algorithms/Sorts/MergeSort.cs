using System;
using System.Collections.Generic;

namespace Algorithms.Sorts
{
    public sealed class MergeSort<T> : SortBase<T>
    {
        #region Static

        private static void Copy(IList<T> src, IList<T> dst)
        {
            for (int i = 0, n = dst.Count; i < n; i++)
            {
                dst[i] = src[i];
            }
        }

        #endregion

        private readonly IComparer<T> m_comparer;
        private readonly Action<IList<T>> m_sort;

        public MergeSort(MergeSortKind kind, IComparer<T> comparer)
        {
            m_comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
            switch (kind)
            {
                case MergeSortKind.NonRecursive:
                    m_sort = NonRecursive;
                    break;
                case MergeSortKind.CopyFirstRecursive:
                    m_sort = CopyFirstRecursive;
                    break;
                case MergeSortKind.Recursive:
                    m_sort = Recursive;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(kind));
            }
        }

        public MergeSort(MergeSortKind kind)
            : this(kind, Comparer<T>.Default)
        {
        }

        public MergeSort(IComparer<T> comparer)
            : this(MergeSortKind.NonRecursive, comparer)
        {
        }

        public MergeSort()
            : this(MergeSortKind.NonRecursive)
        {
        }

        public override void Sort(IList<T> data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            if (data.Count == 0)
            {
                return;
            }
            m_sort(data);
        }

        private void Merge(IList<T> leftBuffer, IList<T> rightBuffer, IList<T> targetBuffer,
            int leftBound, int middle, int rightBound)
        {
            int leftIndex = leftBound;
            int rightIndex = middle + 1;
            int targetIndex = leftIndex;
            while (leftIndex <= middle && rightIndex <= rightBound)
            {
                if (m_comparer.Compare(leftBuffer[leftIndex], rightBuffer[rightIndex]) <= 0)
                {
                    targetBuffer[targetIndex++] = leftBuffer[leftIndex++];
                }
                else
                {
                    targetBuffer[targetIndex++] = rightBuffer[rightIndex++];
                }
            }
            while (leftIndex <= middle)
            {
                targetBuffer[targetIndex++] = leftBuffer[leftIndex++];
            }
            while (rightIndex <= rightBound)
            {
                targetBuffer[targetIndex++] = rightBuffer[rightIndex++];
            }
        }

        private void Recursive(IList<T> data)
        {
            var temp = new T[data.Count];
            var buffer = Recursive(data, temp, 0, data.Count - 1);
            if (buffer.Equals(data))
            {
                return;
            }
            Copy(temp, data);
        }

        private IList<T> Recursive(IList<T> data, IList<T> temp, int leftBound, int rightBound)
        {
            if (leftBound == rightBound)
            {
                temp[leftBound] = data[leftBound];
                return temp;
            }
            var middle = (rightBound + leftBound) / 2;
            var leftBuffer = Recursive(data, temp, leftBound, middle);
            var rightBuffer = Recursive(data, temp, middle + 1, rightBound);
            var targetBuffer = leftBuffer.Equals(data) ? temp : data;
            Merge(leftBuffer, rightBuffer, targetBuffer, leftBound, middle, rightBound);
            return targetBuffer;
        }

        private void CopyFirstRecursive(IList<T> data)
        {
            var temp = new T[data.Count];
            data.CopyTo(temp, 0);
            CopyFirstRecursive(temp, data, 0, data.Count - 1);
        }

        private void CopyFirstRecursive(IList<T> src, IList<T> dst, int leftBound, int rightBound)
        {
            if (leftBound == rightBound)
            {
                return;
            }
            int middle = (rightBound + leftBound) / 2;
            CopyFirstRecursive(dst, src, leftBound, middle);
            CopyFirstRecursive(dst, src, middle + 1, rightBound);
            Merge(src, src, dst, leftBound, middle, rightBound);
        }

        private void NonRecursive(IList<T> data)
        {
            var temp = new T[data.Count];
            var buffer = NonRecursive(data, temp);
            if (buffer.Equals(data))
            {
                return;
            }
            Copy(temp, data);
        }

        private IList<T> NonRecursive(IList<T> data, IList<T> temp)
        {
            var src = temp;
            var dst = data;
            for (int width = 1, n = data.Count; width < n; width = 2 * width)
            {
                var tmp = dst;
                dst = src;
                src = tmp;
                for (int leftBound = 0; leftBound < n; leftBound = leftBound + 2 * width)
                {
                    var middle = Math.Min(leftBound + width, n) - 1;
                    var rightBound = Math.Min(leftBound + 2 * width, n) - 1;
                    Merge(src, src, dst, leftBound, middle, rightBound);
                }
            }
            return dst;
        }
    }
}