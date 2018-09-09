using System.Collections.Generic;

namespace AncesTree.TreeLayout
{
    public interface TreeForTreeLayout<TreeNode>
    {

        /**
         * Returns the the root of the tree.
         * <p>
         * Time Complexity: O(1)
         * 
         * @return the root of the tree
         */
        TreeNode getRoot();

        /**
         * Tells if a node is a leaf in the tree.
         * <p>
         * Time Complexity: O(1)
         * 
         * @param node &nbsp;
         * @return true iff node is a leaf in the tree, i.e. has no children.
         */
        bool isLeaf(TreeNode node);

        /**
         * Tells if a node is a child of a given parentNode.
         * <p>
         * Time Complexity: O(1)
         * 
         * @param node &nbsp;
         * @param parentNode &nbsp;
         * @return true iff the node is a child of the given parentNode
         */
        bool isChildOfParent(TreeNode node, TreeNode parentNode);

        /**
         * Returns the children of a parent node.
         * <p>
         * Time Complexity: O(1)
         * 
         * @param parentNode
         *            [!isLeaf(parentNode)]
         * @return the children of the given parentNode, from first to last
         */
        IEnumerable<TreeNode> getChildren(TreeNode parentNode);

        /**
         * Returns the children of a parent node, in reverse order.
         * <p>
         * Time Complexity: O(1)
         * 
         * @param parentNode
         *            [!isLeaf(parentNode)]
         * @return the children of given parentNode, from last to first
         */
        IEnumerable<TreeNode> getChildrenReverse(TreeNode parentNode);

        /**
         * Returns the first child of a parent node.
         * 
         * @param parentNode
         *            [!isLeaf(parentNode)]
         * @return the first child of the parentNode
         */
        TreeNode getFirstChild(TreeNode parentNode);

        /**
         * Returns the last child of a parent node.
         * 
         * @param parentNode
         *            [!isLeaf(parentNode)]
         * @return the last child of the parentNode
         */
        TreeNode getLastChild(TreeNode parentNode);

        /// <summary>
        /// Returns the first real child of a parent node.
        /// </summary>
        /// <param name="parentNode"></param>
        /// <returns></returns>
        TreeNode getFirstRealChild(TreeNode parentNode);

        /// <summary>
        /// Returns the last real child of a parent node.
        /// </summary>
        /// <param name="parentNode"></param>
        /// <returns></returns>
        TreeNode getLastRealChild(TreeNode parentNode);
    }
}
