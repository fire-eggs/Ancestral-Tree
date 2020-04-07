using AncesTree.TreeLayout;
using AncesTree.TreeModel;
using DrawAnce;
using GEDWrap;
using PrintPreview;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace AncesTree
{
    public partial class Form1 : Form
    {
        private List<CmbItem> _cmbItems;
        protected MruStripMenu mnuMRU;

        public Form1()
        {
            InitializeComponent();

            mnuMRU = new MruStripMenuInline(fileToolStripMenuItem, recentFilesToolStripMenuItem, OnMRU);
            mnuMRU.MaxEntries = 7;

            LoadSettings(); // NOTE: must go after mnuMRU init

            treePanel1.OnNodeClick += TreePanel1_OnNodeClick;
            treePanel1.OnNodeHover += TreePanel1_OnNodeHover;
            treePanel1.OnNodeDoubleClick += TreePanel1_OnNodeDoubleClick;
        }

        private void TreePanel1_OnNodeHover(object sender, ITreeData node)
        {
            var pnode = node as PersonNode;
            //if (pnode == null)
            //{
            //    toolTip1.Hide(treePanel1);
            //}
            //else
            //{
            //    toolTip1.Show(pnode.Text, treePanel1);
            //}
        }

        private void SelectPerson(Person who)
        {
            for (int i = 0; i < _cmbItems.Count; i++)
            {
                if (_cmbItems[i].Value == who)
                {
                    personSel.SelectedIndex = i;
                    return;
                }
            }
        }

        private void TreePanel1_OnNodeClick(object sender, ITreeData node)
        {
            var pnode = node as PersonNode;
            if (pnode == null)
                return;

            // TODO what do we want to do on single-click?
        }

        private void TreePanel1_OnNodeDoubleClick(object sender, ITreeData node)
        {
            var pnode = node as PersonNode;
            if (pnode == null)
                return;
            SelectPerson(pnode.Who);
        }

        private void OnMRU(int number, string filename)
        {
            if (!File.Exists(filename))
            {
                mnuMRU.RemoveFile(number);
                MessageBox.Show("The file no longer exists: " + filename);
                return;
            }
            LastFile = filename;
            mnuMRU.SetFirstFile(number);
            ProcessGED(filename);
        }

        #region Settings
        private DASettings _mysettings;

        private List<string> _fileHistory = new List<string>();

        private string LastFile
        {
            get
            {
                if (_fileHistory == null || _fileHistory.Count < 1)
                    return null;
                return _fileHistory[0]; // First entry is the most recent
            }
            set
            {
                // Make sure to wipe any older instance
                _fileHistory.Remove(value);
                _fileHistory.Insert(0, value); // First entry is the most recent
            }
        }

        private void LoadSettings()
        {
            _mysettings = DASettings.Load();

            // No existing settings. Use default.
            if (_mysettings.Fake)
            {
                StartPosition = FormStartPosition.CenterScreen;
            }
            else
            {
                // restore windows position
                StartPosition = FormStartPosition.Manual;
                Top = _mysettings.WinTop;
                Left = _mysettings.WinLeft;
                Height = _mysettings.WinHigh;
                Width = _mysettings.WinWide;
                _fileHistory = _mysettings.PathHistory ?? new List<string>();
                _fileHistory.Remove(null);
                mnuMRU.SetFiles(_fileHistory.ToArray());

                LastFile = _mysettings.LastPath;
            }
        }

        private void SaveSettings()
        {
            // TODO check minimized
            var bounds = DesktopBounds;
            _mysettings.WinTop = Location.Y;
            _mysettings.WinLeft = Location.X;
            _mysettings.WinHigh = bounds.Height;
            _mysettings.WinWide = bounds.Width;
            _mysettings.Fake = false;
            _mysettings.LastPath = LastFile;
            _mysettings.PathHistory = mnuMRU.GetFiles().ToList();
            _mysettings.Save();
        }
        #endregion

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TreePerson(Person val)
        {
            var config = TreeConfiguration.LoadConfig();

            config.MaxDepth = (int)spinMaxGen.Value;

            var tree = TreeBuild.BuildTree(treePanel1, config, val);

            // create the NodeExtentProvider for TextInBox nodes
            var nodeExtentProvider = new NodeExtents();

            // create the layout
            var treeLayout = new TreeLayout<ITreeData>(tree, nodeExtentProvider, config, !tree.getRoot().IsReal);
            treePanel1.Boxen = treeLayout;
        }

        private void personSel_SelectedIndexChanged(object sender, EventArgs e)
        {
            var val = personSel.SelectedValue as Person;
            if (val == null)
                return;
            TreePerson(val);
        }

        private void ProcessGED(string gedPath)
        {
            Text = gedPath;
            Application.DoEvents(); // Cycle events so image updates in case GED load/process takes a while
            LoadGed();
        }

        private void loadGEDCOMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "GEDCOM files|*.ged;*.GED";
            ofd.FilterIndex = 1;
            ofd.DefaultExt = "ged";
            ofd.CheckFileExists = true;
            if (DialogResult.OK != ofd.ShowDialog(this))
            {
                return;
            }
            mnuMRU.AddFile(ofd.FileName);
            LastFile = ofd.FileName; // TODO invalid ged file
            ProcessGED(ofd.FileName);
        }

        private Forest _gedtrees;

        private class CmbItem
        {
            public string Text { get; set; }
            public Person Value { get; set; }
        }

        void LoadGed()
        {
            _gedtrees = new Forest();
            _gedtrees.LoadGEDCOM(LastFile);

            personSel.SelectedIndexChanged -= personSel_SelectedIndexChanged;
            personSel.Enabled = false;
            personSel.BeginUpdate();
            personSel.SelectedIndex = -1; // force SelectedIndexChanged to happen below
            personSel.DataSource = null;

            // Issue #24: Merely clearing the list does not properly update the combo. Must recreate it.
            _cmbItems = new List<CmbItem>();

            HashSet<string> comboNames = new HashSet<string>();
            Dictionary<string, Person> comboPersons = new Dictionary<string, Person>();
            foreach (var indiId in _gedtrees.AllIndiIds)
            {
                Person p = _gedtrees.PersonById(indiId);
                string byear = p.BirthDate == null ? "?" : p.BirthDate.Year.ToString();
                string dyear = p.DeathDate == null ? "?" : p.DeathDate.Year.ToString();

                var text = string.Format("{0},{1} [b. {3} d. {4}] \t ({2})", p.Surname, p.Given, indiId, byear, dyear);
                comboNames.Add(text);
                comboPersons.Add(text, p);
            }

            var nameSort = comboNames.ToArray();
            Array.Sort(nameSort);
            foreach (var s in nameSort)
            {
                _cmbItems.Add(new CmbItem {Text=s,Value=comboPersons[s]});
            }
            
            personSel.DisplayMember = "Text";
            personSel.ValueMember = "Value";
            personSel.DataSource = _cmbItems;
            personSel.SelectedIndex = -1; // force SelectedIndexChanged to happen below
            personSel.EndUpdate();
            personSel.Enabled = true;
            personSel.SelectedIndexChanged += personSel_SelectedIndexChanged;
            personSel.SelectedIndex = 0; // after switch, force rebuild of tree for the first person in the (new) tree
        }

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            treePanel1.Zoom = treePanel1.Zoom + 0.1f;
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            treePanel1.Zoom = treePanel1.Zoom - 0.1f;
        }

        private void btn100Percent_Click(object sender, EventArgs e)
        {
            treePanel1.Zoom = 1.0f;
        }

        private string _lastSaveFolder; // TODO to app settings
        private int _lastUsedSaveFilter = 2; // TODO to app settings
        private SaveFileDialog _sfd;

        private void btnToImage_Click(object sender, EventArgs e)
        {
            // save to image

            if (_sfd == null)
            {
                _lastSaveFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                _sfd = new SaveFileDialog();
                _sfd.Filter = "Bitmap (*.bmp)|*.bmp|Jpeg (*.jpg)|*.jpg|PNG (*.png)|*.png";
                _sfd.Title = "Save current tree to image";
                _sfd.CheckPathExists = true;
            }
            _sfd.InitialDirectory = Directory.Exists(_lastSaveFolder) ? _lastSaveFolder : @"C:\";
            _sfd.FilterIndex = _lastUsedSaveFilter;
            if (_sfd.ShowDialog() != DialogResult.OK)
                return;

            _lastUsedSaveFilter = _sfd.FilterIndex;
            _lastSaveFolder = Path.GetDirectoryName(_sfd.FileName);
            var iFormat = ImageFormat.Png;
            switch (_lastUsedSaveFilter)
            {
                case 1:
                    iFormat = ImageFormat.Bmp;
                    break;
                case 2:
                    iFormat = ImageFormat.Jpeg;
                    break;
            }

            using (var b = drawToImage())
            {
                b.Save(_sfd.FileName, iFormat);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Control)
                return;
            if (e.KeyCode == Keys.Oemplus)
            {
                btnZoomIn_Click(null, null);
            }
            else if (e.KeyCode == Keys.OemMinus)
            {
                btnZoomOut_Click(null, null);
            }
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            Settings dlg = new Settings();
            dlg.Owner = this;
            dlg.ShowDialog();
            treePanel1.Invalidate();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            PersonSearch ps = new PersonSearch();
            ps.Tree = _gedtrees;
            ps.Owner = this;
            ps.StartPosition = FormStartPosition.CenterParent;
            var result = ps.ShowDialog();
            if (result == DialogResult.OK)
            {
                SelectPerson(ps.SelectedPerson);
            }
        }

        private void previewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var previewDlg = new EnhancedPrintPreviewDialog();
            previewDlg.ShowPageSettingsButton = true;
            previewDlg.ShowPrinterSettingsButton = true;
            previewDlg.ShowPrinterSettingsBeforePrint = true;

            using (var printBmp = drawToImage())
            {
                var pd = new TreePrintDoc(printBmp);

                previewDlg.OnPrintRangeSet += pd.OnPrintRangeSet;

                if (_pageSettings != null)
                    pd.DefaultPageSettings = _pageSettings;
                if (_printSettings != null)
                    pd.PrinterSettings = _printSettings;

                previewDlg.Document = pd;
                previewDlg.Owner = this;
                previewDlg.StartPosition = FormStartPosition.CenterParent;
                previewDlg.ShowDialog();
            }
        }

        private Bitmap drawToImage()
        {
            return treePanel1.DrawToImage(treePanel1.Width, treePanel1.Height);
        }

        private PageSettings _pageSettings;
        private PrinterSettings _printSettings;

        private void pageSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var pd = new PrintDocument();
            PageSetupDialog psd = new PageSetupDialog();
            psd.Document = pd;
            psd.ShowDialog();

            _pageSettings = pd.DefaultPageSettings;
        }

        private void printerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var pd = new PrintDocument();
            PrintDialog pdlg = new PrintDialog();
            pdlg.AllowSomePages = true; // TODO Need to specify page range somehow
            pdlg.Document = pd;
            pdlg.ShowDialog();

            _printSettings = pd.PrinterSettings;
        }

        private void spinMaxGen_ValueChanged(object sender, EventArgs e)
        {
            // this config changes requires a force rebuild
            personSel_SelectedIndexChanged(null, null); 
        }
    }
}
