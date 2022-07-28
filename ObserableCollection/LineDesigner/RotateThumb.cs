using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace ObserableCollection.LineDesigner
{
    public class RotateThumb : Thumb
    {
        private Point centerPoint;
        private Vector startVector;
        private double initialAngle;
        private Canvas designerCanvas;
        private ContentControl designerItem;
        private RotateTransform rotateTransform;

        public RotateThumb()
        {
            DragDelta += new DragDeltaEventHandler(RotateThumb_DragDelta);
            DragStarted += new DragStartedEventHandler(RotateThumb_DragStarted);
        }

        void RotateThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            designerItem = DataContext as ContentControl;

            ContentControl userControl = designerItem.Parent as ContentControl;

            if (userControl != null)
            {
                designerCanvas = VisualTreeHelper.GetParent(userControl) as Canvas;

                if (designerCanvas != null)
                {
                    centerPoint = userControl.TranslatePoint(
                        new Point(designerItem.Width * userControl.RenderTransformOrigin.X,
                                  designerItem.Height * userControl.RenderTransformOrigin.Y),
                                  designerCanvas);

                    Point startPoint = Mouse.GetPosition(designerCanvas);
                    startVector = Point.Subtract(startPoint, centerPoint);

                    rotateTransform = userControl.RenderTransform as RotateTransform;
                    if (rotateTransform == null)
                    {
                        userControl.RenderTransform = new RotateTransform(0);
                        initialAngle = 0;
                    }
                    else
                    {
                        initialAngle = rotateTransform.Angle;
                    }
                }
            }
        }

        private void RotateThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            ContentControl userControl = designerItem.Parent as ContentControl;
            if (userControl != null && designerCanvas != null)
            {
                Point currentPoint = Mouse.GetPosition(designerCanvas);
                Vector deltaVector = Point.Subtract(currentPoint, centerPoint);

                double angle = Vector.AngleBetween(startVector, deltaVector);

                RotateTransform rotateTransform = userControl.RenderTransform as RotateTransform;
                rotateTransform.Angle = initialAngle + Math.Round(angle, 0);
                userControl.InvalidateMeasure();
            }
        }
    }
}
