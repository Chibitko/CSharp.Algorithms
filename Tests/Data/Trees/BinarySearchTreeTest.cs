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
            var tree = new BinarySearchTree<int, int>();
            Assert.True(tree.Find(1000) == null);
            tree.Add(100, 100);
            Assert.True(tree.Find(100) != null);
            tree.Add(150, 150);
            Assert.True(tree.Find(150) != null);
            tree.Add(50, 50);
            Assert.True(tree.Find(50) != null);
            tree.Add(25, 25);
            Assert.True(tree.Find(25) != null);
            tree.Add(75, 75);
            Assert.True(tree.Find(75) != null);
            tree.Add(125, 125);
            Assert.True(tree.Find(125) != null);
            tree.Add(175, 175);
            Assert.True(tree.Find(175) != null);
        }

        [Fact]
        public void Add()
        {
            var tree = new BinarySearchTree<int, int>();
            Assert.True(tree.Add(100, 100));
            Assert.Equal("100", GetKeysAsCommaSeparatedString(tree.Root));
            Assert.True(tree.Add(150, 150));
            Assert.Equal("100,150", GetKeysAsCommaSeparatedString(tree.Root));
            Assert.True(tree.Add(50, 50));
            Assert.Equal("100,50,150", GetKeysAsCommaSeparatedString(tree.Root));
            Assert.True(tree.Add(25, 25));
            Assert.Equal("100,50,150,25", GetKeysAsCommaSeparatedString(tree.Root));
            Assert.True(tree.Add(75, 75));
            Assert.Equal("100,50,150,25,75", GetKeysAsCommaSeparatedString(tree.Root));
            Assert.True(tree.Add(125, 125));
            Assert.Equal("100,50,150,25,75,125", GetKeysAsCommaSeparatedString(tree.Root));
            Assert.True(tree.Add(175, 175));
            Assert.Equal("100,50,150,25,75,125,175", GetKeysAsCommaSeparatedString(tree.Root));
            Assert.False(tree.Add(175, 175));
        }

        [Fact]
        public void Remove()
        {
            var tree = new BinarySearchTree<int, int>();
            tree.Add(100, 100);
            tree.Add(150, 150);
            tree.Add(50, 50);
            tree.Add(25, 25);
            tree.Add(75, 75);
            tree.Add(125, 125);
            tree.Add(175, 175);
            tree.Add(10, 10);
            tree.Add(190, 190);
            Assert.Equal("100,50,150,25,75,125,175,10,190", GetKeysAsCommaSeparatedString(tree.Root));
            Assert.False(tree.Remove(1000));
            // if current has no right child
            Assert.True(tree.Remove(25));
            Assert.Equal("100,50,150,10,75,125,175,190", GetKeysAsCommaSeparatedString(tree.Root));
            // if current's right child has no left child
            tree.Add(5, 5);
            tree.Add(15, 15);
            Assert.Equal("100,50,150,10,75,125,175,5,15,190", GetKeysAsCommaSeparatedString(tree.Root));
            Assert.True(tree.Remove(10));
            Assert.Equal("100,50,150,15,75,125,175,5,190", GetKeysAsCommaSeparatedString(tree.Root));
            // if current's right child has a left child
            tree.Add(145, 145);
            Assert.Equal("100,50,150,15,75,125,175,5,145,190", GetKeysAsCommaSeparatedString(tree.Root));
            Assert.True(tree.Remove(100));
            Assert.Equal("125,50,150,15,75,145,175,5,190", GetKeysAsCommaSeparatedString(tree.Root));
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