using System;
using System.Collections.Generic;
using System.Diagnostics;
using Algorithms.Data.Graphs;

namespace Algorithms.Graphs
{
    public sealed class Dijkstra
    {
        public static readonly int? InfinityDistance = null;

        private readonly Graph<int?> m_weights;
        private readonly int m_vertexCount;
        private readonly DistanceComparer m_distanceComparer;

        public Dijkstra(Graph<int?> weights)
        {
            m_weights = weights ?? throw new ArgumentNullException(nameof(weights));
            m_vertexCount = m_weights.VertexCount;
            m_distanceComparer = new DistanceComparer();
        }

        /// <summary>
        ///     Returns the shortest distances and paths between <see cref="source" /> vertex ant other vertexes.
        /// </summary>
        /// <param name="source">Source vertex.</param>
        /// <returns>Shortest distances and paths.</returns>
        /// <exception cref="ArgumentOutOfRangeException">: source less than 0 or greater than or equal the graph vertext count.</exception>
        public IReadOnlyList<Vertex> GetResult(int source)
        {
            if (source < 0 || source >= m_vertexCount)
            {
                throw new ArgumentOutOfRangeException(nameof(source));
            }

            // Distances and paths.
            var result = new Vertex[m_vertexCount];

            // Visited vertexes.
            var visitedVertexes = new bool[m_vertexCount];

            // Returns a vertex with a min distance from the source vertex to it.
            int GetNotVisitedMinDistanceVertex()
            {
                var minIndex = -1;
                var minDistance = InfinityDistance;
                for (int i = 0, n = result.Length; i < n; i++)
                {
                    if (!visitedVertexes[i]
                        && (minIndex == -1 || m_distanceComparer.Compare(minDistance, result[i].Distance) > 0))
                    {
                        minIndex = i;
                        minDistance = result[i].Distance;
                    }
                }
                return minIndex;
            }

            // Initialization.
            for (int i = 0; i < m_vertexCount; i++)
            {
                if (i == source)
                {
                    // The distance from the source vertex to itself is 0.
                    result[i] = new Vertex(0);
                }
                else
                {
                    // The distance from the source vertex to each other vertex is infinity.
                    result[i] = new Vertex(InfinityDistance);
                }
            }

            while (true)
            {
                // 'min' is not-visited vertex with a shortest distance from the source vertex.
                var min = GetNotVisitedMinDistanceVertex();
                if (min == -1)
                {
                    // If not found work is done.
                    break;
                }

                // Mark 'min' as visited.
                visitedVertexes[min] = true;

                for (var neighbor = 0; neighbor < m_vertexCount; neighbor++)
                {
                    if (neighbor == min)
                    {
                        // Itself.
                        continue;
                    }

                    if (visitedVertexes[neighbor])
                    {
                        // Neighbor is visited.
                        continue;
                    }

                    if (m_weights[min, neighbor] == InfinityDistance)
                    {
                        // Not neighbor, the vertexes are not connected.
                        continue;
                    }

                    // Alternate distance from the source vertex to the neighbor of 'min'.
                    var altDistance = result[min].Distance + m_weights[min, neighbor];
                    if (m_distanceComparer.Compare(result[neighbor].Distance, altDistance) > 0)
                    {
                        result[neighbor].Distance = altDistance;
                        result[neighbor].Previous = min;
                    }
                }
            }

            return result;
        }

        #region Internal types

        private sealed class DistanceComparer : IComparer<int?>
        {
            public int Compare(int? x, int? y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }
                if (x == null)
                {
                    return 1;
                }
                if (y == null)
                {
                    return -1;
                }
                return Comparer<int>.Default.Compare(x.Value, y.Value);
            }
        }

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

        #endregion
    }
}