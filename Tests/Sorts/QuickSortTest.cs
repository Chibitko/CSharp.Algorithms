using Algorithms.Sorts;
using Xunit;

namespace Tests.Algorithms.Sorts
{
    public sealed class QuickSortTest : SortTestBase
    {
        [Fact]
        public void NonRecursive()
        {
            var sort = new QuickSort<Item<int>>(QuickSortKind.NonRecursive);
            SortTest(sort, 15, false);
        }

        [Fact]
        public void Recursive()
        {
            var sort = new QuickSort<Item<int>>(QuickSortKind.Recursive);
            SortTest(sort, 15, false);
        }

        [Fact]
        public void ImprovedRecursive()
        {
            var sort = new QuickSort<Item<int>>(QuickSortKind.ImprovedRecursive);
            SortTest(sort, 50, false);
        }
    }
}