using System.Collections.Generic;
using Algorithms.Data.Graphs;
using Algorithms.Graphs;
using Xunit;

namespace Tests.Graphs
{
    public sealed class DijkstraTest
    {
        [Fact]
        public void Test()
        {
            var weights = new Graph<int?>(new int?[,]
            {
                { 0000, 0007, 0009, null, null, 0014 },
                { 0007, 0000, 0010, 0015, null, null },
                { 0009, 0010, 0000, 0011, null, 0002 },
                { null, 0015, 0011, 0000, 0006, null },
                { null, null, null, 0006, 0000, 0009 },
                { 0014, null, 0002, null, 0009, 0000 },
            });
            var dijkstrasAlgorithm = new Dijkstra(weights);
            var distances = dijkstrasAlgorithm.GetResult(0);
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