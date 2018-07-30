using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using AncesTree.TreeLayout;

namespace AncesTree.TreeModel
{
    public struct LineStyleValues
    {
        public DashStyle LineStyle;
        public int LineWeight;
        public Color LineColor;

        public Pen GetPen()
        {
            return new Pen(LineColor, LineWeight) {DashStyle = LineStyle};
        }
    }

    public class TreeConfiguration : DefaultConfiguration, IDisposable
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

        public LineStyleValues NodeBorder { get; set; }
        public LineStyleValues SpouseLine { get; set; }
        public LineStyleValues ChildLine { get; set; }
        public LineStyleValues MMargLine { get; set; }
        public LineStyleValues DuplLine { get; set; }


        public Color MaleColor { get; set; }
        public Color FemaleColor { get; set; }
        public Color UnknownColor { get; set; }
        public Color BackColor { get; set; }
        public bool RootOnLeft { get { return RootLoc == Location.Left; } }

        public static TreeConfiguration DefaultSettings()
        {
            var config = new TreeConfiguration();
            config.NodeGap = 20; // TODO gapBetweenNodes;
            config.GenerationGap = 30; // TODO gapBetweenLevels;
            config.Align = AlignmentInLevel.TowardsRoot;
            config.RootLoc = Location.Top;
            config.MajorFont = new Font("Times New Roman", 10);
            config.MinorFont = new Font("Times New Roman", 8);
            config.MaleColor = Color.PowderBlue;
            config.FemaleColor = Color.Pink;
            config.UnknownColor = Color.Plum;
            config.NodeBorder = new LineStyleValues
            {
                LineColor = Color.Black,
                LineStyle = DashStyle.Solid,
                LineWeight = 1
            };
            config.SpouseLine = new LineStyleValues
            {
                LineColor = Color.Black,
                LineStyle = DashStyle.Solid,
                LineWeight = 1
            };
            config.ChildLine = new LineStyleValues
            {
                LineColor = Color.Black,
                LineStyle = DashStyle.Solid,
                LineWeight = 1
            };
            config.MMargLine = new LineStyleValues
            {
                LineColor = Color.Coral,
                LineWeight = 2,
                LineStyle = DashStyle.Dash
            };
            config.DuplLine = new LineStyleValues
            {
                LineColor = Color.CornflowerBlue,
                LineWeight = 2,
                LineStyle = DashStyle.Dash
            };

            config.BackColor = Color.Bisque;
            return config;
        }


        public void Dispose()
        {
            MajorFont.Dispose();
            MinorFont.Dispose();
        }
    }
}
