using System.Collections.Generic;
using AncesTree.TreeLayout;
using GEDWrap;
using System;
using System.Drawing;
using System.Windows.Forms;

// ReSharper disable InconsistentNaming

namespace AncesTree.TreeModel
{
    /// <summary>
    /// Common drawing properties for the tree chart.
    /// </summary>
    public interface ITreeData : ITreeNode
    {
        /// <summary>
        /// The location for the 'parent connector'. For a horizontal chart, this is the spot
        /// along the top of the node. For a vertical chart, this is the spot along the right
        /// edge of the node. This is not always the 'center' of the node, as the parent
        /// connector is to be drawn to the 'blood relation' child inside a Union.
        /// </summary>
        int ParentConnectLoc { get; }

        // Is this chart vertical?
        // TODO take from settings?
        bool Vertical { get; }

        // This node might have a duplicate elsewhere
        ITreeData DupNode { get; set; }
    }

    public class PersonNode : ITreeData
    {
        #region Layout properties

        public int Wide { get; private set; }
        public int High { get; private set; }
        public bool IsReal { get; set; }

        #endregion

        #region Common Drawing properties

        public int ParentConnectLoc { get; private set; }

        public bool Vertical { get; private set; }

        public ITreeData DupNode { get; set; }

        #endregion

        public Person Who { get; set; }

        public PersonNode(Person who, string txt, int width, int height, bool vertical)
        {
            Who = who;
            Text = txt;
            Wide = width;
            High = height;
            Vertical = vertical;

            ParentConnectLoc = Vertical ? (int)(height/2.0) : (int) (width/2.0);

            IsReal = true;
        }

        public string Text { get; set; }

        public Color BackColor { get; set; }

        public bool DrawVert { get; set; }

        #region multi-marriage support
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

        #endregion
    }

    // A node consisting of two PersonNodes, connected with a line.
    public class UnionNode : ITreeData
    {
        #region Layout Properties

        public int Wide
        {
            get
            {
                if (Vertical)
                    return Math.Max(P1.Wide, P2.Wide);
                return P1.Wide + P2.Wide + UNION_JOIN_WIDE;
            }
        }

        public int High
        {
            get
            {
                if (Vertical)
                    return P1.High + P2.High + UNION_JOIN_WIDE;
                return Math.Max(P1.High, P2.High);
            }
        }

        public bool IsReal { get; set; }

        #endregion

        #region Common Drawing properties

        public bool Vertical { get; private set; }

        public int ParentConnectLoc
        {
            get
            {
                if (P1.DrawVert) // line should be drawn from P1
                    return P1.ParentConnectLoc;
                // line should be drawn from P2
                if (Vertical)
                    return P1.High + UNION_JOIN_WIDE + P2.ParentConnectLoc;
                return P1.Wide + UNION_JOIN_WIDE + P2.ParentConnectLoc;
            }
        }

        public ITreeData DupNode { get; set; }
        #endregion


        public const int UNION_JOIN_WIDE = 20;     // TODO union-join-width from settings

        public PersonNode P1 { get; set; }

        public PersonNode P2 { get; set; }

        public string UnionId { get; set; }

        public UnionNode(PersonNode p1, PersonNode p2, string unionId, bool vertical)
        {
            IsReal = true;
            P1 = p1;
            P2 = p2;
            UnionId = unionId;
            Vertical = vertical;
        }
    }

    public class NodeFactory : IDisposable
    {
        private readonly Graphics _graphics;
        private readonly Font _mainFont;
        private readonly Font _spouseFont;
        private readonly bool _vertOrient;

        public NodeFactory(Control c, Font f, Font spouseFont, bool vertOrient)
        {
            _graphics = c.CreateGraphics();
            _mainFont = f;
            _spouseFont = spouseFont;
            _vertOrient = vertOrient;
        }

        public void Dispose()
        {
            _graphics.Dispose();
            _mainFont.Dispose();
        }

        public ITreeData Create(Person p, string s, Color clr, bool isSpouse = false)
        {
            // MeasureString and DrawText will behave as desired for multiline text
            // if the Environment.NewLine is used for line separation.
            var s2 = s.Replace("\n", Environment.NewLine);
            SizeF txtSz = _graphics.MeasureString(s2,
                isSpouse ? _spouseFont : _mainFont,
                1000, StringFormat.GenericDefault);

            int w = (int) (txtSz.Width + 1);
            int h = (int) (txtSz.Height);
            var node = new PersonNode(p, s, w, h, _vertOrient);
            node.BackColor = clr;
            node.DrawVert = !isSpouse; // TODO brother/sister incest: both spouses are children

            return node;
        }

        public ITreeData Create(ITreeData p1, ITreeData p2, string unionId)
        {
            var p1A = (PersonNode) p1;
            var p2A = (PersonNode) p2;
            var node = new UnionNode(p1A, p2A, unionId, _vertOrient);
            p1A.IsReal = p1A.DrawVert;  // TODO brother/sister incest: both spouses are children
            p2A.IsReal = p2A.DrawVert;
            return node;
        }
    }
}
