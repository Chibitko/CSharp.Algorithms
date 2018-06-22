using System.Collections.Generic;
using Algorithms.Data.Graphs;
using Algorithms.Graphs;
using Xunit;

namespace Tests.Graphs
{
    public sealed class DijkstraTest
    {
        [Fact]
        public void DenseGraph()
        {
            var weights = new DenseGraph<int?>(new int?[,]
            {
                { 0000, 0007, 0009, null, null, 0014 },
                { 0007, 0000, 0010, 0015, null, null },
                { 0009, 0010, 0000, 0011, null, 0002 },
                { null, 0015, 0011, 0000, 0006, null },
                { null, null, null, 0006, 0000, 0009 },
                { 0014, null, 0002, null, 0009, 0000 },
            });
            var dijkstra = new Dijkstra(weights);
            var distances = dijkstra.GetResult(0);
            Assert.Equal(distances, new List<Dijkstra.Vertex>
            {
                new Dijkstra.Vertex(0),
                new Dijkstra.Vertex(7, 0),
                new Dijkstra.Vertex(9, 0),
                new Dijkstra.Vertex(20, 2),
                new Dijkstra.Vertex(20, 5),
                new Dijkstra.Vertex(11, 2)
            });
        }

        [Fact]
        public void SparseGraph()
        {
            var weights = new SparseGraph<int?>(6)
            {
                [0, 1] = 7,
                [0, 2] = 9,
                [0, 5] = 14,
                [1, 0] = 7,
                [1, 2] = 10,
                [1, 3] = 15,
                [2, 0] = 9,
                [2, 1] = 10,
                [2, 3] = 11,
                [2, 5] = 2,
                [3, 1] = 15,
                [3, 2] = 11,
                [3, 4] = 6,
                [4, 3] = 6,
                [4, 5] = 9,
                [5, 0] = 14,
                [5, 2] = 2,
                [5, 4] = 9
            };
            var dijkstra = new Dijkstra(weights);
            var distances = dijkstra.GetResult(0);
            Assert.Equal(distances, new List<Dijkstra.Vertex>
            {
                new Dijkstra.Vertex(0),
                new Dijkstra.Vertex(7, 0),
                new Dijkstra.Vertex(9, 0),
                new Dijkstra.Vertex(20, 2),
                new Dijkstra.Vertex(20, 5),
                new Dijkstra.Vertex(11, 2)
            });
        }
    }
}