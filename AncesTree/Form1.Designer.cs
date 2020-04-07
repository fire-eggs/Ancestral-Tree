namespace AncesTree
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadGEDCOMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recentFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pageSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.personSel = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel1 = new Controls.FlowNoScroll();
            this.treePanel1 = new AncesTree.Controls.TreePanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnZoomIn = new System.Windows.Forms.Button();
            this.btnZoomOut = new System.Windows.Forms.Button();
            this.btn100Percent = new System.Windows.Forms.Button();
            this.btnSaveToImage = new System.Windows.Forms.Button();
            this.btnConfig = new System.Windows.Forms.Button();
            this.spinMaxGen = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinMaxGen)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.printToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(992, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadGEDCOMToolStripMenuItem,
            this.recentFilesToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadGEDCOMToolStripMenuItem
            // 
            this.loadGEDCOMToolStripMenuItem.Name = "loadGEDCOMToolStripMenuItem";
            this.loadGEDCOMToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.loadGEDCOMToolStripMenuItem.Text = "Load GEDCOM...";
            this.loadGEDCOMToolStripMenuItem.Click += new System.EventHandler(this.loadGEDCOMToolStripMenuItem_Click);
            // 
            // recentFilesToolStripMenuItem
            // 
            this.recentFilesToolStripMenuItem.Name = "recentFilesToolStripMenuItem";
            this.recentFilesToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.recentFilesToolStripMenuItem.Text = "Recent Files";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.previewToolStripMenuItem,
            this.pageSettingsToolStripMenuItem,
            this.printerToolStripMenuItem});
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.printToolStripMenuItem.Text = "Print";
            // 
            // previewToolStripMenuItem
            // 
            this.previewToolStripMenuItem.Name = "previewToolStripMenuItem";
            this.previewToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.previewToolStripMenuItem.Text = "Print Preview...";
            this.previewToolStripMenuItem.Click += new System.EventHandler(this.previewToolStripMenuItem_Click);
            // 
            // pageSettingsToolStripMenuItem
            // 
            this.pageSettingsToolStripMenuItem.Name = "pageSettingsToolStripMenuItem";
            this.pageSettingsToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.pageSettingsToolStripMenuItem.Text = "Page Settings...";
            this.pageSettingsToolStripMenuItem.Click += new System.EventHandler(this.pageSettingsToolStripMenuItem_Click);
            // 
            // printerToolStripMenuItem
            // 
            this.printerToolStripMenuItem.Name = "printerToolStripMenuItem";
            this.printerToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.printerToolStripMenuItem.Text = "Printer Setup...";
            this.printerToolStripMenuItem.Click += new System.EventHandler(this.printerToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select person:";
            // 
            // personSel
            // 
            this.personSel.Enabled = false;
            this.personSel.FormattingEnabled = true;
            this.personSel.Location = new System.Drawing.Point(84, 3);
            this.personSel.Name = "personSel";
            this.personSel.Size = new System.Drawing.Size(254, 21);
            this.personSel.TabIndex = 1;
            this.personSel.SelectedIndexChanged += new System.EventHandler(this.personSel_SelectedIndexChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.treePanel1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 38);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(986, 477);
            this.flowLayoutPanel1.TabIndex = 3;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // treePanel1
            // 
            this.treePanel1.BackColor = System.Drawing.Color.OldLace;
            this.treePanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treePanel1.Location = new System.Drawing.Point(3, 3);
            this.treePanel1.Name = "treePanel1";
            this.treePanel1.Size = new System.Drawing.Size(643, 330);
            this.treePanel1.TabIndex = 0;
            this.treePanel1.TreeMargin = 10;
            this.treePanel1.Zoom = 1F;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Controls.Add(this.label1);
            this.flowLayoutPanel2.Controls.Add(this.personSel);
            this.flowLayoutPanel2.Controls.Add(this.btnSearch);
            this.flowLayoutPanel2.Controls.Add(this.btnZoomIn);
            this.flowLayoutPanel2.Controls.Add(this.btnZoomOut);
            this.flowLayoutPanel2.Controls.Add(this.btn100Percent);
            this.flowLayoutPanel2.Controls.Add(this.btnSaveToImage);
            this.flowLayoutPanel2.Controls.Add(this.btnConfig);
            this.flowLayoutPanel2.Controls.Add(this.spinMaxGen);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(727, 29);
            this.flowLayoutPanel2.TabIndex = 2;
            this.flowLayoutPanel2.WrapContents = false;
            // 
            // btnSearch
            // 
            this.btnSearch.Image = global::AncesTree.Properties.Resources.icons8_search_20;
            this.btnSearch.Location = new System.Drawing.Point(344, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(34, 23);
            this.btnSearch.TabIndex = 7;
            this.toolTip1.SetToolTip(this.btnSearch, "Search for person");
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnZoomIn.Location = new System.Drawing.Point(384, 3);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(38, 23);
            this.btnZoomIn.TabIndex = 2;
            this.btnZoomIn.Text = "+";
            this.btnZoomIn.UseVisualStyleBackColor = true;
            this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnZoomOut.Location = new System.Drawing.Point(428, 3);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(39, 23);
            this.btnZoomOut.TabIndex = 3;
            this.btnZoomOut.Text = "-";
            this.btnZoomOut.UseVisualStyleBackColor = true;
            this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
            // 
            // btn100Percent
            // 
            this.btn100Percent.Location = new System.Drawing.Point(473, 3);
            this.btn100Percent.Name = "btn100Percent";
            this.btn100Percent.Size = new System.Drawing.Size(38, 23);
            this.btn100Percent.TabIndex = 4;
            this.btn100Percent.Text = "0";
            this.btn100Percent.UseVisualStyleBackColor = true;
            this.btn100Percent.Click += new System.EventHandler(this.btn100Percent_Click);
            // 
            // btnSaveToImage
            // 
            this.btnSaveToImage.Location = new System.Drawing.Point(517, 3);
            this.btnSaveToImage.Name = "btnSaveToImage";
            this.btnSaveToImage.Size = new System.Drawing.Size(75, 23);
            this.btnSaveToImage.TabIndex = 5;
            this.btnSaveToImage.Text = "To Image";
            this.btnSaveToImage.UseVisualStyleBackColor = true;
            this.btnSaveToImage.Click += new System.EventHandler(this.btnToImage_Click);
            // 
            // btnConfig
            // 
            this.btnConfig.Location = new System.Drawing.Point(598, 3);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(75, 23);
            this.btnConfig.TabIndex = 6;
            this.btnConfig.Text = "Config";
            this.btnConfig.UseVisualStyleBackColor = true;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // spinMaxGen
            // 
            this.spinMaxGen.Location = new System.Drawing.Point(679, 3);
            this.spinMaxGen.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinMaxGen.Name = "spinMaxGen";
            this.spinMaxGen.Size = new System.Drawing.Size(45, 20);
            this.spinMaxGen.TabIndex = 8;
            this.spinMaxGen.Value = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.spinMaxGen.ValueChanged += new System.EventHandler(this.spinMaxGen_ValueChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(992, 518);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(992, 542);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "View Ancestors Tree";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinMaxGen)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadGEDCOMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recentFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox personSel;
        private Controls.FlowNoScroll flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnZoomIn;
        private System.Windows.Forms.Button btnZoomOut;
        private System.Windows.Forms.Button btn100Percent;
        private System.Windows.Forms.Button btnSaveToImage;
        private System.Windows.Forms.Button btnConfig;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem previewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pageSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printerToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown spinMaxGen;
        private Controls.TreePanel treePanel1;
    }
}

