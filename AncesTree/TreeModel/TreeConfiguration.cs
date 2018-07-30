using System.Drawing;
using System.Drawing.Drawing2D;

namespace AncesTree.TreeModel
{
    public class TreeConfiguration : DefaultConfiguration
    {
        public TreeConfiguration() : base (0, 0)
        {
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

        public Font MajorFont { get; set; }
        public Font MinorFont { get; set; }


        public Color NodeBorderColor { get; set; }
        public DashStyle NodeBorderStyle { get; set; }
        public int NodeBorderWeight { get; set; }

        public Color MaleColor { get; set; }
        public Color FemaleColor { get; set; }
        public Color UnknownColor { get; set; }
        public Color BackColor { get; set; }
        public bool RootOnLeft { get { return RootLoc == Location.Left; } }
    }
}
