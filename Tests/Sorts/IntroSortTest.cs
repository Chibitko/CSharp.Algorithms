using System.Linq;
using Algorithms.Sorts;
using Xunit;

namespace Tests.Sorts
{
    public sealed class IntroSortTest : SortTestBase
    {
        [Fact]
        public void Test()
        {
            var sort = new IntroSort<Item<int>>();
            SortTest(sort, 50, false);
        }

        [Fact]
        public void HeapSortUsed()
        {
            var sort = new IntroSort<Item<int>>(3);
            var data = Enumerable.Range(0, 50)
                .Select(i => new Item<int>(Randomizer.Next(), i))
                .ToArray();
            sort.Sort(data);
            Assert.True(IsSorted(data));
            Assert.True(sort.HeadSortUsed);
        }
    }
}