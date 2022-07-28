using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;

namespace ObserableCollection.LineDesigner
{
    public class ResizeThumb : Thumb
    {
        private RotateTransform rotateTransform;
        private double angle;
        private Adorner adorner;
        private Point transformOrigin;
        private ContentControl designerItem;
        private Canvas canvas;

        public ResizeThumb()
        {
            DragStarted += new DragStartedEventHandler(ResizeThumb_DragStarted);
            DragDelta += new DragDeltaEventHandler(ResizeThumb_DragDelta);
            DragCompleted += new DragCompletedEventHandler(ResizeThumb_DragCompleted);
        }

        private void ResizeThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            designerItem = DataContext as ContentControl;

            ContentControl userControl = designerItem.Parent as ContentControl;

            if (userControl != null)
            {
                canvas = VisualTreeHelper.GetParent(userControl) as Canvas;

                if (canvas != null)
                {
                    transformOrigin = userControl.RenderTransformOrigin;

                    rotateTransform = designerItem.RenderTransform as RotateTransform;
                    var transformGroup = designerItem.RenderTransform as TransformGroup;
                    if (rotateTransform != null)
                    {
                        angle = rotateTransform.Angle * Math.PI / 180.0;
                    }
                    else
                    {
                        angle = 0.0d;
                    }

                    AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(canvas);
                    if (adornerLayer != null)
                    {
                        adorner = new SizeAdorner(userControl);
                        adornerLayer.Add(adorner);
                    }
                }
            }
        }

        private void ResizeThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            ContentControl userControl = designerItem.Parent as ContentControl;

            if (userControl != null)
            {
                double deltaVertical, deltaHorizontal;

                switch (VerticalAlignment)
                {
                    case VerticalAlignment.Bottom:
                        deltaVertical = Math.Min(-e.VerticalChange, userControl.ActualHeight - userControl.MinHeight);
                        Canvas.SetTop(userControl, Canvas.GetTop(userControl) + transformOrigin.Y * deltaVertical * (1 - Math.Cos(-angle)));
                        Canvas.SetLeft(userControl, Canvas.GetLeft(userControl) - deltaVertical * transformOrigin.Y * Math.Sin(-angle));
                        userControl.Height -= deltaVertical;
                        designerItem.Height -= deltaVertical;
                        break;
                    case VerticalAlignment.Top:
                        deltaVertical = Math.Min(e.VerticalChange, userControl.ActualHeight - userControl.MinHeight);
                        Canvas.SetTop(userControl, Canvas.GetTop(userControl) + deltaVertical * Math.Cos(-angle) + transformOrigin.Y * deltaVertical * (1 - Math.Cos(-angle)));
                        Canvas.SetLeft(userControl, Canvas.GetLeft(userControl) + deltaVertical * Math.Sin(-angle) - transformOrigin.Y * deltaVertical * Math.Sin(-angle));
                        userControl.Height -= deltaVertical;
                        designerItem.Height -= deltaVertical;
                        break;
                    default:
                        break;
                }

                switch (HorizontalAlignment)
                {
                    case HorizontalAlignment.Left:
                        deltaHorizontal = Math.Min(e.HorizontalChange, userControl.ActualWidth - userControl.MinWidth);
                        deltaVertical = Math.Min(-e.VerticalChange, userControl.ActualHeight - userControl.MinHeight);
                        System.Diagnostics.Debug.WriteLine($"Horizonal Delta {deltaHorizontal}");
                        System.Diagnostics.Debug.WriteLine($"Vertical Delta {deltaVertical}");

                        var userControlTop = Canvas.GetTop(userControl) + deltaHorizontal * Math.Sin(angle) - transformOrigin.X * deltaHorizontal * Math.Sin(angle);
                        Canvas.SetTop(userControl, userControlTop);
                        //System.Diagnostics.Debug.WriteLine($"User control top: {userControlTop}");
                        var userCntrolLeft = Canvas.GetLeft(userControl) + deltaHorizontal * Math.Cos(angle) + transformOrigin.X * deltaHorizontal * (1 - Math.Cos(angle));

                        //System.Diagnostics.Debug.WriteLine($"User control top: {userCntrolLeft}");
                        Canvas.SetLeft(userControl, userCntrolLeft);
                        userControl.Width -= deltaHorizontal;
                        designerItem.Width -= deltaHorizontal;
                        break;

                    case HorizontalAlignment.Right:
                        deltaHorizontal = Math.Min(-e.HorizontalChange, userControl.ActualWidth - userControl.MinWidth);
                        deltaVertical = Math.Min(-e.VerticalChange, userControl.ActualHeight - userControl.MinHeight);
                        System.Diagnostics.Debug.WriteLine($"HorizonalDelta {deltaHorizontal}");
                        System.Diagnostics.Debug.WriteLine($"Vertical Delta {deltaVertical}");

                        var userControlTopRightMove = Canvas.GetTop(userControl) - transformOrigin.X * deltaHorizontal * Math.Sin(angle);
                        var userCntrolLeftRightMove = Canvas.GetLeft(userControl) + deltaHorizontal * transformOrigin.X * (1 - Math.Cos(angle));

                        //System.Diagnostics.Debug.WriteLine($"User control top: {userControlTopRightMove}");
                        //System.Diagnostics.Debug.WriteLine($"User control top: {userCntrolLeftRightMove}");

                        Canvas.SetTop(userControl, Canvas.GetTop(userControl) - transformOrigin.X * deltaHorizontal * Math.Sin(angle));
                        Canvas.SetLeft(userControl, Canvas.GetLeft(userControl) + deltaHorizontal * transformOrigin.X * (1 - Math.Cos(angle)));
                        userControl.Width -= deltaHorizontal;
                        designerItem.Width -= deltaHorizontal;
                        break;
                    default:
                        break;
                }
            }

            e.Handled = true;
        }

        private void ResizeThumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            if (adorner != null)
            {
                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(canvas);
                if (adornerLayer != null)
                {
                    adornerLayer.Remove(adorner);
                }

                adorner = null;
            }
        }
    }
}
