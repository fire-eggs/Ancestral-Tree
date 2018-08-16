using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace PrintPreview
{

    public partial class EnhancedPrintPreviewDialog : Form
    {

        #region PRIVATE FIELDS

        PrintDocument mDocument;
        PageSetupDialog mPageSetupDialog;
        PrintDialog mPrintDialog;
        int mVisibilePages = 1;
        int mTotalPages = 0;
        bool mShowPageSettingsButton = true;
        bool mShowPrinterSettingsButton = false;
        List<AdditionalText> mAdditionalTextList;

        #endregion

        public EnhancedPrintPreviewDialog()
        {
            ShowPrinterSettingsBeforePrint = true;
            InitializeComponent();
            printPreviewControl1.StartPageChanged += printPreviewControl1_StartPageChanged;
            ShowPrinterSettingsButton = false;
            PrintDialog.UseEXDialog = true; // dialog won't appear on 64-bit unless this is set
        }

        #region PROPERTIES

        public List<AdditionalText> AdditionalTextList
        {
            get { return mAdditionalTextList ?? (mAdditionalTextList = new List<AdditionalText>()); }
            set { mAdditionalTextList = value; }
        }

        public PageSetupDialog PageSetupDialog
        {
            get { return mPageSetupDialog ?? (mPageSetupDialog = new PageSetupDialog()); }
        }

        public PrintDialog PrintDialog
        {
            get { return mPrintDialog ?? (mPrintDialog = new PrintDialog()); }
        }

        public bool ShowPrinterSettingsButton
        {
            get { return mShowPrinterSettingsButton; }
            set
            {
                mShowPrinterSettingsButton = value;
                tsBtnPrinterSettings.Visible = value;
            }
        }

        public bool ShowPageSettingsButton
        {
            get { return mShowPageSettingsButton; }
            set
            {
                mShowPageSettingsButton = value;
                tsBtnPageSettings.Visible = value;
            }
        }

        public bool ShowPrinterSettingsBeforePrint { get; set; }

        public PrintPreviewControl PrintPreviewControl { get { return printPreviewControl1; } }

        public PrintDocument Document
        {
            get { return mDocument; }
            set
            {
                SwitchPrintDocumentHandlers(mDocument, false);
                mDocument = value;
                SwitchPrintDocumentHandlers(mDocument, true);
                printPreviewControl1.Document = mDocument;
                PossiblePrinterChange();
            }
        }

        public bool UseAntiAlias
        {
            get { return printPreviewControl1.UseAntiAlias; }
            set { printPreviewControl1.UseAntiAlias = value; }
        }

        #endregion


        #region DOCUMENT EVENT HANDLERS


        void mDocument_BeginPrint(object sender, PrintEventArgs e)
        {
            mTotalPages = 0;
        }

        void mDocument_EndPrint(object sender, PrintEventArgs e)
        {
            tsLblTotalPages.Text = " / " + mTotalPages;
        }

        void mDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            mTotalPages++;
            if (mAdditionalTextList == null) return;

            PointF point = new PointF();

            foreach (AdditionalText at in mAdditionalTextList)
            {
                string text = at.Text.Replace("$pagenumber", mTotalPages.ToString());
                SizeF measure = e.Graphics.MeasureString(text, at.Font);
                Brush brush = at.Brush;
                StringFormat format = new StringFormat();

                switch (at.Position)
                {
                    case TextPosition.HTopLeft:
                        point.Y = e.MarginBounds.Top - measure.Height;
                        point.X = e.MarginBounds.Left;
                        break;
                    case TextPosition.HTopCenter:
                        point.Y = e.MarginBounds.Top - measure.Height;
                        point.X = e.MarginBounds.Left + (e.MarginBounds.Width - measure.Width) / 2;
                        break;
                    case TextPosition.HTopRight:
                        point.Y = e.MarginBounds.Top - measure.Height;
                        point.X = e.MarginBounds.Right - measure.Width;
                        break;
                    case TextPosition.HBottomLeft:
                        point.Y = e.MarginBounds.Bottom;
                        point.X = e.MarginBounds.Left;
                        break;
                    case TextPosition.HBottomCenter:
                        point.Y = e.MarginBounds.Bottom;
                        point.X = e.MarginBounds.Left + (e.MarginBounds.Width - measure.Width) / 2;
                        break;
                    case TextPosition.HBottomRight:
                        point.Y = e.MarginBounds.Bottom;
                        point.X = e.MarginBounds.Right - measure.Width;
                        break;
                    case TextPosition.VTopLeft:
                        format.FormatFlags = StringFormatFlags.DirectionVertical;
                        point.Y = e.MarginBounds.Top;
                        point.X = e.MarginBounds.Left - measure.Height;
                        break;
                    case TextPosition.VMiddleLeft:
                        format.FormatFlags = StringFormatFlags.DirectionVertical;
                        point.X = e.MarginBounds.Left - measure.Height;
                        point.Y = e.MarginBounds.Top + (e.MarginBounds.Height - measure.Width) / 2;

                        break;
                    case TextPosition.VBottomLeft:
                        format.FormatFlags = StringFormatFlags.DirectionVertical;
                        point.X = e.MarginBounds.Left - measure.Height;
                        point.Y = e.MarginBounds.Bottom - measure.Width;
                        break;
                    case TextPosition.VTopRight:
                        format.FormatFlags = StringFormatFlags.DirectionVertical;
                        point.Y = e.MarginBounds.Top;
                        point.X = e.MarginBounds.Right;
                        break;
                    case TextPosition.VMiddleRight:
                        format.FormatFlags = StringFormatFlags.DirectionVertical;
                        point.Y = e.MarginBounds.Top + (e.MarginBounds.Height - measure.Width) / 2;
                        point.X = e.MarginBounds.Right;
                        break;
                    case TextPosition.VBottomRight:
                        format.FormatFlags = StringFormatFlags.DirectionVertical;
                        point.Y = e.MarginBounds.Bottom - measure.Width;
                        point.X = e.MarginBounds.Right;
                        break;
                    case TextPosition.WaterMark:
                        point.Y = e.MarginBounds.Top + (e.MarginBounds.Height - measure.Height) / 2;
                        point.X = e.MarginBounds.Left + (e.MarginBounds.Width - measure.Width) / 2;
                        point.X += at.OffsetX;
                        point.Y += at.OffsetY;
                        int TranslationX = (int)(point.X + measure.Width / 2);
                        int TranslationY = (int)(point.Y + measure.Height / 2);
                        e.Graphics.TranslateTransform(TranslationX, TranslationY);
                        e.Graphics.RotateTransform(-55f);
                        point.X = -measure.Width / 2;
                        point.Y = -measure.Height / 2;
                        if (at.Brush is SolidBrush && ((SolidBrush)at.Brush).Color.A == 255)
                            brush = new SolidBrush(Color.FromArgb(100, ((SolidBrush)at.Brush).Color));

                        e.Graphics.DrawString(text, at.Font, brush, point, format);
                        e.Graphics.RotateTransform(55f);
                        e.Graphics.TranslateTransform(-TranslationX, -TranslationY);

                        break;
                    default:
                        break;
                }


                if (at.Position != TextPosition.WaterMark)
                {
                    point.X += at.OffsetX;
                    point.Y += at.OffsetY;
                    e.Graphics.DrawString(text, at.Font, brush, point, format);
                }
            }
        }

        #endregion


        #region TOOLBAR EVENT HANDLERS


        private void tsTxtCurrentPage_Leave(object sender, EventArgs e)
        {
            int startPage;
            if (Int32.TryParse(tsTxtCurrentPage.Text, out startPage))
            {
                try
                {
                    startPage--;
                    if (startPage < 0) startPage = 0;
                    if (startPage > mTotalPages - 1) startPage = mTotalPages - mVisibilePages;
                    printPreviewControl1.StartPage = startPage;
                }
                catch { }
            }
        }

        private void NumOfPages_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem mnuitem = (ToolStripMenuItem)sender;
            tsDDownPages.Image = mnuitem.Image;
            mVisibilePages = Int32.Parse((string)mnuitem.Tag);
            switch (mVisibilePages)
            {
                case 1:
                    printPreviewControl1.Rows = 1;
                    printPreviewControl1.Columns = 1;
                    break;
                case 2:
                    printPreviewControl1.Rows = 1;
                    printPreviewControl1.Columns = 2;
                    break;
                case 4:
                    printPreviewControl1.Rows = 2;
                    printPreviewControl1.Columns = 2;
                    break;
                case 6:
                    printPreviewControl1.Rows = 2;
                    printPreviewControl1.Columns = 3;
                    break;
                case 8:
                    printPreviewControl1.Rows = 2;
                    printPreviewControl1.Columns = 4;
                    break;
            }
            tsBtnZoom_Click(null, null);
        }

        // Manages the next and previous button 
        private void Navigate_Click(object sender, EventArgs e)
        {
            ToolStripButton btn = (ToolStripButton)sender;
            int startPage = printPreviewControl1.StartPage;
            try
            {
                if (btn.Name == "tsBtnNext")
                {
                    startPage += mVisibilePages;
                }
                else
                {
                    startPage -= mVisibilePages;
                }
                if (startPage < 0) startPage = 0;
                if (startPage > mTotalPages - 1) startPage = mTotalPages - mVisibilePages;
                printPreviewControl1.StartPage = startPage;

            }
            catch { }
        }

        void printPreviewControl1_StartPageChanged(object sender, EventArgs e)
        {
            int tmp = printPreviewControl1.StartPage + 1;
            tsTxtCurrentPage.Text = tmp.ToString();
        }

        private void tsComboZoom_Leave(object sender, EventArgs e)
        {
            if (tsComboZoom.SelectedIndex == 0)
            {
                printPreviewControl1.AutoZoom = true;
                return;
            }
            string sZoomVal = tsComboZoom.Text.Replace("%", "");
            double zoomval;
            if (double.TryParse(sZoomVal, out zoomval))
            {
                try
                {
                    printPreviewControl1.Zoom = zoomval / 100;
                }
                catch { }
                zoomval = (printPreviewControl1.Zoom * 100);
                tsComboZoom.Text = zoomval + "%";
            }
        }

        private void tsBtnZoom_Click(object sender, EventArgs e)
        {
            tsComboZoom.SelectedIndex = 0;
            tsComboZoom_Leave(null, null);
        }

        private void tsComboZoom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                tsComboZoom_Leave(null, null);
            }
        }

        public delegate void PrintRangeSet(object sender, PageRange newValue);
        public event PrintRangeSet OnPrintRangeSet;

        public class PageRange
        {
            public int From { get; set; }
            public int To { get; set; }

            public PageRange( int fr, int to )
            {
                From = fr;
                To = to;
            }
        }

        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            if (ShowPrinterSettingsBeforePrint)
            {
                var pdlg = PrintDialog;

                pdlg.Document = mDocument;
                pdlg.AllowSomePages = true; // KBR allow user to specify page range
                pdlg.PrinterSettings.FromPage = 1;
                pdlg.PrinterSettings.ToPage = mTotalPages;

                if (pdlg.ShowDialog() != DialogResult.OK)
                    return;

                // Cute PDF and doPDF 'printers' don't respect the print range values.
                // They *should* be passed along to the PrintPage method via the
                // printer settings.
                // Other printers (e.g. Brother) do pass along the print range values,
                // except the values should ideally be available at BeginPrint, so
                // the program can initialize the page index.
                // So deal with both these issues by raising our own 'print range'
                // event.

                if (OnPrintRangeSet != null)
                {
                    if (pdlg.PrinterSettings.PrintRange == PrintRange.SomePages)
                        OnPrintRangeSet(this, new PageRange(pdlg.PrinterSettings.FromPage, pdlg.PrinterSettings.ToPage));
                    else
                        OnPrintRangeSet(this, new PageRange(0, 0));
                }
            }
            try
            {
                mDocument.Print();
            }
            catch { }
        }

        private void PossiblePrinterChange()
        {
            if (mDocument == null)
                return; // not set up yet?
            printPreviewControl1.InvalidatePreview();
            Text = string.Format("Print Preview: {0} [{1}]", mDocument.DocumentName,
                mDocument.PrinterSettings.PrinterName);
        }

        private void tsBtnPageSettings_Click(object sender, EventArgs e)
        {
            PageSetupDialog.Document = mDocument;
            if (PageSetupDialog.ShowDialog() == DialogResult.OK)
            {
                //PossiblePrinterChange(); 
                printPreviewControl1.InvalidatePreview();
            }
        }

        private void tsBtnPrinterSettings_Click(object sender, EventArgs e)
        {
            PrintDialog.Document = mDocument;
            DialogResult result = PrintDialog.ShowDialog();
            Document = mDocument;
            //PossiblePrinterChange(); //printPreviewControl1.InvalidatePreview(); // KBR user may have changed printers
        }

        #endregion


        private void RadPrintPreview_FormClosing(object sender, FormClosingEventArgs e)
        {
            SwitchPrintDocumentHandlers(mDocument, false);
        }

        private void SwitchPrintDocumentHandlers(PrintDocument doc, bool attach)
        {
            if (doc == null) return;
            if (attach)
            {
                mDocument.BeginPrint += mDocument_BeginPrint;
                mDocument.PrintPage += mDocument_PrintPage;
                mDocument.EndPrint += mDocument_EndPrint;
            }
            else
            {
                mDocument.BeginPrint -= mDocument_BeginPrint;
                mDocument.PrintPage -= mDocument_PrintPage;
                mDocument.EndPrint -= mDocument_EndPrint;
            }
        }

    }
}