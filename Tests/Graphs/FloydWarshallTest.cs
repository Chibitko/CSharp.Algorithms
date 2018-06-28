using System.Linq;
using Algorithms;
using Algorithms.Data.Graphs;
using Algorithms.Graphs;
using Xunit;

namespace Tests.Graphs
{
    public sealed class FloydWarshallTest
    {
        [Fact]
        public void Test()
        {
            var weights = new DenseGraph<int?>(new int?[,]
            {
                { 0000, 0007, 0009, null, null, 0014 },
                { 0007, 0000, 0010, 0014, null, null },
                { 0009, 0010, 0000, 0011, null, 0002 },
                { null, 0014, 0011, 0000, 0006, null },
                { null, null, null, 0006, 0000, 0009 },
                { 0014, null, 0002, null, 0009, 0000 },
            });
            var floydWarshall = new FloydWarshall(weights);
            var floydWarshallResult = floydWarshall.GetResult();
            var dijkstra = new Dijkstra(weights);
            for (var i = 0; i < weights.VertexCount; i++)
            {
                var dijkstraResult = dijkstra.GetResult(i);
                Assert.Equal(floydWarshallResult.distance.SliceRow(i), dijkstraResult.Select(x => x.Distance));
                for (var j = 0; j < weights.VertexCount; j++)
                {
                    Assert.Equal(floydWarshall.GetPath(floydWarshallResult.next, i, j), dijkstra.GetPath(dijkstraResult, i, j));
                }
            }
        }
    }
}