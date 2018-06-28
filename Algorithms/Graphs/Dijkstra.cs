using System;
using System.Collections.Generic;
using System.Diagnostics;
using Algorithms.Data.Graphs;
using Algorithms.Data.Heaps;

namespace Algorithms.Graphs
{
    public sealed class Dijkstra
    {
        public static readonly int? INF = null;

        private readonly GraphBase<int?> m_weights;
        private readonly DistanceComparer m_cmp;

        public Dijkstra(GraphBase<int?> weights)
        {
            m_weights = weights ?? throw new ArgumentNullException(nameof(weights));
            m_cmp = new DistanceComparer();
        }

        /// <summary>
        ///     Returns the shortest distances and paths between <see cref="source" /> vertex ant other vertexes.
        /// </summary>
        /// <param name="source">Source vertex.</param>
        /// <returns>Shortest distances and paths.</returns>
        /// <exception cref="ArgumentOutOfRangeException">: source less than 0 or greater than or equal the graph vertext count.</exception>
        public IReadOnlyList<Vertex> GetResult(int source)
        {
            var n = m_weights.VertexCount;

            if (source < 0 || source >= n)
            {
                throw new ArgumentOutOfRangeException(nameof(source));
            }

            // Distances and paths.
            var result = new Vertex[n];

            // Visited vertexes.
            var visitedVertexes = new bool[n];

            // Binary heap to search vertex with min distance.
            var binaryHeap = m_weights is SparseGraph<int?>
                ? new BinaryHeap<(int v, int? d)>(new BinaryHeapComparer())
                : null;

            // Returns from the intermediate result a vertex with a min distance from the source vertex to it.
            int GetMinFromIntermediateResult()
            {
                var minIndex = -1;
                var minDistance = INF;
                for (var i = 0; i < n; i++)
                {
                    if (!visitedVertexes[i]
                        && (minIndex == -1 || m_cmp.Compare(minDistance, result[i].Distance) > 0))
                    {
                        minIndex = i;
                        minDistance = result[i].Distance;
                    }
                }
                return minIndex;
            }

            // Returns from the binary heap a vertex with a min distance from the source vertex to it.
            int GetMinFromBinaryHeap()
            {
                return binaryHeap.Count == 0
                    ? -1
                    : binaryHeap.Pop().v;
            }

            var getMin = binaryHeap == null
                ? GetMinFromIntermediateResult
                : (Func<int>) GetMinFromBinaryHeap;

            // Initialization.
            for (var i = 0; i < n; i++)
            {
                if (i == source)
                {
                    // The distance from the source vertex to itself is 0.
                    result[i] = new Vertex(0);
                }
                else
                {
                    // The distance from the source vertex to each other vertex is infinity.
                    result[i] = new Vertex(INF);
                }
            }

            binaryHeap?.Push((source, 0));

            while (true)
            {
                // 'min' is not-visited vertex with a shortest distance from the source vertex.
                var min = getMin();
                if (min == -1)
                {
                    // If not found work is done.
                    break;
                }

                // Mark 'min' as visited.
                visitedVertexes[min] = true;

                foreach (var neighbor in m_weights.GetNeighbors(min))
                {
                    if (neighbor.Value == INF)
                    {
                        // Vertexes are not connected.
                        continue;
                    }

                    if (visitedVertexes[neighbor.Number])
                    {
                        // Neighbor is visited.
                        continue;
                    }

                    // Alternate distance from the source vertex to the neighbor of 'min'.
                    var altDistance = result[min].Distance + neighbor.Value;

                    // Changing distance to the neighbor if a shorter path found.
                    if (m_cmp.Compare(result[neighbor.Number].Distance, altDistance) > 0)
                    {
                        result[neighbor.Number].Distance = altDistance;
                        result[neighbor.Number].Previous = min;
                        binaryHeap?.Push((neighbor.Number, altDistance));
                    }
                }
            }

            return result;
        }

        public IReadOnlyList<int> GetPath(IReadOnlyList<Vertex> vertexes, int i, int j)
        {
            var result = new List<int>();
            if (i == j)
            {
                return result;
            }
            var c = j;
            result.Add(c);
            while (vertexes[c].Previous != null)
            {
                result.Insert(0, (int) vertexes[c].Previous);
                c = (int) vertexes[c].Previous;
            }
            return result;
        }

        #region Internal types

        [DebuggerDisplay("{Distance} ({Previous})")]
        public struct Vertex
        {
            public Vertex(int? distance, int? previous = null)
            {
                Distance = distance;
                Previous = previous;
            }

            public int? Distance { get; internal set; }

            public int? Previous { get; internal set; }
        }

        private sealed class BinaryHeapComparer : IComparer<(int v, int? d)>
        {
            private readonly DistanceComparer m_cmp = new DistanceComparer();

            public int Compare((int v, int? d) x, (int v, int? d) y)
            {
                var result = m_cmp.Compare(x.d.Value, y.d.Value);
                if (result < 0)
                {
                    return 1;
                }
                if (result > 0)
                {
                    return -1;
                }
                return 0;
            }
        }

        #endregion
    }
}