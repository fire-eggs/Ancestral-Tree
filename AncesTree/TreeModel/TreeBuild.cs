using System.Collections.Generic;
using AncesTree.TreeLayout;
using GEDWrap;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AncesTree.TreeModel
{
    public static class TreeBuild
    {
        private static NodeFactory _nf;
        private static DefaultTreeForTreeLayout<ITreeData> _tree;
        private static Dictionary<string, ITreeData> _unionSet;
        private static TreeConfiguration _config;

        public static TreeForTreeLayout<ITreeData> BuildTree(Control ctl, TreeConfiguration config, Person root)
        {
            _config = config;
            _nf = new NodeFactory(ctl, _config.MajorFont, _config.MinorFont, _config.RootOnLeft);
            _unionSet = new Dictionary<string, ITreeData>();

            ITreeData treeRoot;
            switch (root.SpouseIn.Count)
            {
                case 0:
                case 1:
                    treeRoot = MakeNode(root);
                    _tree = new DefaultTreeForTreeLayout<ITreeData>(treeRoot);
                    GrowTree(treeRoot as UnionNode);
                    break;
                default:
                    // Multi-marriage at the root.
                    treeRoot = _nf.Create(null, " ", Color.GreenYellow);
                    _tree = new DefaultTreeForTreeLayout<ITreeData>(treeRoot);
                    MultiMarriage(treeRoot, root);
                    break;
            }
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
            return _nf.Create(p1, p2, marr.Id);
        }

        private static void GrowTree(UnionNode parent)
        {
            if (parent == null)
                return; // person has no spouse/child

            var who = parent.P1.Who ?? parent.P2.Who;
            if (who == null)
                return;

            // This union might have already been added to the
            // tree. If so, provide a link to the previous node,
            // and do NOT add children.
            ITreeData dup;
            if (_unionSet.TryGetValue(parent.UnionId, out dup))
            {
                parent.DupNode = dup;
                return;
            }

            _unionSet.Add(parent.UnionId, parent);

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
            // 2b. Connect each spouse to the person for drawing.
            // 2c. call GrowTree(spouse, marriage)

            PersonNode nodeP = (PersonNode)_nf.Create(who, StringForNode(who), ColorForNode(who));
            _tree.addChild(parent, nodeP);

            foreach (var marr in who.SpouseIn)
            {
                Person spouseP = marr.Spouse(who);
                PersonNode node = (PersonNode)_nf.Create(spouseP, StringForNode(spouseP), ColorForNode(spouseP), true);
                node.IsReal = false;
                nodeP.AddSpouse(node);
                _tree.addChild(parent, node);

                // This union might have already been added to the
                // tree. If so, provide a link to the previous node,
                // and do NOT add children.
                ITreeData dup;
                if (_unionSet.TryGetValue(marr.Id, out dup))
                {
                    nodeP.DupNode = dup;
                    node.DupNode = dup;
                }
                else
                {
                    _unionSet.Add(marr.Id, nodeP);
                    GrowTree(node, marr);
                }
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
                return _config.UnknownColor;
            switch (who.Sex)
            {
                case "Male":
                    return _config.MaleColor;
                case "Female":
                    return _config.FemaleColor;
                default:
                    return _config.UnknownColor;
            }
        }
    }
}
