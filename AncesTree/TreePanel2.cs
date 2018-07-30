using AncesTree.TreeLayout;
using AncesTree.TreeModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using Point = System.Drawing.Point;

namespace AncesTree
{
    public class TreePanel2 : Panel
    {
        public TreePanel2()
        {
            BorderStyle = BorderStyle.FixedSingle;
            ResizeRedraw = true;
            BackColor = Color.Beige;
            DoubleBuffered = true;
            Zoom = 1.0f;
        }

        public void drawTree(Graphics g)
        {
            if (_boxen == null)
                return;
            _g = g;

            //_g.SmoothingMode = SmoothingMode.AntiAlias;
            //_g.TextRenderingHint = TextRenderingHint.AntiAlias;

            //g.Clear(Color.AntiqueWhite);

            _g.ScaleTransform(_zoom, _zoom);
            _g.TranslateTransform(_margin, _margin);

            // TODO configuration x 3
            using (_duplPen = new Pen(Color.CornflowerBlue) { DashStyle = DashStyle.Dash })
            using (_multEdge = new Pen(Color.Coral) { DashStyle = DashStyle.Dash })
            using (_border = new Pen(BORDER_COLOR))
            {
                PaintEdges(GetTree().getRoot());

                // paint the boxes
                foreach (var node in _boxen.getNodeBounds().Keys)
                {
                    paintNode(node);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            drawTree(e.Graphics);
        }

        private int _margin = 10;

        public int TreeMargin
        {
            get { return _margin; }
            set { _margin = Math.Max(value, 0); ResizeMe(); }
        }

        private float _zoom;
        public float Zoom 
        { 
            get { return _zoom; }
            set { _zoom = Math.Max(value, 0.1f); ResizeMe(); } 
        }

        private Graphics _g;

        private Font _font;
        private Font _font2;

        public Font DrawFont  // TODO configuration
        { 
            get { return _font; }
            set { _font = (Font)(value.Clone()); } // TODO re-layout tree?
        }
        public Font SpouseFont  // TODO configuration
        {
            get { return _font2; }
            set { _font2 = (Font)(value.Clone()); } // TODO re-layout tree?
        }

        private TreeLayout<ITreeData> _boxen;

        public TreeLayout<ITreeData> Boxen
        {
            set { _boxen = value; ResizeMe(); }
        }

        private void ResizeMe()
        {
            // Tree or scale changed. Update control size to fit.

            if (_boxen == null)
                return;
            var newSize = _boxen.getBounds();
            // NOTE side-effect: control is invalidated from resize
            this.Size = new System.Drawing.Size
                ((int)((newSize.Width + 2 * _margin) * _zoom),
                 (int)((newSize.Height + 2 * _margin) * _zoom));
        }

        private Pen _border;

        private Pen _multEdge;

        private Pen _duplPen;

        #region TreeLayout Access/Helper functions

        private Rectangle drawBounds(ITreeData node)
        {
            Rect r = getBoundsOfNode(node);
            return new Rectangle((int)r.Left, (int)r.Top, (int)r.Width, (int)r.Height);
        }

        private Rect getBoundsOfNode(ITreeData node)
        {
            return _boxen.getNodeBounds()[node];
        }

        private IEnumerable<ITreeData> getChildren(ITreeData parent)
        {
            return GetTree().getChildren(parent);
        }

        private bool hasChildren(ITreeData parent)
        {
            var enumer = getChildren(parent);
            return (enumer != null && enumer.Any());
        }

        private TreeForTreeLayout<ITreeData> GetTree()
        {
            return _boxen.getTree();
        }

        #endregion

        private readonly static Color BORDER_COLOR = Color.Gray; // TODO configuration
        private readonly static Color TEXT_COLOR = Color.Black; // TODO configuration

        private const int UNION_BAR_WIDE = 20; // TODO pull from configuration

        private const int gapBetweenLevels = 30; // TODO pull from configuration

        private void paintNode(ITreeData node)
        {
            Rectangle box = drawBounds(node);

            PersonNode foo = node as PersonNode;
            if (foo != null)
            {
                if (foo.Text == " " && foo.Who == null)
                    ; // fake for multi-marriage at root, don't draw
                else
                    paintABox(foo, box);
            }
            else
            {
                UnionNode bar = node as UnionNode;
                if (bar != null)
                {
                    Rectangle box1 = new Rectangle(box.X, box.Y, bar.P1.Wide, bar.P1.High);
                    paintABox(bar.P1, box1);
                    Rectangle box2 = new Rectangle(box.X + bar.P1.Wide + UNION_BAR_WIDE, box.Y, bar.P2.Wide, bar.P2.High);
                    paintABox(bar.P2, box2);
                }

                //using (var pen = new Pen(Color.Magenta))
                //    _g.DrawRectangle(pen, box);
            }
        }

        private void paintABox(PersonNode tib, Rectangle box)
        {
            using (Brush b = new SolidBrush(tib.BackColor))
                _g.FillRectangle(b, box);
            _g.DrawRectangle(_border, box);
            _g.DrawString(tib.Text, 
                tib.DrawVert ? DrawFont : SpouseFont,
                new SolidBrush(TEXT_COLOR), box.X, box.Y);
        }

        private void PaintEdges(ITreeData parent)
        {
            if (parent is PersonNode)
            {
                var p = parent as PersonNode;
                if (p.Who == null && p.Text == " ")
                    ; // fake root for multi-marriage, don't draw!
                else
                    paintEdges(parent as PersonNode);
            }
            else
                paintEdges(parent as UnionNode);
            foreach (var child in getChildren(parent))
            {
                PaintEdges(child);
            }
        }

        private void paintEdges(PersonNode parent)
        {
            Rect b1 = getBoundsOfNode(parent);

            // Spouse connectors in a multi-marriage case.
            // All spouses have been drawn to the right of 
            // this node.
            if (parent.HasSpouses)
            {
                int leftX = (int)b1.Right;
                int leftY = (int) (b1.Top + b1.Height/2);
                foreach (var node in parent.Spouses)
                {
                    Rect b3 = getBoundsOfNode(node);
                    int rightX = (int) b3.Left;

                    // TODO consider drawing distinct line for each?
                    _g.DrawLine(_multEdge, leftX, leftY, rightX, leftY);
                }
            }

            if (GetTree().isLeaf(parent)) // No children, nothing further to do
                return;

            // Children connectors. Used when PersonNode represents
            // a spouse in a multi-marriage.

            // TODO refactor common code
            // center-bottom of parent
            int parentX = (int)(b1.Left + b1.Width / 2);
            int parentY = (int)(b1.Bottom);

            // target for vertical line from child
            int targetY = parentY + (gapBetweenLevels / 2);

            // vertical line from bottom of parent to half-way down to next level
            _g.DrawLine(_border, parentX, parentY, parentX, targetY);

            // determine the left/right of the horizontal line
            int minChildX = int.MaxValue;
            int maxChildX = int.MinValue;

            foreach (var child in getChildren(parent))
            {
                // Do not draw 'I'm a child' line for spouses
                if (child is PersonNode && !((PersonNode)child).DrawVert)
                    continue;

                Rect b2 = getBoundsOfNode(child);
                //int childX = (int) (b2.Left + b2.Width/2);
                int childX = (int)b2.Left + child.ParentVertLocX;
                int childY = (int)(b2.Top);

                minChildX = Math.Min(minChildX, childX);
                maxChildX = Math.Max(maxChildX, childX);

                // vertical line from top of child to half-way up to previous level
                _g.DrawLine(_border, childX, childY, childX, targetY);
            }

            try
            {
                _g.DrawLine(_border, minChildX, targetY, maxChildX, targetY);
            }
            catch (Exception)
            {
                // TODO why?
            }

            // Union has a single child. Vertical from child unlikely to
            // connect to vertical from union. Draw a horizontal connector.
            if (minChildX == maxChildX)
                _g.DrawLine(_border, minChildX, targetY, parentX, targetY);

        }

        private void paintEdges(UnionNode parent)
        {
            // Draw any connectors associated with a Union.
            // a. Connector between spouse boxes
            // b. Drop line from connector to child horz. line
            // c. Child horz. line
            // d. Vertical line from each child to child horz. line

            Rect b1 = getBoundsOfNode(parent);

            // horz connector between spouse boxes
            int y = (int)(b1.Top + (b1.Height / 2));
            int x = (int)(b1.Left + parent.P1.Wide);
            _g.DrawLine(_border, x, y, x + UNION_BAR_WIDE, y);

            if (drawDuplicateUnion(parent))
                return;

            // union has no children, we're done
            if (!hasChildren(parent))
                return;

            // Top point - vertical line from connector to child line
            int vertLx = x + UNION_BAR_WIDE / 2;
            int vertLy = y;

            // Bottom point - vertical line from connector to child line
            int targetY = (int)(b1.Top + parent.High + (gapBetweenLevels / 2));

            // Vertical connector from spouse line to child line
            _g.DrawLine(_border, vertLx, vertLy, vertLx, targetY);

            // determine the left/right of the horizontal line
            int minChildX = int.MaxValue;
            int maxChildX = int.MinValue;

            foreach (var child in getChildren(parent))
            {
                // Do not draw 'I'm a child' line for spouses
                if (child is PersonNode && !((PersonNode)child).DrawVert)
                    continue;

                Rect b2 = getBoundsOfNode(child);
                //int childX = (int) (b2.Left + b2.Width/2);
                int childX = (int)b2.Left + child.ParentVertLocX;
                int childY = (int)(b2.Top);

                minChildX = Math.Min(minChildX, childX);
                maxChildX = Math.Max(maxChildX, childX);

                // vertical line from top of child to half-way up to previous level
                _g.DrawLine(_border, childX, childY, childX, targetY);
            }
            _g.DrawLine(_border, minChildX, targetY, maxChildX, targetY);

            // Union has a single child. Vertical from child unlikely to
            // connect to vertical from union. Draw a horizontal connector.
            if (minChildX == maxChildX)
                _g.DrawLine(_border, minChildX, targetY, vertLx, targetY);

        }

        private bool drawDuplicateUnion(UnionNode union)
        {
            // Draw a connection between two, duplicated unions

            // This is not a duplicate, nothing to do
            if (union.DupNode == null)
                return false;

            var thisRect = drawBounds(union);
            var destRect = drawBounds(union.DupNode);

            thisRect.Inflate(1, 1); // TODO need to use the pen thickness
            destRect.Inflate(1, 1); // TODO need to use the pen thickness

            int midXDest = destRect.Left + (destRect.Right - destRect.Left) / 2;
            int midXthis = thisRect.Left + (thisRect.Right - thisRect.Left) / 2;
            int midmidX = midXDest + (midXthis - midXDest) / 2;

            int midY = Math.Max(thisRect.Bottom, destRect.Bottom) + (gapBetweenLevels / 2) - 5; // TODO tweak/make constant

            Point p1 = new Point(midXthis, thisRect.Bottom);
            Point p2 = new Point(midmidX, midY);
            Point p3 = new Point(midXDest, destRect.Bottom);

            _g.DrawRectangle(_duplPen, thisRect);
            _g.DrawRectangle(_duplPen, destRect);

            // TODO p2 might be below the bottom of the panel.
            // TODO consider drawing from left edge of 'this' to right edge of 'dest'
            _g.DrawCurve(_duplPen, new[] { p1, p2, p3 });
            return true;
        }

    }
}
