using Algorithms;
using Xunit;

namespace Tests
{
    public sealed class ExtensionsTest
    {
        [Fact]
        public void Median()
        {
            Assert.Equal(1, new[] { 1, 2, 3 }.IndexOfMediane(0, 1, 2));
            Assert.Equal(2, new[] { 1, 3, 2 }.IndexOfMediane(0, 1, 2));
            Assert.Equal(0, new[] { 2, 1, 3 }.IndexOfMediane(0, 1, 2));
            Assert.Equal(0, new[] { 2, 3, 1 }.IndexOfMediane(0, 1, 2));
            Assert.Equal(2, new[] { 3, 1, 2 }.IndexOfMediane(0, 1, 2));
            Assert.Equal(1, new[] { 3, 2, 1 }.IndexOfMediane(0, 1, 2));
        }
    }
}