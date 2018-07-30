namespace Algorithms.Data.Trees
{
    public class BinaryTree<TBinaryTree, TValue>
        where TBinaryTree : BinaryTree<TBinaryTree, TValue>
    {
        public TValue Value { get; set; }

        public TBinaryTree Left { get; set; }

        public TBinaryTree Right { get; set; }
    }
}