using System;
using System.Collections.Generic;
using Algorithms.Data.Graphs;

namespace Algorithms.Graphs
{
    public sealed class FloydWarshall
    {
        public static readonly int? INF = null;

        private readonly GraphBase<int?> m_weights;
        private readonly DistanceComparer m_cmp;

        public FloydWarshall(GraphBase<int?> weights)
        {
            m_weights = weights ?? throw new ArgumentNullException(nameof(weights));
            m_cmp = new DistanceComparer();
        }

        /// <summary>
        ///     Returns the shortest distances and paths between all vertexes.
        /// </summary>
        /// <returns>Shortest distances and paths.</returns>
        public (int?[,] distance, int[,] next) GetResult()
        {
            var n = m_weights.VertexCount;

            var d = new int?[n, n];
            var next = new int[n, n];

            // Initialization.
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    d[i, j] = m_weights[i, j];
                    next[i, j] = j;
                }
            }

            for (int k = 0; k < n; ++k)
            {
                for (int i = 0; i < n; ++i)
                {
                    for (int j = 0; j < n; ++j)
                    {
                        if (m_cmp.Compare(d[i, j], d[i, k] + d[k, j]) > 0)
                        {
                            d[i, j] = d[i, k] + d[k, j];
                            next[i, j] = next[i, k];
                        }
                    }
                }
            }

            return (d, next);
        }

        public IReadOnlyList<int> GetPath(int[,] next, int i, int j)
        {
            var result = new List<int>();
            if (i == j)
            {
                return result;
            }
            var c = i;
            while (c != j)
            {
                result.Add(c);
                c = next[c, j];
            }
            result.Add(j);
            return result;
        }
    }
}