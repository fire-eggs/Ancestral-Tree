using System;
using System.Collections.Generic;
using System.Windows;

namespace AncesTree.TreeLayout
{
    /**
     * Implements the actual tree layout algorithm.
     * <p>
     * The nodes with their readonly layout can be retrieved through
     * {@link #getNodeBounds()}.
     * </p>
     * See <a href="package-summary.html">this summary</a> to get an overview how to
     * use TreeLayout.
     * 
     * 
     * @author Udo Borkowski (ub@abego.org)
     *  
     * @param <TreeNode> Type of elements used as nodes in the tree
     */
    public class TreeLayout<TreeNode>
    {
        /*
         * Differences between this implementation and original algorithm
         * --------------------------------------------------------------
         * 
         * For easier reference the same names (or at least similar names) as in the
         * paper of Buchheim, J&uuml;nger, and Leipert are used in this
         * implementation. However in the external interface "first" and "last" are
         * used instead of "left most" and "right most". The implementation also
         * supports tree layouts with the root at the left (or right) side. In that
         * case using "left most" would refer to the "top" child, i.e. using "first"
         * is less confusing.
         * 
         * Also the y coordinate is not the level but directly refers the y
         * coordinate of a level, taking node's height and gapBetweenLevels into
         * account. When the root is at the left or right side the y coordinate
         * actually becomes an x coordinate.
         * 
         * Instead of just using a constant "distance" to calculate the position to
         * the next node we refer to the "size" (width or height) of the node and a
         * "gapBetweenNodes".
         */

        // ------------------------------------------------------------------------
        // tree

        private readonly TreeForTreeLayout<TreeNode> tree;

        /**
         * Returns the Tree the layout is created for.
         * 
         * @return the Tree the layout is created for
         */
        public TreeForTreeLayout<TreeNode> getTree()
        {
            return tree;
        }

        // ------------------------------------------------------------------------
        // nodeExtentProvider

        private readonly NodeExtentProvider<TreeNode> nodeExtentProvider;

        /**
         * Returns the {@link NodeExtentProvider} used by this {@link TreeLayout}.
         * 
         * @return the {@link NodeExtentProvider} used by this {@link TreeLayout}
         */
        public NodeExtentProvider<TreeNode> getNodeExtentProvider()
        {
            return nodeExtentProvider;
        }

        private double getNodeHeight(TreeNode node)
        {
            return nodeExtentProvider.getHeight(node);
        }

        private double getNodeWidth(TreeNode node)
        {
            return nodeExtentProvider.getWidth(node);
        }

        private double getWidthOrHeightOfNode(TreeNode treeNode, bool returnWidth)
        {
            return returnWidth ? getNodeWidth(treeNode) : getNodeHeight(treeNode);
        }

        /**
         * When the level changes in Y-axis (i.e. root location Top or Bottom) the
         * height of a node is its thickness, otherwise the node's width is its
         * thickness.
         * <p>
         * The thickness of a node is used when calculating the locations of the
         * levels.
         * 
         * @param treeNode
         * @return
         */
        private double getNodeThickness(TreeNode treeNode)
        {
            return getWidthOrHeightOfNode(treeNode, !isLevelChangeInYAxis());
        }

        /**
         * When the level changes in Y-axis (i.e. root location Top or Bottom) the
         * width of a node is its size, otherwise the node's height is its size.
         * <p>
         * The size of a node is used when calculating the distance between two
         * nodes.
         * 
         * @param treeNode
         * @return
         */
        private double getNodeSize(TreeNode treeNode)
        {
            return getWidthOrHeightOfNode(treeNode, isLevelChangeInYAxis());
        }

        // ------------------------------------------------------------------------
        // configuration

        private readonly Configuration<TreeNode> configuration;

        /**
         * Returns the Configuration used by this {@link TreeLayout}.
         * 
         * @return the Configuration used by this {@link TreeLayout}
         */
        public Configuration<TreeNode> getConfiguration()
        {
            return configuration;
        }

        private bool isLevelChangeInYAxis()
        {
            Configuration<TreeNode>.Location rootLocation = configuration.getRootLocation();
            return rootLocation == Configuration<TreeNode>.Location.Top || rootLocation == Configuration<TreeNode>.Location.Bottom;
        }

        private int getLevelChangeSign()
        {
            Configuration<TreeNode>.Location rootLocation = configuration.getRootLocation();
            return rootLocation == Configuration<TreeNode>.Location.Bottom
                    || rootLocation == Configuration<TreeNode>.Location.Right ? -1 : 1;
        }

        // ------------------------------------------------------------------------
        // bounds

        private double boundsLeft = Double.MaxValue;
        private double boundsRight = Double.MinValue;
        private double boundsTop = Double.MaxValue;
        private double boundsBottom = Double.MinValue;

        private void updateBounds(TreeNode node, double centerX, double centerY)
        {
            double width = getNodeWidth(node);
            double height = getNodeHeight(node);
            double left = centerX - width / 2;
            double right = centerX + width / 2;
            double top = centerY - height / 2;
            double bottom = centerY + height / 2;
            if (boundsLeft > left)
            {
                boundsLeft = left;
            }
            if (boundsRight < right)
            {
                boundsRight = right;
            }
            if (boundsTop > top)
            {
                boundsTop = top;
            }
            if (boundsBottom < bottom)
            {
                boundsBottom = bottom;
            }
        }


        /**
         * Returns the bounds of the tree layout.
         * <p>
         * The bounds of a TreeLayout is the smallest rectangle containing the
         * bounds of all nodes in the layout. It always starts at (0,0).
         * 
         * @return the bounds of the tree layout
         */
        public Rect getBounds()
        {
            return new Rect(0, 0, boundsRight - boundsLeft,
                 boundsBottom - boundsTop);

        }

        // ------------------------------------------------------------------------
        // size of level

        private readonly List<Double> sizeOfLevel = new List<Double>();

        private void calcSizeOfLevels(TreeNode node, int level)
        {
            double oldSize;
            if (sizeOfLevel.Count <= level)
            {
                sizeOfLevel.Add(0d);
                oldSize = 0;
            }
            else
            {
                oldSize = sizeOfLevel[level];
            }

            double size = getNodeThickness(node);
            // size = nodeExtentProvider.getHeight(node);
            if (oldSize < size)
            {
                sizeOfLevel[level] = size;
            }

            if (!tree.isLeaf(node))
            {
                foreach (TreeNode child in tree.getChildren(node))
                {
                    calcSizeOfLevels(child, level + 1);
                }
            }
        }

        /**
         * Returns the number of levels of the tree.
         * 
         * @return [level &gt; 0]
         */
        public int getLevelCount()
        {
            return sizeOfLevel.Count;
        }

        /**
         * Returns the size of a level.
         * <p>
         * When the root is located at the top or bottom the size of a level is the
         * maximal height of the nodes of that level. When the root is located at
         * the left or right the size of a level is the maximal width of the nodes
         * of that level.
         * 
         * @param level &nbsp;
         * @return the size of the level [level &gt;= 0 &amp;&amp; level &lt; levelCount]
         */
        public double getSizeOfLevel(int level)
        {
            //checkArg(level >= 0, "level must be >= 0");
            //checkArg(level < getLevelCount(), "level must be < levelCount");

            return sizeOfLevel[level];
        }

        // ------------------------------------------------------------------------
        // NormalizedPosition

        /**
         * The algorithm calculates the position starting with the root at 0. I.e.
         * the left children will get negative positions. However we want the result
         * to be normalized to (0,0).
         * <p>
         * {@link NormalizedPosition} will normalize the position (given relative to
         * the root position), taking the current bounds into account. This way the
         * left most node bounds will start at x = 0, the top most node bounds at y
         * = 0.
         */
        private class NormalizedPosition
        {

            private TreeLayout<TreeNode> treeLayout;
            private double x_relativeToRoot;
            private double y_relativeToRoot;

            public NormalizedPosition(TreeLayout<TreeNode> treeLayout, double x_relativeToRoot, double y_relativeToRoot)
            {
                setLocation(x_relativeToRoot, y_relativeToRoot);
                this.treeLayout = treeLayout;
            }

            public double getX()
            {
                return x_relativeToRoot - treeLayout.boundsLeft;
            }


            public double getY()
            {
                return y_relativeToRoot - treeLayout.boundsTop;
            }

            // never called from outside
            public void setLocation(double x_relativeToRoot, double y_relativeToRoot)
            {
                this.x_relativeToRoot = x_relativeToRoot;
                this.y_relativeToRoot = y_relativeToRoot;
            }
        }

        // ------------------------------------------------------------------------
        // The Algorithm

        private readonly bool useIdentity;

        private readonly Dictionary<TreeNode, double> mod;
        private readonly Dictionary<TreeNode, TreeNode> thread;
        private readonly Dictionary<TreeNode, double> prelim;
        private readonly Dictionary<TreeNode, double> change;
        private readonly Dictionary<TreeNode, double> shift;
        private readonly Dictionary<TreeNode, TreeNode> ancestor1;
        private readonly Dictionary<TreeNode, int> number;
        private readonly Dictionary<TreeNode, NormalizedPosition> positions;

        private double getMod(TreeNode node)
        {
            if (mod.ContainsKey(node))
            {
                return mod[node];
            }
            return 0;
        }

        private void setMod(TreeNode node, double d)
        {
            if (mod.ContainsKey(node))
            {
                mod[node] = d;
            }
            else
            {
                mod.Add(node, d);
            }

        }

        private TreeNode getThread(TreeNode node)
        {
            if (thread.ContainsKey(node))
            {
                return thread[node];
            }
            return default(TreeNode);
        }

        private void setThread(TreeNode node, TreeNode thread)
        {
            if (this.thread.ContainsKey(node))
            {
                this.thread[node] = thread;
            }
            else
            {
                this.thread.Add(node, thread);
            }

        }

        private TreeNode getAncestor(TreeNode node)
        {
            if (ancestor1.ContainsKey(node))
            {
                return ancestor1[node];
            }
            return default(TreeNode);
        }

        private void setAncestor(TreeNode node, TreeNode ancestor)
        {
            if (this.ancestor1.ContainsKey(node))
            {
                this.ancestor1[node] = ancestor;
            }
            else
            {
                this.ancestor1.Add(node, ancestor);
            }
        }

        private double getPrelim(TreeNode node)
        {
            if (prelim.ContainsKey(node))
            {
                return prelim[node];
            }
            return 0;
        }

        private void setPrelim(TreeNode node, double d)
        {
            if (prelim.ContainsKey(node))
            {
                prelim[node] = d;
            }
            else
            {
                prelim.Add(node, d);
            }

        }

        private double getChange(TreeNode node)
        {
            if (change.ContainsKey(node))
            {
                return change[node];
            }
            return 0;
        }

        private void setChange(TreeNode node, double d)
        {
            if (change.ContainsKey(node))
            {
                change[node] = d;
            }
            else
            {
                change.Add(node, d);
            }
        }

        private double getShift(TreeNode node)
        {
            if (shift.ContainsKey(node))
            {
                return shift[node];
            }
            return 0;
        }

        private void setShift(TreeNode node, double d)
        {
            if (shift.ContainsKey(node))
            {
                shift[node] = d;
            }
            else
            {
                shift.Add(node, d);
            }

        }

        /**
         * The distance of two nodes is the distance of the centers of both nodes.
         * <p>
         * I.e. the distance includes the gap between the nodes and half of the
         * sizes of the nodes.
         * 
         * @param v
         * @param w
         * @return the distance between node v and w
         */
        private double getDistance(TreeNode v, TreeNode w)
        {
            double sizeOfNodes = getNodeSize(v) + getNodeSize(w);

            double distance = sizeOfNodes / 2
                    + configuration.getGapBetweenNodes(v, w);
            return distance;
        }

        private TreeNode nextLeft(TreeNode v)
        {
            return tree.isLeaf(v) ? getThread(v) : tree.getFirstChild(v);
        }

        private TreeNode nextRight(TreeNode v)
        {
            return tree.isLeaf(v) ? getThread(v) : tree.getLastChild(v);
        }

        /**
         * 
         * @param node
         *            [tree.isChildOfParent(node, parentNode)]
         * @param parentNode
         *            parent of node
         * @return
         */
        private int getNumber(TreeNode node, TreeNode parentNode)
        {
            if (!shift.ContainsKey(node))
            {
                int i = 1;
                foreach (TreeNode child in tree.getChildren(parentNode))
                {
                    if (number.ContainsKey(child))
                    {
                        number[child] = i++;
                    }
                    else
                    {
                        number.Add(child, i++);
                    }
                }


            }
            return number[node];
        }

        /**
         * 
         * @param vIMinus
         * @param v
         * @param parentOfV
         * @param defaultAncestor
         * @return the greatest distinct ancestor of vIMinus and its right neighbor
         *         v
         */
        private TreeNode ancestor(TreeNode vIMinus, TreeNode v, TreeNode parentOfV,
                TreeNode defaultAncestor)
        {
            TreeNode ancestor = getAncestor(vIMinus);

            // when the ancestor of vIMinus is a sibling of v (i.e. has the same
            // parent as v) it is also the greatest distinct ancestor vIMinus and
            // v. Otherwise it is the defaultAncestor

            return tree.isChildOfParent(ancestor, parentOfV) ? ancestor
                    : defaultAncestor;
        }

        private void moveSubtree(TreeNode wMinus, TreeNode wPlus, TreeNode parent,
                double shift)
        {

            int subtrees = getNumber(wPlus, parent) - getNumber(wMinus, parent);
            setChange(wPlus, getChange(wPlus) - shift / subtrees);
            setShift(wPlus, getShift(wPlus) + shift);
            setChange(wMinus, getChange(wMinus) + shift / subtrees);
            setPrelim(wPlus, getPrelim(wPlus) + shift);
            setMod(wPlus, getMod(wPlus) + shift);
        }

        /**
         * In difference to the original algorithm we also pass in the leftSibling
         * and the parent of v.
         * <p>
         * <b>Why adding the parameter 'parent of v' (parentOfV) ?</b>
         * <p>
         * In this method we need access to the parent of v. Not every tree
         * implementation may support efficient (i.e. constant time) access to it.
         * On the other hand the (only) caller of this method can provide this
         * information with only constant extra time.
         * <p>
         * Also we need access to the "left most sibling" of v. Not every tree
         * implementation may support efficient (i.e. constant time) access to it.
         * On the other hand the "left most sibling" of v is also the "first child"
         * of the parent of v. The first child of a parent node we can get in
         * constant time. As we got the parent of v we can so also get the
         * "left most sibling" of v in constant time.
         * <p>
         * <b>Why adding the parameter 'leftSibling' ?</b>
         * <p>
         * In this method we need access to the "left sibling" of v. Not every tree
         * implementation may support efficient (i.e. constant time) access to it.
         * However it is easy for the caller of this method to provide this
         * information with only constant extra time.
         * <p>
         * <p>
         * <p>
         * In addition these extra parameters avoid the need for
         * {@link TreeForTreeLayout} to include extra methods "getParent",
         * "getLeftSibling", or "getLeftMostSibling". This keeps the interface
         * {@link TreeForTreeLayout} small and avoids redundant implementations.
         * 
         * @param v
         * @param defaultAncestor
         * @param leftSibling
         *            [nullable] the left sibling v, if there is any
         * @param parentOfV
         *            the parent of v
         * @return the (possibly changes) defaultAncestor
         */
        private TreeNode apportion(TreeNode v, TreeNode defaultAncestor,
                TreeNode leftSibling, TreeNode parentOfV)
        {
            TreeNode w = leftSibling;
            if (w == null)
            {
                // v has no left sibling
                return defaultAncestor;
            }
            // v has left sibling w

            // The following variables "v..." are used to traverse the contours to
            // the subtrees. "Minus" refers to the left, "Plus" to the right
            // subtree. "I" refers to the "inside" and "O" to the outside contour.
            TreeNode vOPlus = v;
            TreeNode vIPlus = v;
            TreeNode vIMinus = w;
            // get leftmost sibling of vIPlus, i.e. get the leftmost sibling of
            // v, i.e. the leftmost child of the parent of v (which is passed
            // in)
            TreeNode vOMinus = tree.getFirstChild(parentOfV);

            Double sIPlus = getMod(vIPlus);
            Double sOPlus = getMod(vOPlus);
            Double sIMinus = getMod(vIMinus);
            Double sOMinus = getMod(vOMinus);

            TreeNode nextRightVIMinus = nextRight(vIMinus);
            TreeNode nextLeftVIPlus = nextLeft(vIPlus);

            while (nextRightVIMinus != null && nextLeftVIPlus != null)
            {
                vIMinus = nextRightVIMinus;
                vIPlus = nextLeftVIPlus;
                vOMinus = nextLeft(vOMinus);
                vOPlus = nextRight(vOPlus);
                setAncestor(vOPlus, v);
                double shift = (getPrelim(vIMinus) + sIMinus)
                        - (getPrelim(vIPlus) + sIPlus)
                        + getDistance(vIMinus, vIPlus);

                if (shift > 0)
                {
                    moveSubtree(ancestor(vIMinus, v, parentOfV, defaultAncestor),
                            v, parentOfV, shift);
                    sIPlus = sIPlus + shift;
                    sOPlus = sOPlus + shift;
                }
                sIMinus = sIMinus + getMod(vIMinus);
                sIPlus = sIPlus + getMod(vIPlus);
                sOMinus = sOMinus + getMod(vOMinus);
                sOPlus = sOPlus + getMod(vOPlus);

                nextRightVIMinus = nextRight(vIMinus);
                nextLeftVIPlus = nextLeft(vIPlus);
            }

            if (nextRightVIMinus != null && nextRight(vOPlus) == null)
            {
                setThread(vOPlus, nextRightVIMinus);
                setMod(vOPlus, getMod(vOPlus) + sIMinus - sOPlus);
            }

            if (nextLeftVIPlus != null && nextLeft(vOMinus) == null)
            {
                setThread(vOMinus, nextLeftVIPlus);
                setMod(vOMinus, getMod(vOMinus) + sIPlus - sOMinus);
                defaultAncestor = v;
            }
            return defaultAncestor;
        }

        /**
         * 
         * @param v
         *            [!tree.isLeaf(v)]
         */
        private void executeShifts(TreeNode v)
        {
            double shift = 0;
            double change = 0;
            foreach (TreeNode w in tree.getChildrenReverse(v))
            {
                change = change + getChange(w);
                setPrelim(w, getPrelim(w) + shift);
                setMod(w, getMod(w) + shift);
                shift = shift + getShift(w) + change;
            }
        }

        /**
         * In difference to the original algorithm we also pass in the leftSibling
         * (see {@link #apportion(Object, Object, Object, Object)} for a
         * motivation).
         * 
         * @param v
         * @param leftSibling
         *            [nullable] the left sibling v, if there is any
         */
        private void firstWalk(TreeNode v, TreeNode leftSibling)
        {
            if (tree.isLeaf(v))
            {
                // No need to set prelim(v) to 0 as the getter takes care of this.

                TreeNode w = leftSibling;
                if (w != null)
                {
                    // v has left sibling

                    setPrelim(v, getPrelim(w) + getDistance(v, w));
                }

            }
            else
            {
                // v is not a leaf

                TreeNode defaultAncestor = tree.getFirstChild(v);
                TreeNode previousChild = default(TreeNode);
                foreach (TreeNode w1 in tree.getChildren(v))
                {
                    firstWalk(w1, previousChild);
                    defaultAncestor = apportion(w1, defaultAncestor, previousChild, v);
                    previousChild = w1;
                }
                executeShifts(v);

                double midpoint0 = (getPrelim(tree.getFirstChild(v)) + getPrelim(tree.getLastChild(v))) / 2.0;

                double midpoint = (getPrelim(tree.getFirstRealChild(v)) + getPrelim(tree.getLastRealChild(v))) / 2.0;

                Console.WriteLine("Mid0:{0} Mid1:{1}", midpoint0, midpoint);

                TreeNode w = leftSibling;
                if (w != null)
                {
                    // v has left sibling

                    setPrelim(v, getPrelim(w) + getDistance(v, w));
                    setMod(v, getPrelim(v) - midpoint);

                }
                else
                {
                    // v has no left sibling

                    setPrelim(v, midpoint);
                }
            }
        }

        /**
         * In difference to the original algorithm we also pass in extra level
         * information.
         * 
         * @param v
         * @param m
         * @param level
         * @param levelStart
         */
        private void secondWalk(TreeNode v, double m, int level, double levelStart)
        {
            // construct the position from the prelim and the level information

            // The rootLocation affects the way how x and y are changed and in what
            // direction.
            double levelChangeSign = getLevelChangeSign();
            bool levelChangeOnYAxis = isLevelChangeInYAxis();
            double levelSize = getSizeOfLevel(level);

            double x = getPrelim(v) + m;

            double y;
            Configuration<TreeNode>.AlignmentInLevel alignment = configuration.getAlignmentInLevel();
            if (alignment == Configuration<TreeNode>.AlignmentInLevel.Center)
            {
                y = levelStart + levelChangeSign * (levelSize / 2);
            }
            else if (alignment == Configuration<TreeNode>.AlignmentInLevel.TowardsRoot)
            {
                y = levelStart + levelChangeSign * (getNodeThickness(v) / 2);
            }
            else
            {
                y = levelStart + levelSize - levelChangeSign
                        * (getNodeThickness(v) / 2);
            }

            if (!levelChangeOnYAxis)
            {
                double t = x;
                x = y;
                y = t;
            }

            positions.Add(v, new NormalizedPosition(this, x, y));

            // update the bounds
            updateBounds(v, x, y);

            // recurse
            if (!tree.isLeaf(v))
            {
                double nextLevelStart = levelStart
                        + (levelSize + configuration.getGapBetweenLevels(level + 1))
                        * levelChangeSign;
                foreach (TreeNode w in tree.getChildren(v))
                {
                    secondWalk(w, m + getMod(v), level + 1, nextLevelStart);
                }
            }
        }

        // ------------------------------------------------------------------------
        // nodeBounds

        private Dictionary<TreeNode, Rect> nodeBounds;

        /**
         * Returns the layout of the tree nodes by mapping each node of the tree to
         * its bounds (position and size).
         * <p>
         * For each rectangle x and y will be &gt;= 0. At least one rectangle will have
         * an x == 0 and at least one rectangle will have an y == 0.
         * 
         * @return maps each node of the tree to its bounds (position and size).
         */
        public Dictionary<TreeNode, Rect> getNodeBounds()
        {
            if (nodeBounds == null)
            {
                nodeBounds = new Dictionary<TreeNode, Rect>();
                foreach (TreeNode treeNode in positions.Keys)
                {
                    NormalizedPosition pos = positions[treeNode];
                    double w = getNodeWidth(treeNode);
                    double h = getNodeHeight(treeNode);
                    double x = pos.getX() - w / 2;
                    double y = pos.getY() - h / 2;
                    var bounds = new Rect(x, y, w, h);
                    nodeBounds.Add(treeNode, bounds);
                }
            }
            return nodeBounds;
        }

        // ------------------------------------------------------------------------
        // constructor

        /**
         * Creates a TreeLayout for a given tree.
         * <p>
         * In addition to the tree the {@link NodeExtentProvider} and the
         * {@link Configuration} must be given.
         * 
         * @param tree &nbsp;
         * @param nodeExtentProvider &nbsp;
         * @param configuration &nbsp;
         * @param useIdentity
         *            [default: false] when true, identity ("==") is used instead of
         *            equality ("equals(...)") when checking nodes. Within a tree
         *            each node must only exist once (using this check).
         */
        public TreeLayout(TreeForTreeLayout<TreeNode> tree,
                NodeExtentProvider<TreeNode> nodeExtentProvider,
                Configuration<TreeNode> configuration, bool useIdentity)
        {
            this.tree = tree;
            this.nodeExtentProvider = nodeExtentProvider;
            this.configuration = configuration;
            this.useIdentity = useIdentity;

            if (this.useIdentity)
            {
                this.mod = new Dictionary<TreeNode, Double>();
                this.thread = new Dictionary<TreeNode, TreeNode>();
                this.prelim = new Dictionary<TreeNode, Double>();
                this.change = new Dictionary<TreeNode, Double>();
                this.shift = new Dictionary<TreeNode, Double>();
                this.ancestor1 = new Dictionary<TreeNode, TreeNode>();
                this.number = new Dictionary<TreeNode, int>();
                this.positions = new Dictionary<TreeNode, NormalizedPosition>();
            }
            else
            {
                this.mod = new Dictionary<TreeNode, Double>();
                this.thread = new Dictionary<TreeNode, TreeNode>();
                this.prelim = new Dictionary<TreeNode, Double>();
                this.change = new Dictionary<TreeNode, Double>();
                this.shift = new Dictionary<TreeNode, Double>();
                this.ancestor1 = new Dictionary<TreeNode, TreeNode>();
                this.number = new Dictionary<TreeNode, int>();
                this.positions = new Dictionary<TreeNode, NormalizedPosition>();
            }

            // No need to explicitly set mod, thread and ancestor as their getters
            // are taking care of the initial values. This avoids a full tree walk
            // through and saves some memory as no entries are added for
            // "initial values".

            TreeNode r = tree.getRoot();
            firstWalk(r, default(TreeNode));
            calcSizeOfLevels(r, 0);
            secondWalk(r, -getPrelim(r), 0, 0);
        }

        public TreeLayout(TreeForTreeLayout<TreeNode> tree,
                NodeExtentProvider<TreeNode> nodeExtentProvider,
                Configuration<TreeNode> configuration) : this(tree, nodeExtentProvider, configuration, false)
        {

        }

        // ------------------------------------------------------------------------
        // checkTree

        private void addUniqueNodes(Dictionary<TreeNode, TreeNode> nodes, TreeNode newNode)
        {
            if (nodes.ContainsKey(newNode))
            {
                throw new SystemException(String.Format("Node used more than once in tree: %s", newNode));
            }
            nodes.Add(newNode, newNode);
            foreach (TreeNode n in tree.getChildren(newNode))
            {
                addUniqueNodes(nodes, n);
            }
        }

        /**
         * Check if the tree is a "valid" tree.
         * <p>
         * Typically you will use this method during development when you get an
         * unexpected layout from your trees.
         * <p>
         * The following checks are performed:
         * <ul>
         * <li>Each node must only occur once in the tree.</li>
         * </ul>
         */
        public void checkTree()
        {
            Dictionary<TreeNode, TreeNode> nodes = this.useIdentity ? new Dictionary<TreeNode, TreeNode>()
                    : new Dictionary<TreeNode, TreeNode>();

            // Traverse the tree and check if each node is only used once.
            addUniqueNodes(nodes, tree.getRoot());
        }

        // ------------------------------------------------------------------------
        // dumpTree

        //private void dumpTree(StreamWriter output, TreeNode node, int indent,
        //        DumpConfiguration dumpConfiguration)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    for (int i = 0; i < indent; i++)
        //    {
        //        sb.Append(dumpConfiguration.indent);
        //    }

        //    if (dumpConfiguration.includeObjectToString)
        //    {
        //        sb.Append("[");
        //        sb.Append(node.GetType().Name + "@"
        //                + node.GetHashCode().ToString("X"));
        //        if (node.GetHashCode() != node.GetHashCode())
        //        {
        //            sb.Append("/identityHashCode:");
        //            sb.Append(node.GetHashCode().ToString("X"));
        //        }
        //        sb.Append("]");
        //    }

        //    sb.Append(StringUtil.quote(node != null ? node.ToString() : null));

        //    if (dumpConfiguration.includeNodeSize)
        //    {
        //        sb.Append(" (size: ");
        //        sb.Append(getNodeWidth(node));
        //        sb.Append("x");
        //        sb.Append(getNodeHeight(node));
        //        sb.Append(")");
        //    }

        //    output.WriteLine(sb.ToString());

        //    foreach (TreeNode n in tree.getChildren(node))
        //    {
        //        dumpTree(output, n, indent + 1, dumpConfiguration);
        //    }
        //}

        //public class DumpConfiguration
        //{
        //    /**
        //     * The text used to indent the output per level.
        //     */
        //    public readonly String indent;
        //    /**
        //     * When true the dump also includes the size of each node, otherwise
        //     * not.
        //     */
        //    public readonly bool includeNodeSize;
        //    /**
        //     * When true, the text as returned by {@link Object#toString()}, is
        //     * included in the dump, in addition to the text returned by the
        //     * possibly overridden toString method of the node. When the hashCode
        //     * method is overridden the output will also include the
        //     * "identityHashCode".
        //     */
        //    public readonly bool includeObjectToString;

        //    /**
        //     * 
        //     * @param indent [default: "    "]
        //     * @param includeNodeSize [default: false]
        //     * @param includePointer [default: false]
        //     */
        //    public DumpConfiguration(String indent, bool includeNodeSize,
        //            bool includePointer)
        //    {
        //        this.indent = indent;
        //        this.includeNodeSize = includeNodeSize;
        //        this.includeObjectToString = includePointer;
        //    }

        //    public DumpConfiguration() : this("    ", false, false)
        //    {

        //    }
        //}

        /**
         * Prints a dump of the tree to the given printStream, using the node's
         * "toString" method.
         * 
         * @param printStream &nbsp;
         * @param dumpConfiguration
         *            [default: new DumpConfiguration()]
         */
        //public void dumpTree(StreamWriter printStream, DumpConfiguration dumpConfiguration)
        //{
        //    dumpTree(printStream, tree.getRoot(), 0, dumpConfiguration);
        //}

        //public void dumpTree(StreamWriter printStream)
        //{
        //    dumpTree(printStream, new DumpConfiguration());
        //}
    }
}
