using System.ComponentModel;
using System.Drawing;

// TODO implement IDisposable to clean up brush

namespace PrintPreview
{
    public enum TextPosition
    {
        HTopLeft, HTopCenter, HTopRight,
        HBottomLeft, HBottomCenter, HBottomRight,
        VTopLeft, VMiddleLeft, VBottomLeft,
        VTopRight, VMiddleRight, VBottomRight,
        WaterMark
    }

    public class AdditionalText
    {
        private Brush _brush;

        public string Text { get; set; }

        public Font Font { get; set; }

        [Browsable(false)]
        public Brush Brush
        {
            get { return _brush; }
            set { _brush = value; }
        }

        public TextPosition Position { get; set; }

        public int OffsetX { get; set; }

        public int OffsetY { get; set; }

        public Color Color
        {
            get { return (_brush is SolidBrush) ? ((SolidBrush)_brush).Color : Color.Black; }
            set { _brush = new SolidBrush(value); }
        }


        public AdditionalText(string text, Font font, Brush brush, TextPosition position, int offsetX, int offsetY)
        {
            Text = text;
            Font = font ?? new Font("Arial", 12f);
            _brush = brush ?? Brushes.Gray;
            Position = position;
            OffsetX = offsetX;
            OffsetY = offsetY;
        }

        public AdditionalText(string text, TextPosition position) : this(text, null, null, position, 0, 0) { }

        public AdditionalText(string text, TextPosition position, int offsetX, int offsetY) : this(text, null, null, position, offsetX, offsetY) { }

        public AdditionalText(string text) : this(text, null, null, TextPosition.HBottomCenter, 0, 0) { }

    }
}
