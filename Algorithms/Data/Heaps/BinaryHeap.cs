using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Algorithms.Data.Exceptions;

namespace Algorithms.Data.Heaps
{
    public class BinaryHeap<T> : PriorityQueue<T>
    {
        protected readonly List<T> m_items;
        protected readonly IComparer<T> m_comparer;

        public BinaryHeap(IComparer<T> comparer = null)
        {
            m_items = new List<T>();
            m_comparer = comparer ?? Comparer<T>.Default;
        }

        public override int Count => m_items.Count;

        public override T this[int index] => m_items[index];

        public override IEnumerator<T> GetEnumerator()
        {
            return m_items.GetEnumerator();
        }

        public override void Push(T item)
        {
            m_items.Add(item);
            SiftUp(m_items, 0, Count - 1, m_comparer);
        }

        public override T Pop()
        {
            if (m_items.Count == 0)
            {
                throw new EmptyCollectionException();
            }
            var result = m_items[0];
            m_items[0] = m_items[m_items.Count - 1];
            m_items.RemoveAt(m_items.Count - 1);
            SiftDown(m_items, 0, Count - 1, m_comparer);
            return result;
        }

        public override T Peak()
        {
            if (m_items.Count == 0)
            {
                throw new EmptyCollectionException();
            }
            return m_items[0];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Parent(int i)
        {
            return (int) Math.Floor((double) (i - 1) / 2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int LeftChild(int i)
        {
            return 2 * i + 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int RightChild(int i)
        {
            return 2 * i + 2;
        }

        /// <summary>
        ///     Repair the heap whose root element is at index <see cref="start" />,
        ///     assuming the heaps rooted at its children are valid.
        /// </summary>
        /// <param name="a">List of elements.</param>
        /// <param name="start">Root element index.</param>
        /// <param name="end">Last element index.</param>
        /// <param name="comparer">Element comparer.</param>
        public static void SiftDown(IList<T> a, int start, int end, IComparer<T> comparer)
        {
            var root = start;
            while (true)
            {
                var leftChild = LeftChild(root); // Left child of root.
                if (leftChild > end)
                {
                    // Left child is out of range.
                    return;
                }
                var min = root; // Keeps track of child to swap with.
                if (comparer.Compare(a[min], a[leftChild]) < 0)
                {
                    min = leftChild;
                }
                var rightChild = leftChild + 1; // Right child of root.
                if (rightChild <= end && comparer.Compare(a[min], a[rightChild]) < 0)
                {
                    // If there is a right child and that child is greater.
                    min = rightChild;
                }
                if (min == root)
                {
                    // The root holds the largest element. Since we assume the heaps rooted at the
                    // children are valid, this means that we are done.
                    return;
                }
                a.Swap(root, min);
                root = min;
                // Repeat to continue sifting down the child now.
            }
        }

        /// <summary>
        ///     Repair the heap whose root element is at index <see cref="start" />,
        ///     assuming the new element at index <see cref="end" />.
        /// </summary>
        /// <param name="a">List of elements.</param>
        /// <param name="start">Root element index.</param>
        /// <param name="end">New element index.</param>
        /// <param name="comparer">Element comparer.</param>
        public static void SiftUp(IList<T> a, int start, int end, IComparer<T> comparer)
        {
            var child = end;
            while (child > start)
            {
                var parent = Parent(child);
                if (parent < start || comparer.Compare(a[parent], a[child]) >= 0)
                {
                    // Parent and child are in the right order.
                    return;
                }
                a.Swap(parent, child);
                child = parent;
                // Repeat to continue sifting up the parent now.
            }
        }

        /// <summary>
        ///     Put elements of <see cref="a" /> in heap order, in-place.
        ///     Has O(n) time complexity.
        /// </summary>
        /// <param name="a">List of elements.</param>
        /// <param name="comparer">Element comparer.</param>
        public static void HeapifyDown(IList<T> a, IComparer<T> comparer)
        {
            var count = a.Count;
            // 'start' is assigned the index in 'a' of the last parent node.
            // The last element in a 0-based array is at index count-1; find the parent of that element.
            var start = Parent(count - 1);
            while (start >= 0)
            {
                // Sift down the node at index 'start' to the proper place such that all nodes below
                // the start index are in heap order.
                SiftDown(a, start, count - 1, comparer);
                // Go to the next parent node.
                start--;
            }
            // After sifting down the root all nodes / elements are in heap order.
        }

        /// <summary>
        ///     Put elements of <see cref="a" /> in heap order, in-place.
        ///     Has O(n log n) time complexity.
        /// </summary>
        /// <param name="a">List of elements.</param>
        /// <param name="comparer">Element comparer.</param>
        public static void HeapifyUp(IList<T> a, IComparer<T> comparer)
        {
            var count = a.Count;
            // 'end' is assigned the index of the first (left) child of the root.
            var end = 1;
            while (end < count)
            {
                // Sift up the node at index end to the proper place such that all nodes above
                // the end index are in heap order.
                SiftUp(a, 0, end, comparer);
                end++;
            }
            // After sifting up the last node all nodes are in heap order.
        }
    }
}