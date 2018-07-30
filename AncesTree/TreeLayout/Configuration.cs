﻿namespace AncesTree.TreeLayout
{
    /**
     * Used to configure the tree layout algorithm.
     * <p>
     * Also see <a href="package-summary.html">this overview</a>.
     * </p>
     * @author Udo Borkowski (ub@abego.org)
     * 
     * @param <TreeNode> Type of elements used as nodes in the tree
     */
    public abstract class Configuration
    {

        // ------------------------------------------------------------------------
        // rootLocation

        /**
         * Identifies the sides of a rectangle (top, left, ...)
         */
        public enum Location
        {
            Top, Left, Bottom, Right
        }

        // ------------------------------------------------------------------------
        // alignmentInLevel

        /**
         * Returns the position of the root node in the diagram.
         * <p>
         * By default the root of the tree is located at the top of the diagram.
         * However one may also put it at the left, right or bottom of the diagram.
         * </p>
         * <table border="1">
         * <caption>Possible Root Positions</caption>
         * <tr>
         * <th>Top (Default)</th>
         * <th>Left</th>
         * <th>Right</th>
         * <th>Bottom</th>
         * </tr>
         * <tr>
         * <td style="padding:10px;"><img src="doc-files/TreeGraphView-Top.png" alt="Tree with root at top"></td>
         * <td style="padding:10px;"><img src="doc-files/TreeGraphView-Left.png" alt="Tree with root at left side"></td>
         * <td style="padding:10px;"><img src="doc-files/TreeGraphView-Right.png" alt="Tree with root at right side"></td>
         * <td style="padding:10px;"><img src="doc-files/TreeGraphView-Bottom.png" alt="Tree with root at bottom"></td>
         * </tr>
         * </table>
         * 
         * @return the position of the root node in the diagram
         */
        public abstract Location getRootLocation();

        /**
         * Possible alignments of a node within a level (centered, towards or away
         * from root)
         */
        public enum AlignmentInLevel
        {
            Center, TowardsRoot, AwayFromRoot
        }

        /**
         * Returns the alignment of "smaller" nodes within a level.
         * <p>
         * By default all nodes of one level are centered in the level. However one
         * may also align them "towards the root" or "away from the root". When the
         * root is located at the top this means the nodes are aligned "to the top
         * of the level" or "to the bottom of the level".
         * </p>
         * <table border="1">
         * <caption>Alignment in level when root is at the top</caption>
         * <tr>
         * <th>Center (Default)</th>
         * <th>TowardsRoot ("top of level")</th>
         * <th>AwayFromRoot ("bottom of level")</th>
         * </tr>
         * <tr>
         * <td style="padding:10px;"><img src="doc-files/TreeGraphView-Center.png" alt="Tree with root at top and nodes center aligned"></td>
         * <td style="padding:10px;"><img
         * src="doc-files/TreeGraphView-TowardsRoot.png" alt="Tree with root at top and nodes aligned to top"></td>
         * <td style="padding:10px;"><img
         * src="doc-files/TreeGraphView-AwayFromRoot.png" alt="Tree with root at top and nodes aligned to bottom"></td>
         * </tr>
         * </table>
         * <p>
         * Alignment in level when root is at the left:
         * </p>
         * <table border="1">
         * <caption>Table: Possible Alignments of Nodes (when root at left side)</caption>
         * <tr>
         * <th>Center (Default)</th>
         * <th>TowardsRoot ("left of level")</th>
         * <th>AwayFromRoot<br>
         * ("right of level")</th>
         * </tr>
         * <tr>
         * <td style="padding:10px;"><img
         * src="doc-files/TreeGraphView-Center-RootLeft.png" alt="Tree with root at left side and nodes center aligned"></td>
         * <td style="padding:10px;"><img
         * src="doc-files/TreeGraphView-TowardsRoot-RootLeft.png" alt="Tree with root at left side and nodes left aligned"></td>
         * <td style="padding:10px;"><img
         * src="doc-files/TreeGraphView-AwayFromRoot-RootLeft.png" alt="Tree with root at left side and nodes right aligned"></td>
         * </tr>
         * </table>
         * 
         * <p>
         * Of cause the alignment also works when the root is at the bottom or at
         * the right side.
         * </p>
         * 
         * @return the alignment of "smaller" nodes within a level
         */
        public abstract AlignmentInLevel getAlignmentInLevel();

        // ------------------------------------------------------------------------
        // gapBetweenLevels/Nodes

        /**
         * Returns the size of the gap between subsequent levels.
         * <p>
         * <img src="doc-files/gapBetweenLevels.png" alt="gapBetweenLevels Visualization">
         * 
         * @param nextLevel [nextLevel &gt; 0]
         * 
         * @return the size of the gap between level (nextLevel-1) and nextLevel
         *         [result &gt;= 0]
         */
        public abstract double getGapBetweenLevels(int nextLevel);

        /**
         * Returns the size of the minimal gap of nodes within a level.
         * <p>
         * In the layout there will be a gap of at least the returned size between
         * both given nodes.
         * <p>
         * <img src="doc-files/gapBetweenNodes.png" alt="gapBetweenNodes Visualization">
         * <p>
         * node1 and node2 are at the same level and are placed next to each other.
         * 
         * @param node1 &nbsp;
         * @param node2 &nbsp;
         * @return the minimal size of the gap between node1 and node2 [result &gt;= 0]
         */
        public abstract double getGapBetweenNodes(); //(TreeNode node1, TreeNode node2);
    }
}
