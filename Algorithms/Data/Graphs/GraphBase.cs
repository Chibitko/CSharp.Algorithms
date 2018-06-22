using System.Collections.Generic;

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
        /// <returns>An edge value or default(T) if an edge is absent.</returns>
        public abstract T this[int from, int to] { get; set; }

        /// <summary>
        ///     Returns an edge value.
        /// </summary>
        /// <param name="from">The initial vertex of an edge.</param>
        /// <param name="to">The terminal vertex of an edge.</param>
        /// <param name="edge">Edge value</param>
        /// <returns>true if edge is present in the grath otherwise false.</returns>
        public abstract bool TryGetEdge(int from, int to, out T edge);

        /// <summary>
        ///     Returns neighbors of a vertex.
        /// </summary>
        /// <param name="vertex">Vertex.</param>
        /// <returns>Vertex neighbors.</returns>
        public abstract IEnumerable<Neighbor> GetNeighbors(int vertex);

        #region Internal types

        public struct Neighbor
        {
            public int Number { get; internal set; }

            public T Value { get; internal set; }
        }

        #endregion
    }
}