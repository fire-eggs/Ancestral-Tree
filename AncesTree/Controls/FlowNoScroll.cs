using System.Windows.Forms;

// A temp. fix for clicking in the TreePanel causes it to scroll. From:
// https://stackoverflow.com/questions/419774/how-can-you-stop-a-winforms-panel-from-scrolling
// TODO merge this into TreePanel or add scrollbars to TreePanel

namespace AncesTree.Controls
{
    class FlowNoScroll : FlowLayoutPanel
    {
        protected override System.Drawing.Point ScrollToControl(Control activeControl)
        {
            // When there's only 1 control in the panel and the user clicks
            //  on it, .NET tries to scroll to the control. This invariably
            //  forces the panel to scroll up. This little hack prevents that.

            return DisplayRectangle.Location;
        }

    }
}
