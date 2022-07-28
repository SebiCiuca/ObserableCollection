using System.Windows;
using System.Windows.Controls;

namespace ObserableCollection.LineDesigner
{
    public class ResizeRotateLineChrome : Control
    {
        static ResizeRotateLineChrome()
        {
            DefaultStyleKeyProperty
                .OverrideMetadata(typeof(ResizeRotateLineChrome), new FrameworkPropertyMetadata(typeof(ResizeRotateLineChrome)));
        }
    }
}
