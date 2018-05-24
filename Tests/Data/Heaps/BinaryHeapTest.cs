using System.Linq;
using Algorithms.Data.Heaps;
using Xunit;

namespace Tests.Data.Heaps
{
    public sealed class BinaryHeapTest
    {
        [Fact]
        public void Test()
        {
            var heap = new BinaryHeap<int>();
            heap.Push(2);
            Assert.True(heap.SequenceEqual(new[] { 2 }));
            heap.Push(1);
            Assert.True(heap.SequenceEqual(new[] { 2, 1 }));
            heap.Push(3);
            Assert.True(heap.SequenceEqual(new[] { 3, 1, 2 }));
            heap.Push(4);
            Assert.True(heap.SequenceEqual(new[] { 4, 3, 2, 1 }));
            heap.Push(5);
            Assert.True(heap.SequenceEqual(new[] { 5, 4, 2, 1, 3 }));
            heap.Push(6);
            Assert.True(heap.SequenceEqual(new[] { 6, 4, 5, 1, 3, 2 }));
            heap.Push(7);
            Assert.True(heap.SequenceEqual(new[] { 7, 4, 6, 1, 3, 2, 5 }));
            var max = heap.Pop();
            Assert.Equal(7, max);
            Assert.True(heap.SequenceEqual(new[] { 6, 4, 5, 1, 3, 2 }));
            max = heap.Pop();
            Assert.Equal(6, max);
            Assert.True(heap.SequenceEqual(new[] { 5, 4, 2, 1, 3 }));
            max = heap.Pop();
            Assert.Equal(5, max);
            Assert.True(heap.SequenceEqual(new[] { 4, 3, 2, 1 }));
            max = heap.Pop();
            Assert.Equal(4, max);
            Assert.True(heap.SequenceEqual(new[] { 3, 1, 2 }));
            max = heap.Pop();
            Assert.Equal(3, max);
            Assert.True(heap.SequenceEqual(new[] { 2, 1 }));
            max = heap.Pop();
            Assert.Equal(2, max);
            Assert.True(heap.SequenceEqual(new[] { 1 }));
            max = heap.Pop();
            Assert.Equal(1, max);
            Assert.Equal(0, heap.Count);
        }
    }
}