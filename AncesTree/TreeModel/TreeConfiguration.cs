using DrawAnce;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

// TODO consider splitting this into 'layout' and 'drawing' configurations? 
// Layout configuration is as defined by DefaultConfiguration.

namespace AncesTree.TreeModel
{
    public struct LineStyleValues
    {
        public DashStyle LineStyle;
        public int LineWeight;
        public ColorValues LineColor;

        public Pen GetPen()
        {
            return new Pen(LineColor.GetColor(), LineWeight) {DashStyle = LineStyle};
        }
    }

    public struct FontValues
    {
        public string Family;
        public float Size;

        public Font GetFont()
        {
            return new Font(Family, Size);
        }
    }

    public struct ColorValues
    {
        public Int32 ARGB;
        public Color GetColor() { return Color.FromArgb(ARGB);}
    }

    public enum HighlightStyles
    {
        None = 0,
        Line,
        Glow,
        ThreeD
    }

    public class TreeConfiguration : DefaultConfiguration
    {
        public TreeConfiguration() : base (0, 0)
        {
            Inited = false;
        }

        public bool Inited { get; set; }

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

        // These two properties modify the base class, therefore not trivial!
#pragma warning disable S2292 // Trivial properties should be auto-implemented
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

#pragma warning restore S2292 // Trivial properties should be auto-implemented

        public bool GenLines { get; set; }

        public int SpouseGap { get; set; }

        public FontValues MajorFont { get; set; }
        public FontValues MinorFont { get; set; }

        public LineStyleValues NodeBorder { get; set; }
        public LineStyleValues SpouseLine { get; set; }
        public LineStyleValues ChildLine { get; set; }
        public LineStyleValues MMargLine { get; set; }
        public LineStyleValues DuplLine { get; set; }

        public ColorValues MaleColor { get; set; }
        public ColorValues FemaleColor { get; set; }
        public ColorValues UnknownColor { get; set; }
        public ColorValues BackColor { get; set; }
        public ColorValues HighlightColor { get; set; }

        public HighlightStyles HighlightStyle { get; set; } = HighlightStyles.None;

        public bool RootOnLeft { get { return RootLoc == Location.Left; } }

        public int MaxDepth { get; set; }

        public static TreeConfiguration DefaultSettings()
        {
            var config = new TreeConfiguration();
            config.Inited = true;
            config.NodeGap = 20; // TODO gapBetweenNodes;
            config.GenerationGap = 30; // TODO gapBetweenLevels;
            config.Align = AlignmentInLevel.TowardsRoot;
            config.RootLoc = Location.Top;
            config.MajorFont = new FontValues {Family = "Times New Roman", Size = 10};
            config.MinorFont = new FontValues {Family = "Times New Roman", Size = 8};
            config.MaleColor = new ColorValues { ARGB = Color.PowderBlue.ToArgb()};
            config.FemaleColor = new ColorValues { ARGB = Color.Pink.ToArgb() };
            config.UnknownColor = new ColorValues { ARGB = Color.Plum.ToArgb() };
            config.NodeBorder = new LineStyleValues
            {
                LineColor = new ColorValues { ARGB = Color.Black.ToArgb() },
                LineStyle = DashStyle.Solid,
                LineWeight = 1
            };
            config.SpouseLine = new LineStyleValues
            {
                LineColor = new ColorValues { ARGB = Color.Black.ToArgb() },
                LineStyle = DashStyle.Solid,
                LineWeight = 1
            };
            config.ChildLine = new LineStyleValues
            {
                LineColor = new ColorValues { ARGB = Color.Black.ToArgb() },
                LineStyle = DashStyle.Solid,
                LineWeight = 1
            };
            config.MMargLine = new LineStyleValues
            {
                LineColor = new ColorValues { ARGB = Color.Coral.ToArgb() },
                LineWeight = 2,
                LineStyle = DashStyle.Dash
            };
            config.DuplLine = new LineStyleValues
            {
                LineColor = new ColorValues { ARGB = Color.CornflowerBlue.ToArgb() },
                LineWeight = 2,
                LineStyle = DashStyle.Dash
            };

            config.BackColor = new ColorValues { ARGB = Color.Bisque.ToArgb() };

            config.HighlightColor = new ColorValues { ARGB = Color.Red.ToArgb() };
            config.HighlightStyle = HighlightStyles.Line;

            config.MaxDepth = 10;
            return config;
        }

        public void Save()
        {
            Inited = true;
            AppSettings<TreeConfiguration>.Save(this, "TreeConfig.jsn");
        }

        public static TreeConfiguration LoadConfig()
        {
            try
            {
                var config = AppSettings<TreeConfiguration>.Load("TreeConfig.jsn");
                if (!config.Inited)
                    config = DefaultSettings();

                // 20180818 Enforce align as towardRoot: drawing code is currently
                // dependent on it.
                config.Align = AlignmentInLevel.TowardsRoot;

                return config;
            }
            catch (Exception)
            {
                return DefaultSettings();
            }
        }
    }
}
