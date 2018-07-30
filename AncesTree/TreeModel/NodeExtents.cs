using AncesTree.TreeLayout;

namespace AncesTree.TreeModel
{
    public class NodeExtents : NodeExtentProvider<ITreeData>
    {

        public double getWidth(ITreeData treeNode)
        {
            return treeNode.Wide;
        }

        public double getHeight(ITreeData treeNode)
        {
            return treeNode.High;
        }
    }

}
