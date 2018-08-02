using System;
using System.Collections.Generic;

namespace Algorithms.Data.Trees
{
    /// <summary>
    ///     Represents the binary search tree node
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the tree</typeparam>
    /// <typeparam name="TValue">The type of the data values in the tree</typeparam>
    public class BinarySearchTreeNode<TKey, TValue> : BinaryTree<BinarySearchTreeNode<TKey, TValue>, KeyValuePair<TKey, TValue>>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a node object.
        /// </summary>
        /// <param name="key">The node's key</param>
        /// <param name="value">The node's data value</param>
        public BinarySearchTreeNode(TKey key, TValue value)
        {
            Value = new KeyValuePair<TKey, TValue>(key, value);
        }

        /// <summary>
        ///     Searches the node for a child that contains key
        /// </summary>
        /// <param name="key">Search key</param>
        /// <param name="comparer">The key comparer</param>
        /// <returns>Returns a child if found and NULL if not</returns>
        public BinarySearchTreeNode<TKey, TValue> Find(TKey key, IComparer<TKey> comparer = null)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            var cmp = comparer ?? Comparer<TKey>.Default;
            var current = this;
            // trace down the tree until we hit a key
            while (current != null)
            {
                var c = cmp.Compare(key, current.Value.Key);
                if (c == 0)
                {
                    // key is found, return current node
                    return current;
                }
                if (c < 0)
                {
                    // current.Value.Key > key, search current's left subtree
                    current = current.Left;
                }
                else
                {
                    // current.Value.Key < key, search current's right subtree
                    current = current.Right;
                }
            }
            return null;
        }

        /// <summary>
        ///     Breadth-first search (BFS)
        /// </summary>
        /// <param name="node">The func must be called for each node visited while BFS running</param>
        public void BreadthFirstSearch(Func<BinarySearchTreeNode<TKey, TValue>, bool> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }
            var queue = new Queue<BinarySearchTreeNode<TKey, TValue>>();
            queue.Enqueue(this);
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (node(current))
                {
                    return;
                }
                if (current.Left != null)
                {
                    queue.Enqueue(current.Left);
                }
                if (current.Right != null)
                {
                    queue.Enqueue(current.Right);
                }
            }
        }

        /// <summary>
        ///     Adds a new node to the tree
        /// </summary>
        /// <param name="tree">The tree to add a node</param>
        /// <param name="key">The node key</param>
        /// <param name="value">The node data value</param>
        /// <param name="comparer">The key comparer</param>
        /// <returns>Returns TRUE if a new node was added; returns FALSE if a node with the same key was found</returns>
        public static bool Add(ref BinarySearchTreeNode<TKey, TValue> tree, TKey key, TValue value, IComparer<TKey> comparer = null)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            var cmp = comparer ?? Comparer<TKey>.Default;
            var current = tree;
            var parent = (BinarySearchTreeNode<TKey, TValue>) null;
            var c = 0;
            // trace down the tree until we hit a NULL
            while (current != null)
            {
                c = cmp.Compare(key, current.Value.Key);
                if (c == 0)
                {
                    // attempting to enter a duplicate - do nothing
                    // a new node was not added
                    return false;
                }
                parent = current;
                if (c < 0)
                {
                    // current.Value.Key > key, must add value to current's left subtree
                    current = current.Left;
                }
                else
                {
                    // current.Value.Key < key, must add value to current's right subtree
                    current = current.Right;
                }
            }
            var node = new BinarySearchTreeNode<TKey, TValue>(key, value);
            if (parent == null)
            {
                // the tree was empty, make node the root
                tree = node;
            }
            else
            {
                if (c < 0)
                {
                    // parent.Value.Key > key, therefore node must be added to the left subtree
                    parent.Left = node;
                }
                else
                {
                    // parent.Value.Key < key, therefore node must be added to the right subtree
                    parent.Right = node;
                }
            }
            // a new node was added
            return true;
        }

        /// <summary>
        ///     Removes a node from the tree
        /// </summary>
        /// <param name="tree">The tree to remove a node</param>
        /// <param name="key">The node key</param>
        /// <param name="comparer">The key comparer</param>
        /// <returns>Returns TRUE if a node was removed; returns FALSE if a node with the same key was not found</returns>
        public static bool Remove(ref BinarySearchTreeNode<TKey, TValue> tree, TKey key, IComparer<TKey> comparer = null)
        {
            if (tree == null)
            {
                throw new ArgumentNullException(nameof(tree));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            var cmp = comparer ?? Comparer<TKey>.Default;
            var current = tree;
            var parent = (BinarySearchTreeNode<TKey, TValue>) null;
            int c;
            // trace down the tree until we hit a NULL
            while (current != null)
            {
                c = cmp.Compare(key, current.Value.Key);
                if (c == 0)
                {
                    break;
                }
                parent = current;
                if (c < 0)
                {
                    // current.Value.Key > key, search current's left subtree
                    current = current.Left;
                }
                else
                {
                    // current.Value.Key < key, search current's right subtree
                    current = current.Right;
                }
            }
            if (current == null)
            {
                // a node with the key was not found
                return false;
            }
            // if current has no right child, then current's left child becomes the node pointed to by the parent
            if (current.Right == null)
            {
                if (parent == null)
                {
                    tree = current.Left;
                }
                else
                {
                    c = cmp.Compare(parent.Value.Key, current.Value.Key);
                    if (c > 0)
                    {
                        // parent.Value.Key > current.Value.Key, so make current's left child a left child of parent
                        parent.Left = current.Left;
                    }
                    else
                    {
                        // parent.Value.Key < current.Value.Key, so make current's left child a right child of parent
                        parent.Right = current.Left;
                    }
                }
            }
            // if current's right child has no left child, then current's right child replaces current in the tree
            else if (current.Right.Left == null)
            {
                current.Right.Left = current.Left;
                if (parent == null)
                {
                    tree = current.Right;
                }
                else
                {
                    c = cmp.Compare(parent.Value.Key, current.Value.Key);
                    if (c > 0)
                    {
                        // parent.Value > current.Value, so make current's right child a left child of parent
                        parent.Left = current.Right;
                    }
                    else if (c < 0)
                    {
                        // parent.Value < current.Value, so make current's right child a right child of parent
                        parent.Right = current.Right;
                    }
                }
            }
            // if current's right child has a left child, replace current with current's right child's left-most descendent
            else
            {
                // We first need to find the right node's left-most child
                var leftMost = current.Right.Left;
                var leftMostParent = current.Right;
                while (leftMost.Left != null)
                {
                    leftMostParent = leftMost;
                    leftMost = leftMost.Left;
                }

                // the parent's left subtree becomes the leftMost's right subtree
                leftMostParent.Left = leftMost.Right;

                // assign leftmost's left and right to current's left and right children
                leftMost.Left = current.Left;
                leftMost.Right = current.Right;

                if (parent == null)
                {
                    tree = leftMost;
                }
                else
                {
                    c = cmp.Compare(parent.Value.Key, current.Value.Key);
                    if (c > 0)
                    {
                        // parent.Value > current.Value, so make leftmost a left child of parent
                        parent.Left = leftMost;
                    }
                    else if (c < 0)
                    {
                        // parent.Value < current.Value, so make leftmost a right child of parent
                        parent.Right = leftMost;
                    }
                }
            }
            // a node with the key was removed
            return true;
        }
    }
}