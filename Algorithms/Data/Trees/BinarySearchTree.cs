using System;
using System.Collections.Generic;

namespace Algorithms.Data.Trees
{
    /// <summary>
    ///     Represents the binary search tree
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the tree</typeparam>
    /// <typeparam name="TValue">The type of the data values in the tree</typeparam>
    public sealed class BinarySearchTree<TKey, TValue>
    {
        private readonly IComparer<TKey> m_cmp;

        private BinarySearchTreeNode<TKey, TValue> m_root;

        /// <summary>
        ///     Initializes a tree object.
        /// </summary>
        /// <param name="comparer">The tree keys comparer</param>
        public BinarySearchTree(IComparer<TKey> comparer)
        {
            m_cmp = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a tree object.
        /// </summary>
        public BinarySearchTree()
            : this(Comparer<TKey>.Default)
        {
        }

        /// <summary>
        ///     Current root node.
        /// </summary>
        public BinarySearchTreeNode<TKey, TValue> Root => m_root;

        /// <summary>
        ///     Searches the tree for a node that contains key
        /// </summary>
        /// <param name="key">Search key</param>
        /// <returns>Returns a node if found and NULL if not</returns>
        public BinarySearchTreeNode<TKey, TValue> Find(TKey key)
        {
            return Root?.Find(key);
        }

        /// <summary>
        ///     Adds a new node to the tree
        /// </summary>
        /// <param name="key">The node key</param>
        /// <param name="value">The node data value</param>
        /// <returns>Returns TRUE if a new node was added; returns FALSE if a node with the same key was found</returns>
        public bool Add(TKey key, TValue value)
        {
            return BinarySearchTreeNode<TKey, TValue>.Add(ref m_root, key, value, m_cmp);
        }

        /// <summary>
        ///     Removes a node from the tree
        /// </summary>
        /// <param name="key">The node key</param>
        /// <returns>Returns TRUE if a node was removed; returns FALSE if a node with the same key was not found</returns>
        public bool Remove(TKey key)
        {
            return BinarySearchTreeNode<TKey, TValue>.Remove(ref m_root, key, m_cmp);
        }

        /// <summary>
        ///     Breadth-first search (BFS)
        /// </summary>
        /// <param name="node">The func must be called for each node visited while BFS running</param>
        public void BreadthFirstSearch(Func<BinarySearchTreeNode<TKey, TValue>, bool> node)
        {
            Root?.BreadthFirstSearch(node);
        }
    }
}