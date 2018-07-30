using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using AncesTree.TreeLayout;
using AncesTree.TreeModel;

namespace AncesTree
{
    public class TreePanel : Panel
    {
        public TreePanel()
        {
            DoubleBuffered = true;
        }

        public Font DrawFont { private get; set; }

        private TreeLayout<ITreeData> _boxen;

        public TreeLayout<ITreeData> Boxen
        {
            set
            {
                _boxen = value;
                Invalidate();
            }
        }

        private TreeForTreeLayout<ITreeData> GetTree()
        {
            return _boxen.getTree();
        }

        private Pen _border;

        private readonly static Color BORDER_COLOR = Color.Gray;
        private readonly static Color TEXT_COLOR = Color.Black;

        private Graphics _g;

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (_boxen == null)
                return;

            _g = e.Graphics;

            using (_border = new Pen(BORDER_COLOR))
            {
                paintEdges(GetTree().getRoot());

                // paint the boxes
                foreach (var node in _boxen.getNodeBounds().Keys)
                {
                    paintNode(node);
                }

                //paintDirtyEdges(getTree().getRoot());

            }

        }

        private Rectangle drawBounds(ITreeData node)
        {
            Rect r = _boxen.getNodeBounds()[node];
            return new Rectangle((int)r.Left, (int)r.Top, (int)r.Width, (int)r.Height);
        }


        private void paintNode(ITreeData node)
        {
            Rectangle box = drawBounds(node);

            PersonNode foo = node as PersonNode;
            if (foo != null)
            {
                paintABox(foo, box);
            }

            //UnionInBox bar = textInBox as UnionInBox;
            //if (bar != null)
            //{
            //    Rectangle box1 = new Rectangle(box.X, box.Y, bar.P1.Wide, bar.P1.High);
            //    paintABox(g, bar.P1, box1);
            //    // TODO UNION_BAR_WIDE
            //    Rectangle box2 = new Rectangle(box.X + bar.P1.Wide + 20, box.Y, bar.P2.Wide, bar.P2.High);
            //    paintABox(g, bar.P2, box2);
            //}

            using (var pen = new Pen(Color.Magenta))
                _g.DrawRectangle(pen, box);
            
        }

        private void paintABox(PersonNode tib, Rectangle box)
        {
            using (Brush b = new SolidBrush(tib.BackColor))
                _g.FillRectangle(b, box);
            _g.DrawRectangle(_border, box);
            _g.DrawString(tib.Text, DrawFont, new SolidBrush(TEXT_COLOR), box.X, box.Y);
        }


        private void paintEdges(ITreeData getRoot)
        {
            
        }
    }
}
