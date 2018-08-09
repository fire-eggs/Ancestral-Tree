using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AncesTree.TreeModel;

namespace AncesTree.Controls
{
    public class ColorButton : Button
    {
        public delegate void ColorChanged(object sender, ColorValues newValue);

        [Browsable(true)]
        public event ColorChanged OnColorChange;

        public ColorButton()
        {
            base.Text = "";
            Click += ColorButton_Click;
        }

        void ColorButton_Click(object sender, EventArgs e)
        {
            var dlg = new ColorDialog();
            dlg.Color = Value.GetColor();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Value = new ColorValues {ARGB = dlg.Color.ToArgb()};
                if (OnColorChange != null)
                    OnColorChange(this, Value);
            }
        }

        public ColorValues Value
        {
            get { return new ColorValues {ARGB = BackColor.ToArgb()}; }
            set { BackColor = Color.FromArgb(value.ARGB); }
        }

        public override string Text
        {
            get { return ""; }
            set { }
        }
    }
}
