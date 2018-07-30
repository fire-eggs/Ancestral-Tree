using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AncesTree.TreeLayout;
using AncesTree.TreeModel;
using GEDWrap;
using SharpGEDParser;
using SharpGEDParser.Model;

namespace AncesTree
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            Load += Settings_Load;
        }

        void Settings_Load(object sender, EventArgs e)
        {
            LoadSettings();
            lineStyleCombo1.Init(); // TODO automate!
            lineWeightCombo1.Init(); // TODO automate!
            MakeTree();
        }

        // use a TreeConfiguration instance for all these
        private Font _majorFont;
        private Font _minorFont;
        private bool _rootOnLeft;
        private int _generationGap;
        private int _spouseGap;
        private int _nodeGap;
        private DashStyle _boxStyle = DashStyle.Solid;
        private int _boxWeight = 1;
        private Color _boxColor = Color.Black;

        private Configuration.AlignmentInLevel _align;

        private TreeLayout<ITreeData> _tree;

        private void LoadSettings()
        {
            // TODO read from config file
            _majorFont = new Font("Times New Roman", 10);
            _minorFont = new Font("Times New Roman", 10);
            _rootOnLeft = false;
            _generationGap = 30;
            _spouseGap = 20;
            _nodeGap = 20;
            _align = Configuration.AlignmentInLevel.TowardsRoot;
        }

        private void MakeTree()
        {
            var ged = LoadGEDFromStream(gedcom);
            var patriarch = ged.PersonById("I1");

            var tree = TreeBuild.BuildTree(treePanel21, _majorFont, _minorFont, patriarch, _rootOnLeft);
            var configuration = new TreeConfiguration();
            configuration.GenerationGap = _generationGap;
            configuration.NodeGap = _nodeGap;
            configuration.SpouseGap = _spouseGap;
            configuration.RootLoc = _rootOnLeft ? Configuration.Location.Left
                                                : Configuration.Location.Top;
            configuration.Align = _align;

            configuration.NodeBorderColor = _boxColor;
            configuration.NodeBorderStyle = _boxStyle;
            configuration.NodeBorderWeight = _boxWeight;

            var nodeExtentProvider = new NodeExtents();
            var treeLayout = new TreeLayout<ITreeData>(tree, nodeExtentProvider, configuration);

            treePanel21.DrawFont = _majorFont;
            treePanel21.SpouseFont = _minorFont;
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
            "0 @F1@ FAM\n1 HUSB @I1@\n1 WIFE @I2@\n1 CHIL @I3@\n1 CHIL @I4@\n" +
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

        private void lineStyleCombo1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _boxStyle = (DashStyle) lineStyleCombo1.SelectedItem;
            MakeTree();
            treePanel21.Invalidate();
        }

        private void lineWeightCombo1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _boxWeight = (int)lineWeightCombo1.SelectedItem;
            MakeTree();
            treePanel21.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var dlg = new ColorDialog();
            dlg.Color = _boxColor;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _boxColor = dlg.Color;
                MakeTree();
                treePanel21.Invalidate();
            }
        }

    }
}
