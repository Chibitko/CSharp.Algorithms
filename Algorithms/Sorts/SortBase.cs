using System.Collections.Generic;

namespace Algorithms.Sorts
{
    /// <summary>
    ///     Base class for sorting algorithms.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of elements to sort.
    /// </typeparam>
    public abstract class SortBase<T>
    {
        /// <summary>
        ///     Sorts the elements in the entire <paramref name="items" />.
        /// </summary>
        /// <param name="items">
        ///     Represents a collection of elements to sort.
        /// </param>
        public abstract void Sort(IList<T> items);
    }
}