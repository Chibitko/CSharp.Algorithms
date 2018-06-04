namespace Algorithms.Data.Graphs
{
    /// <summary>
    ///     Base graph class.
    /// </summary>
    /// <typeparam name="T">Type of edge.</typeparam>
    public abstract class GraphBase<T>
    {
        /// <summary>
        ///     Graph vertex count.
        /// </summary>
        public abstract int VertexCount { get; }

        /// <summary>
        ///     Edges.
        /// </summary>
        /// <param name="from">The initial vertex of an edge.</param>
        /// <param name="to">The terminal vertex of an edge.</param>
        /// <returns>An edge value - bool, int and etc.</returns>
        public abstract T this[int from, int to] { get; set; }
    }
}