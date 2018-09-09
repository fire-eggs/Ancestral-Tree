/*
The C# port of the abego TreeLayout project. Extended by Kevin Routley
for the Ancestral-Tree project.

The abego TreeLayout project is distributed under BSD License:
http://treelayout.sourceforge.net/
https://github.com/abego/treelayout

The C# port:
https://sourceforge.net/projects/citexplore-code-treelayout/

The Ancestral-Tree change is to add support for "not-real" nodes. "Real"
and "not-real" nodes are treated the same except that "not-real" nodes
are NOT to be centered relative to their parents.
*/

// TODO need better name than 'IsReal'
// TODO clean up all java-style doc to .NET doc

using System;
using System.Collections.Generic;
using System.Linq;

namespace AncesTree.TreeLayout
{
    /// <summary>
    /// This is the minimum interface required for the tree layout to function.
    /// 
    /// The concrete implementation of the interface may have any additional
    /// properties required for drawing purposes.
    /// </summary>
    public interface ITreeNode
    {
        int Wide { get; }
        int High { get; }
        bool IsReal { get; }
    }

    /**
     * Provides an easy way to implement the {@link org.abego.treelayout.TreeForTreeLayout} interface by
     * defining just two simple methods and a constructor.
     * <p>
     * To use this class the underlying tree must provide the children as a list
     * (see {@link #getChildrenList(Object)} and give direct access to the parent of
     * a node (see {@link #getParent(Object)}).
     * <p>
     * 
     * See also {@link DefaultTreeForTreeLayout}.
     * 
     * @author Udo Borkowski (ub@abego.org)
     * 
     * @param <TreeNode> Type of elements used as nodes in the tree
     */
    abstract public class AbstractTreeForTreeLayout<TTreeNode> : TreeForTreeLayout<TTreeNode> where TTreeNode : ITreeNode
    {

        /**
         * Returns the parent of a node, if it has one.
         * <p>
         * Time Complexity: O(1)
         * 
         * @param node &nbsp;
         * @return [nullable] the parent of the node, or null when the node is a
         *         root.
         */
        abstract public TTreeNode getParent(TTreeNode node);

        /**
         * Return the children of a node as a {@link List}.
         * <p>
         * Time Complexity: O(1)
         * <p>
         * Also the access to an item of the list must have time complexity O(1).
         * <p>
         * A client must not modify the returned list.
         * 
         * @param node &nbsp;
         * @return the children of the given node. When node is a leaf the list is
         *         empty.
         */
        abstract public List<TTreeNode> getChildrenList(TTreeNode node);

        private readonly TTreeNode root;

        public AbstractTreeForTreeLayout(TTreeNode root)
        {
            this.root = root;
        }

        public TTreeNode getRoot()
        {
            return root;
        }

        public bool isLeaf(TTreeNode node)
        {
            return getChildrenList(node).Count == 0;
        }

        public bool isChildOfParent(TTreeNode node, TTreeNode parentNode)
        {
            return Object.ReferenceEquals(getParent(node), parentNode);

        }

        public IEnumerable<TTreeNode> getChildren(TTreeNode node)
        {
            return getChildrenList(node);
        }

        public IEnumerable<TTreeNode> getChildrenReverse(TTreeNode node)
        {
            return getChildren(node).Reverse();
        }

        /// <summary>
        /// Acquire the first child node of a given node.
        /// </summary>
        /// <param name="parentNode"></param>
        /// <returns></returns>
        public TTreeNode getFirstChild(TTreeNode parentNode)
        {
            return getChildrenList(parentNode)[0];
        }

        /// <summary>
        /// Acquire the last child node of a given node.
        /// </summary>
        /// <param name="parentNode"></param>
        /// <returns></returns>
        public TTreeNode getLastChild(TTreeNode parentNode)
        {
            var list = getChildrenList(parentNode);
            return list[list.Count - 1];
        }

        /// <summary>
        /// Acquire the first real child node of a given node.
        /// </summary>
        /// <param name="parentNode"></param>
        /// <returns></returns>
        public TTreeNode getFirstRealChild(TTreeNode parentNode)
        {
            var list = getChildrenList(parentNode);
            foreach (var child in list)
            {
                if (child.IsReal)
                    return child;
            }
            return default(TTreeNode);
        }

        /// <summary>
        /// Acquire the last real child node of a given node.
        /// </summary>
        /// <param name="parentNode"></param>
        /// <returns></returns>
        public TTreeNode getLastRealChild(TTreeNode parentNode)
        {
            var list = getChildrenReverse(parentNode);
            foreach (var child in list)
            {
                if (child.IsReal)
                    return child;
            }
            return default(TTreeNode);
        }
    }
}
