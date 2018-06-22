using System;
using System.Collections.Generic;

namespace Algorithms.Data.Graphs
{
    public sealed class SparseGraph<T> : GraphBase<T>
    {
        private readonly List<Neighbor>[] m_graph;

        /// <summary>
        ///     Initializes a graph object.
        /// </summary>
        /// <param name="vertexCount">Vertex count.</param>
        /// <exception cref="ArgumentOutOfRangeException">: vertexCount less than or equal 0.</exception>
        public SparseGraph(int vertexCount)
        {
            if (vertexCount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(vertexCount));
            }
            m_graph = new List<Neighbor>[vertexCount];
            for (int i = 0, n = m_graph.Length; i < n; i++)
            {
                m_graph[i] = new List<Neighbor>();
            }
        }

        public override int VertexCount => m_graph.Length;

        public override T this[int from, int to]
        {
            get
            {
                if (TryGetEdge(from, to, out var edge))
                {
                    return edge;
                }
                return default(T);
            }
            set
            {
                var neighbors = m_graph[from];
                for (var i = 0; i < neighbors.Count; i++)
                {
                    if (neighbors[i].Number == to)
                    {
                        neighbors[i] = new Neighbor
                        {
                            Number = to,
                            Value = value
                        };
                        return;
                    }
                }
                neighbors.Add(new Neighbor
                {
                    Number = to,
                    Value = value
                });
            }
        }

        public override bool TryGetEdge(int from, int to, out T edge)
        {
            foreach (var neighbor in m_graph[from])
            {
                if (neighbor.Number == to)
                {
                    edge = neighbor.Value;
                    return true;
                }
            }
            edge = default(T);
            return false;
        }

        public override IEnumerable<Neighbor> GetNeighbors(int vertex)
        {
            return m_graph[vertex];
        }
    }
}