using Algorithms.Sorts;
using Xunit;

namespace Tests.Algorithms.Sorts
{
    public sealed class HeapSortTest : SortTestBase
    {
        [Fact]
        public void HeapifyDown()
        {
            var sort = new HeapSort<Item<int>>(HeapSortKind.HeapifyDown);
            SortTest(sort, 15, false);
        }

        [Fact]
        public void HeapifyUp()
        {
            var sort = new HeapSort<Item<int>>(HeapSortKind.HeapifyUp);
            SortTest(sort, 15, false);
        }
    }
}