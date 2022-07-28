using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace ObserableCollection.LineDesigner
{
    public class ResizeRotateLineAdorner : Adorner
    {
        private VisualCollection visuals;
        private ResizeRotateLineChrome chrome;

        protected override int VisualChildrenCount
        {
            get
            {
                return visuals.Count;
            }
        }

        public ResizeRotateLineAdorner(ContentControl designerItem)
            : base(designerItem)
        {
            SnapsToDevicePixels = true;
            chrome = new ResizeRotateLineChrome();
            chrome.DataContext = designerItem;
            visuals = new VisualCollection(this);
            visuals.Add(chrome);
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            chrome.Arrange(new Rect(arrangeBounds));
            return arrangeBounds;
        }

        protected override Visual GetVisualChild(int index)
        {
            return visuals[index];
        }
    }
}
