using AncesTree.Controls;
using AncesTree.TreeLayout;
using AncesTree.TreeModel;
using GEDWrap;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace AncesTree
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            Load += Settings_Load;
            button4.Click += button4_Click;
        }

        void Settings_Load(object sender, EventArgs e)
        {
            _loadComplete = false;
            LoadSettings();
            _loadComplete = true;
            MakeTree();
        }

        private bool _loadComplete;
        private TreeConfiguration _settings;

        private void LoadSettings()
        {
            // TODO read from config file
            _settings = new TreeConfiguration();
            _settings.GenerationGap = 30;
            _settings.NodeGap = 20;
            _settings.SpouseGap = 20;
            _settings.RootLoc = Configuration.Location.Top;
            _settings.Align = Configuration.AlignmentInLevel.TowardsRoot;

            _settings.NodeBorder = new LineStyleValues {
                                                            LineColor = Color.Black,
                                                            LineWeight = 1,
                                                            LineStyle = DashStyle.Solid
                                                        };
            _settings.SpouseLine = new LineStyleValues {
                                                            LineColor = Color.Black,
                                                            LineWeight = 1,
                                                            LineStyle = DashStyle.Solid
                                                        };
            _settings.ChildLine = new LineStyleValues {
                                                            LineColor = Color.Black,
                                                            LineWeight = 1,
                                                            LineStyle = DashStyle.Solid
                                                        };
            _settings.MMargLine = new LineStyleValues
            {
                LineColor = Color.Coral,
                LineWeight = 2,
                LineStyle = DashStyle.Dash
            };
            _settings.DuplLine = new LineStyleValues
            {
                LineColor = Color.CornflowerBlue,
                LineWeight = 2,
                LineStyle = DashStyle.Dash
            };
            boxPen.Values = _settings.NodeBorder;
            spousePen.Values = _settings.SpouseLine;
            childPen.Values = _settings.ChildLine;
            mmargPen.Values = _settings.MMargLine;
            duplLine.Values = _settings.DuplLine;

            _settings.MajorFont = new Font("Times New Roman", 10);
            _settings.MinorFont = new Font("Times New Roman", 10);

            _settings.BackColor = Color.Beige;
            _settings.MaleColor = Color.PowderBlue;
            _settings.FemaleColor = Color.Pink;
            _settings.UnknownColor = Color.Plum;

            btnMaleColor.Value = _settings.MaleColor;
            btnFemaleColor.Value = _settings.FemaleColor;
            btnUnknownColor.Value = _settings.UnknownColor;
            btnBackColor.Value = _settings.BackColor;
        }

        private void OnColorChange(ColorButton sender, Color newValue)
        {
            // TODO is member binding possible?
            // TODO just rebuild settings from controls?
            if (sender == btnMaleColor)
                _settings.MaleColor = btnMaleColor.Value;
            if (sender == btnFemaleColor)
                _settings.FemaleColor = btnFemaleColor.Value;
            if (sender == btnUnknownColor)
                _settings.UnknownColor = btnUnknownColor.Value;
            if (sender == btnBackColor)
                _settings.BackColor = btnBackColor.Value;
            Rebuild();
        }

        private Person _patriarch;

        private void MakeTree()
        {
            if (_patriarch == null)
            {
                var ged = LoadGEDFromStream(gedcom);
                _patriarch = ged.PersonById("I1");
            }

            var tree = TreeBuild.BuildTree(treePanel21, _settings, _patriarch);

            var nodeExtentProvider = new NodeExtents();
            var treeLayout = new TreeLayout<ITreeData>(tree, nodeExtentProvider, _settings);
            treePanel21.Boxen = treeLayout;
        }

        private const string gedcom = 
            "0 @I1@ INDI\n1 NAME John /Doe/\n1 SEX M\n" + 
            "0 @I2@ INDI\n1 NAME Mary /Moe/\n1 SEX F\n" + 
            "0 @I3@ INDI\n1 NAME Richard /Moe/\n1 SEX M\n" + 
            "0 @I4@ INDI\n1 NAME Janet /Moe/\n1 SEX F\n" +
            "0 @I5@ INDI\n1 NAME Margaret /Moe/\n1 SEX F\n"+
            "0 @I6@ INDI\n1 NAME Thomas /Toe/\n1 SEX M\n" +
            "0 @I7@ INDI\n1 NAME Billy /Boe/\n1 SEX U\n" +
            "0 @I8@ INDI\n1 NAME Clarissa /Moe/\n1 SEX F\n" +
            "0 @F1@ FAM\n1 HUSB @I1@\n1 WIFE @I2@\n1 CHIL @I3@\n1 CHIL @I8@\n1 CHIL @I4@\n" +
            "0 @F2@ FAM\n1 HUSB @I3@\n1 WIFE @I4@\n1 CHIL @I5@\n" +
            "0 @F3@ FAM\n1 HUSB @I6@\n1 WIFE @I5@\n" +
            "0 @F4@ FAM\n1 HUSB @I7@\n1 WIFE @I5@\n"
            ;

        public static Stream ToStream(string str)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(str));
        }

        protected Forest LoadGEDFromStream(string testString)
        {
            Forest f = new Forest();
            using (var stream = new StreamReader(ToStream(testString)))
            {
                f.LoadFromStream(stream);
            }
            return f;
        }

        private void Rebuild()
        {
            MakeTree();
            treePanel21.Invalidate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _settings.MajorFont = doFont(_settings.MajorFont);
            Rebuild();
        }

        private Font doFont(Font fontIn)
        {
            var dlg = new FontDialog();
            dlg.Font = fontIn;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                return dlg.Font;
            }
            return fontIn;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _settings.MinorFont = doFont(_settings.MinorFont);
            Rebuild();
        }

        private void penStyle_OnLineStyleChange(PenStyle sender)
        {
            if (!_loadComplete)
                return;
            if (sender == boxPen)
                _settings.NodeBorder = boxPen.Values;
            if (sender == spousePen)
                _settings.SpouseLine = spousePen.Values;
            if (sender == childPen)
                _settings.ChildLine = childPen.Values;

            Rebuild();
        }
    }
}
