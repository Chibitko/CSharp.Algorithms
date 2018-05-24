using Algorithms.Sorts;
using Xunit;

namespace Tests.Sorts
{
    public sealed class InsertionSortTest : SortTestBase
    {
        [Fact]
        public void Test()
        {
            var sort = new InsertionSort<Item<int>>();
            SortTest(sort, 15, true);
        }
    }
}