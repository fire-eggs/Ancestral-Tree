using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public class LineStyleCombo : ComboBox
    {
        public LineStyleCombo()
        {
            this.DrawMode = DrawMode.OwnerDrawVariable;
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public void Init()
        {
            this.Items.Clear();
            this.Items.Add(DashStyle.Solid);
            this.Items.Add(DashStyle.Dash);
            this.Items.Add(DashStyle.Dot);
            this.Items.Add(DashStyle.DashDot);
            this.Items.Add(DashStyle.DashDotDot);
            this.SelectedIndex = 0;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index < 0) 
                return;
            if ((e.State & DrawItemState.Selected) != 0)
            {
                LinearGradientBrush brush = new LinearGradientBrush(e.Bounds, Color.FromArgb(255, 251, 237),
                    Color.FromArgb(255, 236, 181), LinearGradientMode.Vertical);
                Rectangle borderRect = new Rectangle(3, e.Bounds.Y, e.Bounds.Width - 5, e.Bounds.Height - 2);

                e.Graphics.FillRectangle(brush, borderRect);

                Pen pen = new Pen(Color.FromArgb(229, 195, 101));
                e.Graphics.DrawRectangle(pen, borderRect);
            }
            else
            {
                SolidBrush brush = new SolidBrush(Color.FromArgb(255, 255, 255));
                e.Graphics.FillRectangle(brush, e.Bounds);
            }


            Pen MyPen = new Pen(new SolidBrush(Color.Black), 1);
            MyPen.DashStyle = (DashStyle)(this.Items[e.Index]);
            e.Graphics.DrawLine(MyPen, e.Bounds.Left + 3, e.Bounds.Top + 8, e.Bounds.Right - 4, e.Bounds.Top + 8);
        }
    }
}

