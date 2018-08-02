using System.Text;
using Algorithms.Data.Trees;
using Xunit;

namespace Tests.Data.Trees
{
    public sealed class BinarySearchTreeTest
    {
        [Fact]
        public void BreadthFirstSearch()
        {
            var root = new BinarySearchTreeNode<int, int>(100, 100)
            {
                Left = new BinarySearchTreeNode<int, int>(50, 50)
                {
                    Left = new BinarySearchTreeNode<int, int>(25, 25),
                    Right = new BinarySearchTreeNode<int, int>(75, 75)
                },
                Right = new BinarySearchTreeNode<int, int>(150, 150)
                {
                    Left = new BinarySearchTreeNode<int, int>(125, 125),
                    Right = new BinarySearchTreeNode<int, int>(175, 175)
                }
            };

            Assert.Equal("100,50,150,25,75,125,175", GetKeysAsCommaSeparatedString(root));
        }

        [Fact]
        public void Find()
        {
            var root = new BinarySearchTree<int, int>();
            Assert.True(root.Find(1000) == null);
            root.Add(100, 100);
            Assert.True(root.Find(100) != null);
            root.Add(150, 150);
            Assert.True(root.Find(150) != null);
            root.Add(50, 50);
            Assert.True(root.Find(50) != null);
            root.Add(25, 25);
            Assert.True(root.Find(25) != null);
            root.Add(75, 75);
            Assert.True(root.Find(75) != null);
            root.Add(125, 125);
            Assert.True(root.Find(125) != null);
            root.Add(175, 175);
            Assert.True(root.Find(175) != null);
        }

        [Fact]
        public void Add()
        {
            var root = new BinarySearchTree<int, int>();
            Assert.True(root.Add(100, 100));
            Assert.Equal("100", GetKeysAsCommaSeparatedString(root));
            Assert.True(root.Add(150, 150));
            Assert.Equal("100,150", GetKeysAsCommaSeparatedString(root));
            Assert.True(root.Add(50, 50));
            Assert.Equal("100,50,150", GetKeysAsCommaSeparatedString(root));
            Assert.True(root.Add(25, 25));
            Assert.Equal("100,50,150,25", GetKeysAsCommaSeparatedString(root));
            Assert.True(root.Add(75, 75));
            Assert.Equal("100,50,150,25,75", GetKeysAsCommaSeparatedString(root));
            Assert.True(root.Add(125, 125));
            Assert.Equal("100,50,150,25,75,125", GetKeysAsCommaSeparatedString(root));
            Assert.True(root.Add(175, 175));
            Assert.Equal("100,50,150,25,75,125,175", GetKeysAsCommaSeparatedString(root));
            Assert.False(root.Add(175, 175));
        }

        [Fact]
        public void Remove()
        {
            var root = new BinarySearchTree<int, int>();
            root.Add(100, 100);
            root.Add(150, 150);
            root.Add(50, 50);
            root.Add(25, 25);
            root.Add(75, 75);
            root.Add(125, 125);
            root.Add(175, 175);
            root.Add(10, 10);
            root.Add(190, 190);
            Assert.Equal("100,50,150,25,75,125,175,10,190", GetKeysAsCommaSeparatedString(root));
            Assert.False(root.Remove(1000));
            // if current has no right child
            Assert.True(root.Remove(25));
            Assert.Equal("100,50,150,10,75,125,175,190", GetKeysAsCommaSeparatedString(root));
            // if current's right child has no left child
            root.Add(5, 5);
            root.Add(15, 15);
            Assert.Equal("100,50,150,10,75,125,175,5,15,190", GetKeysAsCommaSeparatedString(root));
            Assert.True(root.Remove(10));
            Assert.Equal("100,50,150,15,75,125,175,5,190", GetKeysAsCommaSeparatedString(root));
            // if current's right child has a left child
            root.Add(145, 145);
            Assert.Equal("100,50,150,15,75,125,175,5,145,190", GetKeysAsCommaSeparatedString(root));
            Assert.True(root.Remove(100));
            Assert.Equal("125,50,150,15,75,145,175,5,190", GetKeysAsCommaSeparatedString(root));
        }

        private static string GetKeysAsCommaSeparatedString(BinarySearchTree<int, int> root)
        {
            var bfs = new StringBuilder();
            root.BreadthFirstSearch(node =>
            {
                if (bfs.Length > 0)
                {
                    bfs.Append(",");
                }
                bfs.Append(node.Value.Key);
                return false;
            });
            return bfs.ToString();
        }

        private static string GetKeysAsCommaSeparatedString(BinarySearchTreeNode<int, int> root)
        {
            var bfs = new StringBuilder();
            root.BreadthFirstSearch(node =>
            {
                if (bfs.Length > 0)
                {
                    bfs.Append(",");
                }
                bfs.Append(node.Value.Key);
                return false;
            });
            return bfs.ToString();
        }
    }
}