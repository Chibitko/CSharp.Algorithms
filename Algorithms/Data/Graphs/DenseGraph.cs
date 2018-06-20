using System;
using System.Collections.Generic;

namespace Algorithms.Data.Graphs
{
    public sealed class DenseGraph<T> : GraphBase<T>
    {
        private readonly T[,] m_graph;

        /// <summary>
        ///     Initializes a graph object.
        /// </summary>
        /// <param name="vertexCount">Vertex count.</param>
        /// <exception cref="ArgumentOutOfRangeException">: vertexCount less than or equal 0.</exception>
        public DenseGraph(int vertexCount)
        {
            if (vertexCount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(vertexCount));
            }
            m_graph = new T[vertexCount, vertexCount];
            VertexCount = vertexCount;
        }

        /// <summary>
        ///     Initializes a graph object.
        /// </summary>
        /// <param name="graph">Graph as a 2D array.</param>
        /// <exception cref="ArgumentNullException">: array is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">: array is empty or has different dimension sizes.</exception>
        public DenseGraph(T[,] graph)
        {
            if (graph == null)
            {
                throw new ArgumentNullException(nameof(graph));
            }
            if (graph.Length == 0 || graph.GetLength(0) != graph.GetLength(1))
            {
                throw new ArgumentOutOfRangeException(nameof(graph));
            }
            m_graph = graph;
            VertexCount = graph.GetLength(0);
        }

        public override int VertexCount { get; }

        public override T this[int from, int to]
        {
            get => m_graph[from, to];
            set => m_graph[from, to] = value;
        }

        public override bool TryGetEdge(int from, int to, out T edge)
        {
            edge = m_graph[from, to];
            return true;
        }

        public override IReadOnlyList<Neighbor> GetNeighbors(int vertex)
        {
            var result = new Neighbor[VertexCount - 1];
            for (int i = 0; i < vertex; i++)
            {
                result[i] = new Neighbor
                {
                    Number = i,
                    Value = m_graph[vertex, i]
                };
            }
            for (int i = vertex + 1, n = VertexCount; i < n; i++)
            {
                result[i - 1] = new Neighbor
                {
                    Number = i,
                    Value = m_graph[vertex, i]
                };
            }
            return result;
        }
    }
}