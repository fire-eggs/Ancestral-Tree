using GEDWrap;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AncesTree
{
    public partial class PersonSearch : Form
    {
        Forest _tree;
        private ListViewColumnSorter lvwColumnSorter;

        private bool onLoad = true;

        public PersonSearch()
        {
            InitializeComponent();
            lvwColumnSorter = new ListViewColumnSorter();
            listView1.ListViewItemSorter = lvwColumnSorter;

            button1.Text = "OK";
            button2.Text = "Cancel";
            button1.Enabled = false;
        }

        public Forest Tree
        {
            set
            {
                if (_tree == value)
                    return;
                _tree = value;
                _fullList = null;
            }
        }

        public Person SelectedPerson { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SelectedPerson = null;
            Close();
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (listView1.SelectedItems.Count < 1)
            {
                SelectedPerson = null;
                button1.Enabled = false;
            }
            else
            {
                var item = listView1.SelectedItems[0];
                var id = item.SubItems[3].Text;
                SelectedPerson = _tree.PersonById(id);
                button1.Enabled = true;
            }
        }

        private void PersonSearch_Load(object sender, EventArgs e)
        {
            InitList();
            textBox1.Clear();

            listView1.BeginUpdate();
            listView1.Items.AddRange(_fullList.ToArray());
            listView1.Sort();
            foreach (ColumnHeader c in listView1.Columns)
                c.Width = -1;
            listView1.EndUpdate();

            onLoad = true;
        }

        private void InitList()
        {
            _filterList = new List<ListViewItem>();
            _fullList = new List<ListViewItem>();
            foreach (Person p in _tree.AllPeople)
            {
                var ld = new ListViewItem(p.Surname);
                ld.SubItems.Add(p.Given);
                ld.SubItems.Add(p.BirthDate == null ? "?" : p.BirthDate.Year.ToString());
                ld.SubItems.Add(p.DeathDate == null ? "?" : p.DeathDate.Year.ToString());
                ld.SubItems.Add(p.Id);

                _fullList.Add(ld);
            }
        }

        private List<ListViewItem> _fullList;

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.listView1.Sort();
        }

        private class ListViewColumnSorter : System.Collections.IComparer
        {
            public int SortColumn { get; set; }
            public SortOrder Order { get; set; }

            private readonly System.Collections.CaseInsensitiveComparer _objectCompare;

            public ListViewColumnSorter()
            {
                SortColumn = 0;
                Order = SortOrder.Ascending;
                _objectCompare = new System.Collections.CaseInsensitiveComparer();
            }

            public int Compare(object x, object y)
            {
                var lvX = x as ListViewItem;
                var lvY = y as ListViewItem;
                int result = _objectCompare.Compare(lvX.SubItems[SortColumn].Text, 
                                                    lvY.SubItems[SortColumn].Text);
                switch (Order)
                {
                    case SortOrder.Ascending:
                        return result;
                    case SortOrder.Descending:
                        return -result;
                    default:
                        return 0;
                }
            }
        }

        private List<ListViewItem> _filterList;

        private void ClearSelection()
        {
            if (listView1.SelectedIndices.Count > 0)
                for (int i = 0; i < listView1.SelectedIndices.Count; i++)
                {
                    listView1.Items[listView1.SelectedIndices[i]].Selected = false;
                }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                ClearSelection();
                listView1.Items.Clear();
                listView1.Items.AddRange(_fullList.ToArray());
                return;
            }

            _filterList.Clear();
            string filter = textBox1.Text.ToLower();
            for (int i = 0; i < _fullList.Count; i++)
            {
                var lvi = _fullList[i];
                for (int j = 0; j < lvi.SubItems.Count; j++)
                {
                    var lvsi = lvi.SubItems[j];
                    if (lvsi.Text.ToLower().Contains(filter))
                    {
                        _filterList.Add(lvi);
                        break;
                    }
                }
            }

            ClearSelection();
            listView1.Items.Clear();
            listView1.Items.AddRange(_filterList.ToArray());
            listView1.Sort();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox1.Focus();
        }

        private void listView1_Enter(object sender, EventArgs e)
        {
            // For reasons I don't know, the listview insists on grabbing 
            // initial focus. The filter box needs initial focus.
            if (onLoad)
                textBox1.Focus();
            onLoad = false;
        }
    }
}
