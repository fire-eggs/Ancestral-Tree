using System.Collections.Generic;
using AncesTree.TreeLayout;

namespace AncesTree.TreeModel
{
    /**
     * Provides a generic implementation for the {@link org.abego.treelayout.TreeForTreeLayout}
     * interface, applicable to any type of tree node.
     * <p>
     * It allows you to create a tree "from scratch", without creating any new
     * class.
     * <p>
     * To create a tree you must provide the root of the tree (see
     * {@link #DefaultTreeForTreeLayout(Object)}. Then you can incrementally
     * construct the tree by adding children to the root or other nodes of the tree
     * (see {@link #addChild(Object, Object)} and
     * {@link #addChildren(Object, Object...)}).
     * 
     * @author Udo Borkowski (ub@abego.org)
     * 
     * @param <TreeNode> Type of elements used as nodes in the tree
     */
    public class DefaultTreeForTreeLayout<TreeNode> : AbstractTreeForTreeLayout<TreeNode> where TreeNode : ITreeNode
            
    {

        private List<TreeNode> emptyList;

        private List<TreeNode> getEmptyList()
        {
            if (emptyList == null)
            {
                emptyList = new List<TreeNode>();
            }
            return emptyList;
        }

        private Dictionary<TreeNode, List<TreeNode>> childrenMap = new Dictionary<TreeNode, List<TreeNode>>();
        private Dictionary<TreeNode, TreeNode> parents = new Dictionary<TreeNode, TreeNode>();

        /**
         * Creates a new instance with a given node as the root
         * 
         * @param root
         *            the node to be used as the root.
         */
        public DefaultTreeForTreeLayout(TreeNode root) : base(root)
        {
        }

        public override TreeNode getParent(TreeNode node)
        {
            if (node == null)
                return default(TreeNode);

            try
            {
                TreeNode parent = default(TreeNode);
                parents.TryGetValue(node, out parent);
                return parent;
            }
            catch
            {
                return default(TreeNode);
            }

        }


        public override List<TreeNode> getChildrenList(TreeNode node)
        {
            List<TreeNode> result;
            return childrenMap.TryGetValue(node, out result) ? result : getEmptyList();

        }

        /**
         * 
         * @param node &nbsp;
         * @return true iff the node is in the tree
         */
        public bool hasNode(TreeNode node)
        {
            return object.ReferenceEquals(node, getRoot()) || parents.ContainsKey(node);
        }

        /**
         * @param parentNode
         *            [hasNode(parentNode)]
         * @param node
         *            [!hasNode(node)]
         */
        public void addChild(TreeNode parentNode, TreeNode node)
        {
            //checkArg(hasNode(parentNode), "parentNode is not in the tree");
            //checkArg(!hasNode(node), "node is already in the tree");
            if (!childrenMap.ContainsKey(parentNode))
            {
                List<TreeNode> list = new List<TreeNode>();
                childrenMap.Add(parentNode, list);
            }
            List<TreeNode> list1 = childrenMap[parentNode];
            list1.Add(node);
            parents.Add(node, parentNode);
        }

        public void addChildren(TreeNode parentNode, params TreeNode[] nodes)
        {
            foreach (TreeNode node in nodes)
            {
                addChild(parentNode, node);
            }
        }

    }
}
