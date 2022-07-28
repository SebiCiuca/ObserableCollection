using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System;

namespace ObserableCollection.LineDesigner
{
    public class MoveThumb : Thumb
    {
        private RotateTransform rotateTransform;
        private ContentControl designerItem;

        public MoveThumb()
        {
            DragStarted += new DragStartedEventHandler(MoveThumb_DragStarted);
            DragDelta += new DragDeltaEventHandler(MoveThumb_DragDelta);
        }

        private void MoveThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            designerItem = DataContext as ContentControl;

            ContentControl userControl = designerItem.Parent as ContentControl;

            if (designerItem != null)
            {
                rotateTransform = userControl.RenderTransform as RotateTransform;
            }
        }

        private void MoveThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            ContentControl userControl = designerItem.Parent as ContentControl;

            if (userControl != null)
            {
                Point dragDelta = new(e.HorizontalChange, e.VerticalChange);

                if (rotateTransform != null)
                {
                    dragDelta = rotateTransform.Transform(dragDelta);
                }

                Canvas.SetLeft(userControl, Canvas.GetLeft(userControl) + dragDelta.X);
                Canvas.SetTop(userControl, Canvas.GetTop(userControl) + dragDelta.Y);
            }
        }
    }
}
