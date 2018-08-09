using AncesTree.TreeModel;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AncesTree.Controls
{
    public partial class PenStyle : UserControl
    {
        public delegate void LineStyleChanged(object sender);

        [Browsable(true)]
        public event LineStyleChanged OnLineStyleChange;

        public PenStyle()
        {
            InitializeComponent();

            lineStyleCombo1.Init(); // TODO automate!
            lineWeightCombo1.Init(); // TODO automate!
        }

        public int LineWeight
        {
            get { return (int)lineWeightCombo1.SelectedItem; }
            set { lineWeightCombo1.SelectedItem = value; }
        }

        public DashStyle LineStyle
        {
            get { return (DashStyle)lineStyleCombo1.SelectedItem; }
            set { lineStyleCombo1.SelectedItem = value; }
        }

        public ColorValues LineColor
        {
            get { return colorButton1.Value; }
            set { colorButton1.Value = value; }
        }

        public Pen LinePen
        {
            get { return new Pen(LineColor.GetColor(), LineWeight) {DashStyle = LineStyle};}
        }

        private void Fire()
        {
            if (lineStyleCombo1.SelectedItem != null &&
                lineWeightCombo1.SelectedItem != null &&
                OnLineStyleChange != null)
                OnLineStyleChange(this);
        }

        private void colorButton1_OnColorChange(object sender, ColorValues newValue)
        {
            Fire();
        }

        private void lineStyleCombo1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fire();
        }

        private void lineWeightCombo1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fire();
        }

        public LineStyleValues Values
        {
            get
            {
                return new LineStyleValues
                {
                    LineColor = LineColor,
                    LineWeight = LineWeight,
                    LineStyle = LineStyle
                };
            }
            set
            {
                LineColor = value.LineColor;
                LineWeight = value.LineWeight;
                LineStyle = value.LineStyle;
            }
        }
    }

}
