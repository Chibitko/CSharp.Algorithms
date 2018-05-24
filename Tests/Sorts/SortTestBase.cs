using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Algorithms.Sorts;
using Xunit;

namespace Tests.Sorts
{
    public abstract class SortTestBase
    {
        #region Internal types

        /// <inheritdoc />
        /// <summary>Value object wrapper.</summary>
        /// <typeparam name="T">The type of value objects.</typeparam>
        [DebuggerDisplay("{Value} ({Order})")]
        protected sealed class Item<T> : IComparable<Item<T>>
        {
            private readonly IComparer<T> m_comparer;

            /// <summary>Value object.</summary>
            public T Value { get; set; }

            /// <summary>Initial position in a collection.</summary>
            public int Order { get; set; }

            /// <summary>Initializes a new instance of the wrapper.</summary>
            /// <param name="value">Value object.</param>
            /// <param name="order">Initial position in a collection.</param>
            /// <param name="comparer">Value object comparer.</param>
            public Item(T value, int order, IComparer<T> comparer)
            {
                m_comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
                Value = value;
                Order = order;
            }

            /// <inheritdoc />
            /// <summary>
            ///     Initializes a new instance of the wrapper using the default value object comparer (<see cref="Comparer{T}" />
            ///     ).
            /// </summary>
            public Item(T value, int order)
                : this(value, order, Comparer<T>.Default)
            {
            }

            public int CompareTo(Item<T> other)
            {
                if (ReferenceEquals(this, other))
                {
                    return 0;
                }
                if (ReferenceEquals(null, other))
                {
                    return 1;
                }
                return m_comparer.Compare(Value, other.Value);
            }
        }

        /// <inheritdoc />
        /// <summary>Comparing the value objects.</summary>
        /// <typeparam name="T">The type of value objects.</typeparam>
        protected sealed class ValueComparer<T> : IComparer<Item<T>>
        {
            public int Compare(Item<T> x, Item<T> y)
            {
                if (ReferenceEquals(x, y))
                {
                    return 0;
                }
                if (ReferenceEquals(null, x))
                {
                    return -1;
                }
                if (ReferenceEquals(null, y))
                {
                    return 1;
                }
                return x.CompareTo(y);
            }
        }

        /// <inheritdoc />
        /// <summary>Comparing the value objects and its orders.</summary>
        /// <typeparam name="T">The type of value objects.</typeparam>
        protected sealed class ValueAndOrderComparer<T> : IComparer<Item<T>>
        {
            public int Compare(Item<T> x, Item<T> y)
            {
                if (ReferenceEquals(x, y))
                {
                    return 0;
                }
                if (ReferenceEquals(null, x))
                {
                    return -1;
                }
                if (ReferenceEquals(null, y))
                {
                    return 1;
                }
                int result = x.CompareTo(y);
                if (result != 0)
                {
                    return result;
                }
                return x.Order.CompareTo(y.Order);
            }
        }

        #endregion

        /// <summary>
        ///     Pseudo-random number generator.
        /// </summary>
        protected static readonly Random Randomizer = new Random();

        /// <summary>Checks if a collection of <see cref="Item{T}" /> is sorted.</summary>
        /// <typeparam name="T">The type of value objects.</typeparam>
        /// <param name="items">Items after sorting.</param>
        /// <param name="comparer"><see cref="Item{T}" /> comparer.</param>
        /// <returns>true if items are sorted; otherwise, false</returns>
        protected bool IsSorted<T>(IList<Item<T>> items, IComparer<Item<T>> comparer) where T : IComparable<T>
        {
            for (int i = 0, l = items.Count - 1; i < l; i++)
            {
                if (comparer.Compare(items[i], items[i + 1]) > 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>Checks if a collection is sorted using <see cref="ValueComparer{T}" />.</summary>
        /// <typeparam name="T">The type of value objects.</typeparam>
        /// <param name="items">Items after sorting.</param>
        /// <returns>true if items are sorted; otherwise, false</returns>
        protected bool IsSorted<T>(IList<Item<T>> items) where T : IComparable<T>
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }
            return IsSorted(items, new ValueComparer<T>());
        }

        /// <summary>Checks if a collection is sorted using <see cref="ValueAndOrderComparer{T}" />.</summary>
        /// <typeparam name="T">The type of value objects.</typeparam>
        /// <param name="items">Items after sorting.</param>
        /// <returns>true if items are sorted; otherwise, false</returns>
        protected bool IsStableSorted<T>(IList<Item<T>> items) where T : IComparable<T>
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }
            return IsSorted(items, new ValueAndOrderComparer<T>());
        }

        /// <summary>Checks collection.</summary>
        /// <param name="sort">Sort algorithm.</param>
        /// <param name="maxLength">Max length of test collections.</param>
        /// <param name="stable">Check if sorting is stable.</param>
        /// <param name="value">Value generator.</param>
        private void SortTest(SortBase<Item<int>> sort, int maxLength, bool stable, Func<int, int> value)
        {
            for (var length = 10; length < maxLength; length++)
            {
                var data = Enumerable.Range(0, length)
                    .Select(i => new Item<int>(value(i), i))
                    .ToArray();
                sort.Sort(data);
                if (stable)
                {
                    Assert.True(IsStableSorted(data));
                }
                else
                {
                    Assert.True(IsSorted(data));
                }
            }
        }

        /// <summary>Main test method.</summary>
        /// <param name="sort">Sort algorithm.</param>
        /// <param name="maxLength">Max length of test collections.</param>
        /// <param name="stable">Check if sorting is stable.</param>
        protected void SortTest(SortBase<Item<int>> sort, int maxLength, bool stable)
        {
            // Random numbers sorting.
            SortTest(sort, maxLength, stable, i => Randomizer.Next());
            // Numbers in ascending order sorting.
            SortTest(sort, maxLength, stable, i => i);
            // Numbers in descending order sorting.
            SortTest(sort, maxLength, stable, i => maxLength - i);
            // Identical numbers sorting.
            if (stable)
            {
                SortTest(sort, maxLength, true, i => 1000);
            }
        }

        /// <summary>Self testing.</summary>
        [Fact]
        public void SortedAndStable()
        {
            var data = new List<Item<int>>
            {
                new Item<int>(3, 1),
                new Item<int>(7, 2),
                new Item<int>(7, 3),
                new Item<int>(9, 4),
                new Item<int>(15, 5)
            };
            Assert.True(IsSorted(data));
            Assert.True(IsStableSorted(data));
            data = new List<Item<int>>
            {
                new Item<int>(3, 1),
                new Item<int>(7, 3),
                new Item<int>(7, 2),
                new Item<int>(9, 4),
                new Item<int>(15, 5)
            };
            Assert.True(IsSorted(data));
            Assert.False(IsStableSorted(data));
            data = new List<Item<int>>
            {
                new Item<int>(9, 1),
                new Item<int>(7, 3),
                new Item<int>(7, 3),
                new Item<int>(9, 4),
                new Item<int>(15, 5)
            };
            Assert.False(IsSorted(data));
        }
    }
}