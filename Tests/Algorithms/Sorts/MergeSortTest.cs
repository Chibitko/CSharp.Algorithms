using Algorithms.Sorts;
using Xunit;

namespace Tests.Algorithms.Sorts
{
    public class MergeSortTest : SortTestBase
    {
        [Fact]
        public void NonRecursive()
        {
            var mergSort = new MergeSort<Item<int>>(MergeSortKind.NonRecursive);
            SortTest(mergSort, 15, true);
        }

        [Fact]
        public void CopyFirstRecursive()
        {
            var mergSort = new MergeSort<Item<int>>(MergeSortKind.CopyFirstRecursive);
            SortTest(mergSort, 15, true);
        }

        [Fact]
        public void Recursive()
        {
            var mergSort = new MergeSort<Item<int>>(MergeSortKind.Recursive);
            SortTest(mergSort, 15, true);
        }
    }
}