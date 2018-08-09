using AncesTree.TreeLayout;
using AncesTree.TreeModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using Point = System.Drawing.Point;

namespace AncesTree
{
    public class TreePanel2 : Panel
    {
        private TreeConfiguration _config;

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

            _config = _boxen.getConfiguration() as TreeConfiguration;
            if (_config == null)
                return;

            g.Clear(_config.BackColor.GetColor());
            _g.ScaleTransform(_zoom, _zoom);
            _g.TranslateTransform(_margin, _margin);

            _duplPen = _config.DuplLine.GetPen();
            _multEdge = _config.MMargLine.GetPen();
            _border = _config.NodeBorder.GetPen();
            _spousePen = _config.SpouseLine.GetPen();
            _childPen = _config.ChildLine.GetPen();
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

        private Pen _spousePen;

        private Pen _childPen;

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

        private readonly static Color TEXT_COLOR = Color.Black; // TODO configuration

        private const int UNION_BAR_WIDE = 20; // TODO pull from configuration

        private const int gapBetweenLevels = 30; // TODO pull from configuration

        private void paintNode(ITreeData node)
        {
            Rectangle box = drawBounds(node);

            PersonNode foo = node as PersonNode;
            if (foo != null)
            {
                // Don't draw the fake for multi-marriage at root
                if (foo.Text != " " || foo.Who != null)
                    paintABox(foo, box);
            }
            else
            {
                UnionNode bar = node as UnionNode;
                if (bar != null)
                {
                    Rectangle box1 = new Rectangle(box.X, box.Y, bar.P1.Wide, bar.P1.High);
                    Rectangle box2;
                    if (bar.Vertical)
                    {
                        box2 = new Rectangle(box.X, box.Y + bar.P1.High + UNION_BAR_WIDE, bar.P2.Wide, bar.P2.High);
                    }
                    else
                    {
                        box2 = new Rectangle(box.X + bar.P1.Wide + UNION_BAR_WIDE, box.Y, bar.P2.Wide, bar.P2.High);
                    }
                    paintABox(bar.P1, box1);
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
            using (var font = tib.DrawVert ? _config.MajorFont.GetFont() : _config.MinorFont.GetFont())
                _g.DrawString(tib.Text, font, new SolidBrush(TEXT_COLOR), box.X, box.Y);
        }

        private void PaintEdges(ITreeData parent)
        {
            if (parent is PersonNode)
            {
                var p = parent as PersonNode;
                // Don't draw the fake for multi-marriage at root
                if (p.Text != " " || p.Who != null)
                    paintEdges(parent as PersonNode);
            }
            else
            {
                if (parent.Vertical)
                    paintEdgesV(parent as UnionNode);
                else
                    paintEdgesH(parent as UnionNode);
            }
            foreach (var child in getChildren(parent))
            {
                PaintEdges(child);
            }
        }

        private void paintEdges(PersonNode parent)
        {
            var b1 = drawBounds(parent);

            // Spouse connectors in a multi-marriage case.
            // All spouses have been drawn to the right of 
            // this node.
            if (parent.HasSpouses)
            {
                int leftX = b1.Right;
                int leftY = b1.Top + b1.Height/2;
                foreach (var node in parent.Spouses)
                {
                    var b3 = drawBounds(node);
                    int rightX = b3.Left;

                    // TODO consider drawing distinct line for each?
                    _g.DrawLine(_multEdge, leftX, leftY, rightX, leftY);
                }
            }

            if (GetTree().isLeaf(parent)) // No children, nothing further to do
                return;

            // center-bottom of parent
            int parentX = b1.Left + b1.Width / 2;
            int parentY = b1.Bottom;

            DrawChildrenEdgesH(parent, parentX, parentY);
        }

        private void DrawChildrenEdgesH(ITreeData parent, int startx, int starty)
        {
            // Common code to draw edges from parent-node to children-nodes.
            // Horizontal (root at top) variant.
            // Used for UnionNode parent, and PersonNode parent in the multi-marriage
            // scenario.
            // Start position: place to start whether a union or a person node

            var b1 = drawBounds(parent);

            // Bottom point - vertical line from start position to child line
            int targetY = b1.Bottom + (gapBetweenLevels / 2);

            // Vertical connector from start position to child-line
            _g.DrawLine(_childPen, startx, starty, startx, targetY);

            // determine the left/right of the child-line
            int minChildX = int.MaxValue;
            int maxChildX = int.MinValue;

            foreach (var child in getChildren(parent))
            {
                // Do not draw 'I'm a child' line for spouses
                if (child is PersonNode && !((PersonNode)child).DrawVert)
                    continue;

                var b2 = drawBounds(child);
                //int childX = (int) (b2.Left + b2.Width/2);
                int childX = b2.Left + child.ParentVertLocX;
                int childY = b2.Top;

                minChildX = Math.Min(minChildX, childX);
                maxChildX = Math.Max(maxChildX, childX);

                // vertical line from top of child to half-way up to previous level
                _g.DrawLine(_childPen, childX, childY, childX, targetY);
            }
            _g.DrawLine(_childPen, minChildX, targetY, maxChildX, targetY);

            // Union has a single child. Vertical from child unlikely to
            // connect to vertical from union. Draw a horizontal connector.
            if (minChildX == maxChildX)
                _g.DrawLine(_childPen, minChildX, targetY, startx, targetY);
        }

        private void paintEdgesV(UnionNode parent)
        {
            Rectangle b1 = drawBounds(parent);

            // spouse connector between boxes
            int x = b1.Left + Math.Min(parent.P1.Wide, parent.P2.Wide) / 2;
            int y = b1.Top + parent.P1.High;
            _g.DrawLine(_spousePen, x, y, x, y + UNION_BAR_WIDE);

            // nothing further to do if this is a "duplicate" or there are no children
            if (DrawDuplicateUnion(parent) || !hasChildren(parent))
                return;

            // Draw the connector from the spouse-line to the children-line
            int horzLx = x;
            int horzLy = y + UNION_BAR_WIDE / 2;
            int targetX = b1.Left + b1.Width + (gapBetweenLevels / 2);
            _g.DrawLine(_childPen, horzLx, horzLy, targetX, horzLy);

            // determine top/bottom of children line
            int minChildY = int.MaxValue;
            int maxChildY = int.MinValue;

            foreach (var child in getChildren(parent))
            {
                // Do not draw 'I'm a child' line for spouses
                if (child is PersonNode && !((PersonNode)child).DrawVert)
                    continue;

                var b2 = drawBounds(child);
                int childX = b2.Left;
                int childY = b2.Top + child.ParentVertLocX;

                minChildY = Math.Min(minChildY, childY);
                maxChildY = Math.Max(maxChildY, childY);

                // connector from child to child-line
                _g.DrawLine(_childPen, childX, childY, targetX, childY);
            }
            // the child-line proper
            _g.DrawLine(_childPen, targetX, minChildY, targetX, maxChildY);

            // Union has a single child. Connector from child unlikely to
            // match to connector from union. Draw an extra connector.
            if (minChildY == maxChildY)
                _g.DrawLine(_childPen, targetX, minChildY, targetX, horzLy);
        }

        private void paintEdgesH(UnionNode parent)
        {
            // Draw edge connectors associated with a Union. This is the horizontal
            // (root at top) variant.

            var b1 = drawBounds(parent);

            // connector between spouse boxes
            int y = b1.Top + (b1.Height / 2);
            int x = b1.Left + parent.P1.Wide;
            _g.DrawLine(_spousePen, x, y, x + UNION_BAR_WIDE, y);

            // if this is a duplicate, or there are no children, we're done
            if (DrawDuplicateUnion(parent) || !hasChildren(parent))
                return;

            // Top point - vertical line from connector to child line
            int vertLx = x + UNION_BAR_WIDE / 2;
            int vertLy = y;

            DrawChildrenEdgesH(parent, vertLx, vertLy);
        }

        private bool DrawDuplicateUnion(UnionNode union)
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
