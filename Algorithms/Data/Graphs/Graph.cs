using System;

namespace Algorithms.Data.Graphs
{
    public class Graph<T> : GraphBase<T>
    {
        private readonly T[,] m_graph;

        /// <summary>
        ///     Initializes a graph object.
        /// </summary>
        /// <param name="vertexCount">Vertex count.</param>
        /// <exception cref="ArgumentOutOfRangeException">: vertexCount less than or equal 0.</exception>
        public Graph(int vertexCount)
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
        /// <exception cref="ArgumentOutOfRangeException">: array is empty or has a different dimension size.</exception>
        public Graph(T[,] graph)
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
    }
}