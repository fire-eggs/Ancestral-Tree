using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AncesTree.Controls
{
    public class ColorButton : Button
    {
        public delegate void ColorChanged(ColorButton sender, Color newValue);

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
            dlg.Color = Value;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Value = dlg.Color;
                if (OnColorChange != null)
                    OnColorChange(this, Value);
            }
        }

        public Color Value
        {
            get { return BackColor; }
            set { BackColor = value; }
        }

        public override string Text
        {
            get { return ""; }
            set { }
        }
    }
}
