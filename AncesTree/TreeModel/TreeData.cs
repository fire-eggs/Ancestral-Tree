using AncesTree.TreeLayout;
using GEDWrap;
using System;
using System.Drawing;
using System.Windows.Forms;

// ReSharper disable InconsistentNaming

namespace AncesTree.TreeModel
{
    public interface ITreeData : ITreeNode
    {
        int Wide { get; }
        int High { get; }
        int CenterX { get; }
    }

    public class PersonNode : ITreeData
    {
        // Layout properties

        public int Wide { get; private set; }
        public int High { get; private set; }
        public int CenterX { get; private set; }
        public bool IsReal { get; set; }

        public Person Who { get; set; }

        public PersonNode(Person who, string txt, int width, int height)
        {
            Who = who;
            Text = txt;
            Wide = width;
            High = height;

            CenterX = (int) (width/2.0);

            IsReal = true;
        }

        // Drawing properties
        public string Text { get; set; }

        public Color BackColor { get; set; }

        public bool DrawVert { get; set; }
    }

    // A node consisting of two PersonNodes, connected with a line.
    public class UnionNode : ITreeData
    {
        public const int UNION_JOIN_WIDE = 20;     // TODO union-join-width from settings

        public int Wide
        {
            get { return P1.Wide + P2.Wide + UNION_JOIN_WIDE; }
        }

        public int High { get { return Math.Max(P1.High, P2.High); } }

        public int CenterX // TODO rename: location of upward line to parent bar
        {
            get
            {
                if (P1.DrawVert) // line should be drawn from P1
                    return P1.CenterX;
                // line should be drawn from P2
                return P1.Wide + UNION_JOIN_WIDE + P2.CenterX;
            }
        }

        public bool IsReal { get; set; }

        public PersonNode P1 { get; set; }

        public PersonNode P2 { get; set; }

        public UnionNode(PersonNode p1, PersonNode p2)
        {
            IsReal = true;
            P1 = p1;
            P2 = p2;
        }

    }

    public class NodeFactory : IDisposable
    {
        private readonly Graphics _graphics;
        private readonly Font _font;

        public NodeFactory(Control c, Font f)
        {
            _graphics = c.CreateGraphics();
            _font = f;
        }

        public void Dispose()
        {
            _graphics.Dispose();
            _font.Dispose();
        }

        public ITreeData Create(Person p, string s, Color clr, bool isSpouse = false)
        {
            var s2 = s.Replace("\n", Environment.NewLine);
            SizeF txtSz = _graphics.MeasureString(s2, _font, 1000, StringFormat.GenericTypographic);

            int w = (int)(txtSz.Width + 9); // TODO why so much extra required?
            int h = (int)(txtSz.Height + 2);
            var node = new PersonNode(p, s, w, h);
            node.BackColor = clr;
            node.DrawVert = !isSpouse; // TODO brother/sister incest: both spouses are children

            return node;
        }

        public ITreeData Create(ITreeData p1, ITreeData p2)
        {
            var p1A = (PersonNode) p1;
            var p2A = (PersonNode) p2;
            var node = new UnionNode(p1A, p2A);
            p1A.IsReal = p1A.DrawVert;  // TODO brother/sister incest: both spouses are children
            p2A.IsReal = p2A.DrawVert;
            return node;
        }
    }
}
