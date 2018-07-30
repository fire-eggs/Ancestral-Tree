
using AncesTree.TreeLayout;

namespace AncesTree.TreeModel
{
    /**
     * Specify a {@link Configuration} through configurable parameters, or falling
     * back to some frequently used defaults.
     * 
     * @author Udo Borkowski (ub@abego.org)
     * 
     * 
     * @param <TreeNode> Type of elements used as nodes in the tree
     */
    public class DefaultConfiguration<TreeNode> : Configuration<TreeNode>
    {

        /**
         * Specifies the constants to be used for this Configuration.
         * 
         * @param gapBetweenLevels &nbsp;
         * @param gapBetweenNodes &nbsp;
         * @param location
         *            [default: {@link org.abego.treelayout.Configuration.Location#Top Top}]
         * @param alignmentInLevel
         *            [default: {@link org.abego.treelayout.Configuration.AlignmentInLevel#Center Center}]
         */
        public DefaultConfiguration(double gapBetweenLevels,
                double gapBetweenNodes, Location location,
                AlignmentInLevel alignmentInLevel)
        {
            //checkArg(gapBetweenLevels >= 0, "gapBetweenLevels must be >= 0");
            //checkArg(gapBetweenNodes >= 0, "gapBetweenNodes must be >= 0");

            this.gapBetweenLevels = gapBetweenLevels;
            this.gapBetweenNodes = gapBetweenNodes;
            this.location = location;
            this.alignmentInLevel = alignmentInLevel;
        }

        /**
         * Convenience constructor, using a default for the alignmentInLevel.
         * <p>
         * see
         * {@link #DefaultConfiguration(double, double, org.abego.treelayout.Configuration.Location, org.abego.treelayout.Configuration.AlignmentInLevel)}
         * </p>
         * @param gapBetweenLevels &nbsp;
         * @param gapBetweenNodes &nbsp;
         * @param location &nbsp;
         */
        public DefaultConfiguration(double gapBetweenLevels, double gapBetweenNodes, Location location) :
            this(gapBetweenLevels, gapBetweenNodes, location, AlignmentInLevel.Center)
        {
        }

        /**
         * Convenience constructor, using a default for the rootLocation and the
         * alignmentInLevel.
         * <p>
         * see
         * {@link #DefaultConfiguration(double, double,  org.abego.treelayout.Configuration.Location, org.abego.treelayout.Configuration.AlignmentInLevel)}
         * </p>
         * @param gapBetweenLevels &nbsp;
         * @param gapBetweenNodes &nbsp;
         */
        public DefaultConfiguration(double gapBetweenLevels, double gapBetweenNodes) :
            this(gapBetweenLevels, gapBetweenNodes, Location.Top, AlignmentInLevel.Center)
        {

        }

        // -----------------------------------------------------------------------
        // gapBetweenLevels

        private readonly double gapBetweenLevels;


        public override double getGapBetweenLevels(int nextLevel)
        {
            return gapBetweenLevels;
        }

        // -----------------------------------------------------------------------
        // gapBetweenNodes

        private readonly double gapBetweenNodes;


        public override double getGapBetweenNodes(TreeNode node1, TreeNode node2)
        {
            return gapBetweenNodes;
        }

        // -----------------------------------------------------------------------
        // location

        private readonly Location location;


        public override Location getRootLocation()
        {
            return location;
        }

        // -----------------------------------------------------------------------
        // alignmentInLevel

        private AlignmentInLevel alignmentInLevel;


        public override AlignmentInLevel getAlignmentInLevel()
        {
            return alignmentInLevel;
        }

    }
}
