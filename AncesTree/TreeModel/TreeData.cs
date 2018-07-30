using System.Collections.Generic;
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
        int ParentVertLocX { get; }
    }

    public class PersonNode : ITreeData
    {
        // Layout properties

        public int Wide { get; private set; }
        public int High { get; private set; }
        public int ParentVertLocX { get; private set; }
        public bool IsReal { get; set; }

        public Person Who { get; set; }

        public PersonNode(Person who, string txt, int width, int height)
        {
            Who = who;
            Text = txt;
            Wide = width;
            High = height;

            ParentVertLocX = (int) (width/2.0);

            IsReal = true;
        }

        // Drawing properties
        public string Text { get; set; }

        public Color BackColor { get; set; }

        public bool DrawVert { get; set; }

        private List<ITreeData> _multSpouses;

        public void AddSpouse(ITreeData node)
        {
            // In the multi-marriage situation, the child
            // has been created as a PersonNode, and each
            // spouse as "not real" PersonNodes. To be able
            // to draw the appropriate edge(s) from the
            // child to each spouse, track them here.
            if (_multSpouses == null)
                _multSpouses = new List<ITreeData>();
            _multSpouses.Add(node);
        }

        public bool HasSpouses { get { return _multSpouses != null; } }

        public List<ITreeData> Spouses { get { return _multSpouses; } }
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

        public int ParentVertLocX
        {
            get
            {
                if (P1.DrawVert) // line should be drawn from P1
                    return P1.ParentVertLocX;
                // line should be drawn from P2
                return P1.Wide + UNION_JOIN_WIDE + P2.ParentVertLocX;
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
        private readonly Font _mainFont;
        private readonly Font _spouseFont;

        public NodeFactory(Control c, Font f, Font spouseFont)
        {
            _graphics = c.CreateGraphics();
            _mainFont = f;
            _spouseFont = spouseFont;
        }

        public void Dispose()
        {
            _graphics.Dispose();
            _mainFont.Dispose();
        }

        public ITreeData Create(Person p, string s, Color clr, bool isSpouse = false)
        {
            var s2 = s.Replace("\n", Environment.NewLine);
            SizeF txtSz = _graphics.MeasureString(s2,
                isSpouse ? _spouseFont : _mainFont,
                1000, StringFormat.GenericDefault);
                //StringFormat.GenericTypographic);

            int w = (int) (txtSz.Width); //+ 9); // TODO why so much extra required?
            int h = (int) (txtSz.Height);// + 2);
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
