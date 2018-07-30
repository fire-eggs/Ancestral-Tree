using System.Drawing;
using System.Drawing.Drawing2D;

namespace AncesTree.TreeModel
{
    class TreeConfiguration : DefaultConfiguration
    {
        public TreeConfiguration() : base (30, 20)
        {
            NodeBorderColor = Color.Black;
            NodeBorderStyle = DashStyle.Solid;
            NodeBorderWeight = 1;
        }

        public int GenerationGap
        {
            get { return (int)gapBetweenLevels; }
            set { gapBetweenLevels = value; }
        }

        public int NodeGap
        {
            get { return (int) gapBetweenNodes; }
            set { gapBetweenNodes = value; }
        }

        public Location RootLoc
        {
            get { return location; }
            set { location = value; }
        }

        public AlignmentInLevel Align
        {
            get { return alignmentInLevel; }
            set { alignmentInLevel = value; }
        }

        public int SpouseGap { get; set; }


        public Color NodeBorderColor { get; set; }
        public DashStyle NodeBorderStyle { get; set; }
        public int NodeBorderWeight { get; set; }
    }
}
