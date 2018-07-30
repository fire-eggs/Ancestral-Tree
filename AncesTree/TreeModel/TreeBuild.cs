using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AncesTree.TreeLayout;
using GEDWrap;

namespace AncesTree.TreeModel
{
    public static class TreeBuild
    {
        private static NodeFactory _nf;
        private static DefaultTreeForTreeLayout<ITreeData> _tree;

        public static TreeForTreeLayout<ITreeData> BuildTree(Control ctl, Font font, Person root)
        {
            _nf = new NodeFactory(ctl, font);

            var rootN = MakeNode(root);  // TODO multi-marriage at root
            _tree = new DefaultTreeForTreeLayout<ITreeData>(rootN);
            GrowTree(rootN as UnionNode);
            return _tree;
        }

        private static ITreeData MakeNode(Person who)
        {
            //// No spouse/children: make a personnode
            if (who.SpouseIn.Count == 0)
            {
                return _nf.Create(who, StringForNode(who), ColorForNode(who));
            }

            Union marr = who.SpouseIn.First();

            //var rootP = _nf.Create(who, StringForNode(who), ColorForNode(who));
            //Person spouse = marr.Spouse(who);
            //var spouseP = _nf.Create(spouse, StringForNode(spouse), ColorForNode(spouse));
            //return _nf.Create(rootP, spouseP);

            // convention of husband-left, wife-right
            var p1 = _nf.Create(marr.Husband, StringForNode(marr.Husband), 
                ColorForNode(marr.Husband), marr.Husband == null || marr.Husband.Id != who.Id);
            var p2 = _nf.Create(marr.Wife, StringForNode(marr.Wife), 
                ColorForNode(marr.Wife), marr.Wife == null || marr.Wife.Id != who.Id);
            return _nf.Create(p1, p2);
        }

        private static void GrowTree(UnionNode parent)
        {
            if (parent == null)
                return; // person has no spouse/child

            var who = parent.P1.Who ?? parent.P2.Who;
            if (who == null)
                return;

            Union marr = who.SpouseIn.First();
            foreach (var child in marr.Childs)
            {
                switch (child.SpouseIn.Count)
                {
                    case 0:
                    case 1:
                        ITreeData node = MakeNode(child);
                        _tree.addChild(parent, node);
                        GrowTree(node as UnionNode);
                        break;
                    default:
                        MultiMarriage(parent, child);
                        break;
                }
            }
        }

        private static void MultiMarriage(ITreeData parent, Person who)
        {
            // A person has multiple marriages.
            // 1. Make the person a child of the parent.
            // 2. For each marriage:
            // 2a. Add the spouse as a not-real child of the parent.
            // 2b. call GrowTree(spouse, marriage)

            ITreeData node = _nf.Create(who, StringForNode(who), ColorForNode(who));
            _tree.addChild(parent, node);

            foreach (var marr in who.SpouseIn)
            {
                Person spouseP = marr.Spouse(who);
                node = _nf.Create(spouseP, StringForNode(spouseP), ColorForNode(spouseP), true);
                node.IsReal = false;
                _tree.addChild(parent, node);
                Debug.Assert(node.IsReal == false);

                GrowTree(node, marr);
            }
        }

        private static void GrowTree(ITreeData parent, Union marr)
        {
            // In the multi-marriage situation, we need to add
            // the children of a *specific* marriage as child-nodes
            // of the parent.
            foreach (var child in marr.Childs)
            {
                switch (child.SpouseIn.Count)
                {
                    case 0:
                    case 1:
                        ITreeData node = MakeNode(child);
                        _tree.addChild(parent, node);
                        GrowTree(node as UnionNode);
                        break;
                    default:
                        MultiMarriage(parent, child);
                        break;
                }
            }
        }

        public static string StringForNode(Person who)
        {
            if (who == null)
                return string.Format("{3}?{3}?-?", "", "", "", Environment.NewLine); ;
            var byr = who.BirthDate == null ? "?" : who.BirthDate.Year.ToString();

            // TODO need DeathDate from GedWrap!
            //var dyr = who.DeathDate == null ? "?" : who.DeathDate.Year.ToString();

            return string.Format("{0}{3}{1}{3}{2}", who.Given, who.Surname, byr, Environment.NewLine);
        }

        public static Color ColorForNode(Person who)
        {
            if (who == null)
                return Color.MediumOrchid;
            switch (who.Sex)
            {
                case "Male":
                    return Color.PowderBlue;
                case "Female":
                    return Color.Pink;
                default:
                    return Color.MediumOrchid;
            }
        }
    }
}
