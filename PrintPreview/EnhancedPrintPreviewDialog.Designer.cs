namespace PrintPreview {
    partial class EnhancedPrintPreviewDialog {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnhancedPrintPreviewDialog));
            this.printPreviewControl1 = new System.Windows.Forms.PrintPreviewControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsBtnPrint = new System.Windows.Forms.ToolStripButton();
            this.tsBtnPrinterSettings = new System.Windows.Forms.ToolStripButton();
            this.tsBtnPageSettings = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.tsDDownPages = new System.Windows.Forms.ToolStripDropDownButton();
            this.pageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pagesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pagesToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.pagesToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnZoom = new System.Windows.Forms.ToolStripButton();
            this.tsComboZoom = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnNext = new System.Windows.Forms.ToolStripButton();
            this.tsBtnPrev = new System.Windows.Forms.ToolStripButton();
            this.tsLblTotalPages = new System.Windows.Forms.ToolStripLabel();
            this.tsTxtCurrentPage = new System.Windows.Forms.ToolStripTextBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // printPreviewControl1
            // 
            resources.ApplyResources(this.printPreviewControl1, "printPreviewControl1");
            this.printPreviewControl1.Name = "printPreviewControl1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnPrint,
            this.tsBtnPrinterSettings,
            this.tsBtnPageSettings,
            this.toolStripSeparator,
            this.tsDDownPages,
            this.toolStripSeparator1,
            this.tsBtnZoom,
            this.tsComboZoom,
            this.toolStripSeparator2,
            this.tsBtnNext,
            this.tsBtnPrev,
            this.tsLblTotalPages,
            this.tsTxtCurrentPage});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // tsBtnPrint
            // 
            this.tsBtnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsBtnPrint, "tsBtnPrint");
            this.tsBtnPrint.Name = "tsBtnPrint";
            this.tsBtnPrint.Click += new System.EventHandler(this.tsBtnPrint_Click);
            // 
            // tsBtnPrinterSettings
            // 
            this.tsBtnPrinterSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsBtnPrinterSettings, "tsBtnPrinterSettings");
            this.tsBtnPrinterSettings.Name = "tsBtnPrinterSettings";
            this.tsBtnPrinterSettings.Click += new System.EventHandler(this.tsBtnPrinterSettings_Click);
            // 
            // tsBtnPageSettings
            // 
            this.tsBtnPageSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsBtnPageSettings, "tsBtnPageSettings");
            this.tsBtnPageSettings.Name = "tsBtnPageSettings";
            this.tsBtnPageSettings.Click += new System.EventHandler(this.tsBtnPageSettings_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            resources.ApplyResources(this.toolStripSeparator, "toolStripSeparator");
            // 
            // tsDDownPages
            // 
            this.tsDDownPages.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsDDownPages.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pageToolStripMenuItem,
            this.pagesToolStripMenuItem,
            this.pagesToolStripMenuItem1,
            this.pagesToolStripMenuItem2,
            this.pagesToolStripMenuItem3});
            resources.ApplyResources(this.tsDDownPages, "tsDDownPages");
            this.tsDDownPages.Name = "tsDDownPages";
            this.tsDDownPages.Tag = "1";
            // 
            // pageToolStripMenuItem
            // 
            resources.ApplyResources(this.pageToolStripMenuItem, "pageToolStripMenuItem");
            this.pageToolStripMenuItem.Name = "pageToolStripMenuItem";
            this.pageToolStripMenuItem.Tag = "1";
            this.pageToolStripMenuItem.Click += new System.EventHandler(this.NumOfPages_Click);
            // 
            // pagesToolStripMenuItem
            // 
            resources.ApplyResources(this.pagesToolStripMenuItem, "pagesToolStripMenuItem");
            this.pagesToolStripMenuItem.Name = "pagesToolStripMenuItem";
            this.pagesToolStripMenuItem.Tag = "2";
            this.pagesToolStripMenuItem.Click += new System.EventHandler(this.NumOfPages_Click);
            // 
            // pagesToolStripMenuItem1
            // 
            resources.ApplyResources(this.pagesToolStripMenuItem1, "pagesToolStripMenuItem1");
            this.pagesToolStripMenuItem1.Name = "pagesToolStripMenuItem1";
            this.pagesToolStripMenuItem1.Tag = "4";
            this.pagesToolStripMenuItem1.Click += new System.EventHandler(this.NumOfPages_Click);
            // 
            // pagesToolStripMenuItem2
            // 
            resources.ApplyResources(this.pagesToolStripMenuItem2, "pagesToolStripMenuItem2");
            this.pagesToolStripMenuItem2.Name = "pagesToolStripMenuItem2";
            this.pagesToolStripMenuItem2.Tag = "6";
            this.pagesToolStripMenuItem2.Click += new System.EventHandler(this.NumOfPages_Click);
            // 
            // pagesToolStripMenuItem3
            // 
            resources.ApplyResources(this.pagesToolStripMenuItem3, "pagesToolStripMenuItem3");
            this.pagesToolStripMenuItem3.Name = "pagesToolStripMenuItem3";
            this.pagesToolStripMenuItem3.Tag = "8";
            this.pagesToolStripMenuItem3.Click += new System.EventHandler(this.NumOfPages_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // tsBtnZoom
            // 
            resources.ApplyResources(this.tsBtnZoom, "tsBtnZoom");
            this.tsBtnZoom.Margin = new System.Windows.Forms.Padding(40, 1, 0, 2);
            this.tsBtnZoom.Name = "tsBtnZoom";
            this.tsBtnZoom.Click += new System.EventHandler(this.tsBtnZoom_Click);
            // 
            // tsComboZoom
            // 
            this.tsComboZoom.Items.AddRange(new object[] {
            resources.GetString("tsComboZoom.Items"),
            resources.GetString("tsComboZoom.Items1"),
            resources.GetString("tsComboZoom.Items2"),
            resources.GetString("tsComboZoom.Items3"),
            resources.GetString("tsComboZoom.Items4"),
            resources.GetString("tsComboZoom.Items5"),
            resources.GetString("tsComboZoom.Items6"),
            resources.GetString("tsComboZoom.Items7"),
            resources.GetString("tsComboZoom.Items8"),
            resources.GetString("tsComboZoom.Items9"),
            resources.GetString("tsComboZoom.Items10")});
            this.tsComboZoom.Name = "tsComboZoom";
            resources.ApplyResources(this.tsComboZoom, "tsComboZoom");
            this.tsComboZoom.SelectedIndexChanged += new System.EventHandler(this.tsComboZoom_Leave);
            this.tsComboZoom.Leave += new System.EventHandler(this.tsComboZoom_Leave);
            this.tsComboZoom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tsComboZoom_KeyPress);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // tsBtnNext
            // 
            this.tsBtnNext.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsBtnNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsBtnNext, "tsBtnNext");
            this.tsBtnNext.Name = "tsBtnNext";
            this.tsBtnNext.Tag = "next";
            this.tsBtnNext.Click += new System.EventHandler(this.Navigate_Click);
            // 
            // tsBtnPrev
            // 
            this.tsBtnPrev.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsBtnPrev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsBtnPrev, "tsBtnPrev");
            this.tsBtnPrev.Name = "tsBtnPrev";
            this.tsBtnPrev.Tag = "prev";
            this.tsBtnPrev.Click += new System.EventHandler(this.Navigate_Click);
            // 
            // tsLblTotalPages
            // 
            this.tsLblTotalPages.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsLblTotalPages.Name = "tsLblTotalPages";
            resources.ApplyResources(this.tsLblTotalPages, "tsLblTotalPages");
            // 
            // tsTxtCurrentPage
            // 
            this.tsTxtCurrentPage.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsTxtCurrentPage.Name = "tsTxtCurrentPage";
            resources.ApplyResources(this.tsTxtCurrentPage, "tsTxtCurrentPage");
            this.tsTxtCurrentPage.Leave += new System.EventHandler(this.tsTxtCurrentPage_Leave);
            // 
            // RadPrintPreview
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.printPreviewControl1);
            this.MinimizeBox = false;
            this.Name = "RadPrintPreview";
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RadPrintPreview_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PrintPreviewControl printPreviewControl1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsBtnPrint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton tsDDownPages;
        private System.Windows.Forms.ToolStripMenuItem pageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pagesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem pagesToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem pagesToolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsBtnNext;
        private System.Windows.Forms.ToolStripButton tsBtnPrev;
        private System.Windows.Forms.ToolStripLabel tsLblTotalPages;
        private System.Windows.Forms.ToolStripTextBox tsTxtCurrentPage;
        private System.Windows.Forms.ToolStripComboBox tsComboZoom;
        private System.Windows.Forms.ToolStripButton tsBtnPageSettings;
        private System.Windows.Forms.ToolStripButton tsBtnPrinterSettings;
        private System.Windows.Forms.ToolStripButton tsBtnZoom;
    }
}