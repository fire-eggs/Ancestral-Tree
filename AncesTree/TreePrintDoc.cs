using System;
using System.Drawing;
using System.Drawing.Printing;
using PrintPreview;

namespace AncesTree
{
    public class TreePrintDoc : PrintDocument
    {
        readonly Bitmap _printBmp;
        private int _pageIndex; // actively printing page
        private int _startPage; // first page to print
        private int _endPage;   // last page to print
        private readonly int _totH;
        private readonly int _totW;
        private int _maxPages; // total number of pages

        public TreePrintDoc(Bitmap bmp) : base()
        {
            _printBmp = bmp;
            _totH = bmp.Height;
            _totW = bmp.Width;
            _maxPages = 0;
        }

        protected override void OnBeginPrint(PrintEventArgs e)
        {
            base.OnBeginPrint(e);

            _pageIndex = _startPage == 0 ? 0 : _startPage - 1;
            _maxPages = 0;
        }

        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            base.OnPrintPage(e);

            var printArea = e.MarginBounds;

            // Ideally would be calculated in OnStartPrint, but we aren't
            // given the page bounds at that time!
            int maxW = (int)Math.Ceiling((double)_totW / e.MarginBounds.Width);
            int maxY = (int)Math.Ceiling((double)_totH / e.MarginBounds.Height);
            if (_maxPages == 0)
                _maxPages = maxW * maxY;

            // If user didn't say when to stop, print the whole thing
            int stopPrint = _endPage == 0 ? _maxPages : _endPage;

            int x = _pageIndex % maxW;
            int y = _pageIndex / maxW;

            var thisRect = new Rectangle(x * printArea.Width, y * printArea.Height, printArea.Width, printArea.Height);
            e.Graphics.DrawImage(_printBmp, printArea, thisRect, GraphicsUnit.Pixel);

            _pageIndex++;
            e.HasMorePages = _pageIndex < stopPrint;
        }

        internal void OnPrintRangeSet(object sender, EnhancedPrintPreviewDialog.PageRange newValue)
        {
            // User specified the from-to print range to be
            // printed.
            _startPage = newValue.From;
            _endPage = newValue.To;
        }
    }
}
