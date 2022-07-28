using ReactiveUI;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace ObserableCollection
{
    public class CustomLineViewModel : ReactiveObject
    {
        private Point _linePoint1;

        private Point _linePoint2;

        private double _rotateTransformInitialAngle;
        private TransformGroup _transformGroup;

        string _name;
        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        string _strokeColor;
        public string StrokeColor
        {
            get => _strokeColor;
            set => this.RaiseAndSetIfChanged(ref _strokeColor, value);
        }

        int _strokeThickness = 1;
        public int StrokeThickness
        {
            get => _strokeThickness;
            set => this.RaiseAndSetIfChanged(ref _strokeThickness, value);
        }

        double _actualWidth;
        public double ActualWidth
        {
            get => _actualWidth;
            set
            {
                this.RaiseAndSetIfChanged(ref _actualWidth, value);
                System.Diagnostics.Debug.WriteLine($"Actual Width {_actualWidth}");
            }
        }

        double _actualHeight;
        public double ActualHeight
        {
            get => _actualHeight;
            set
            {
                this.RaiseAndSetIfChanged(ref _actualHeight, value);
                System.Diagnostics.Debug.WriteLine($"Actual Height {_actualHeight}");
            }
        }

        public CustomLineViewModel(string name)
        {
            Name = name;
        }

        public Point LinePoint1
        {
            get => _linePoint1;
            set => this.RaiseAndSetIfChanged(ref _linePoint1, value);
        }
        public Point LinePoint2
        {
            get => _linePoint2;
            set => this.RaiseAndSetIfChanged(ref _linePoint2, value);
        }
        public double RotationTransformInitialAngle
        {
            get => _rotateTransformInitialAngle;
            set
            {
                if (_rotateTransformInitialAngle == value)
                {
                    return;
                }

                this.RaiseAndSetIfChanged(ref _rotateTransformInitialAngle, value);
                OnRotationAngleChanged();
            }
        }
        public TransformGroup TransformGroup
        {
            get => _transformGroup;
            set
            {
                if (_transformGroup == value)
                {
                    return;
                }

                this.RaiseAndSetIfChanged(ref _transformGroup, value);
            }
        }

        private void OnRotationAngleChanged()
        {
            if (TransformGroup == null)
            {
                TransformGroup group = new();
                RotateTransform rotate = new(_rotateTransformInitialAngle);

                group.Children.Add(rotate);

                TransformGroup = group;
            }

            var rotateTransform = TransformGroup.Children.OfType<RotateTransform>().FirstOrDefault();

            rotateTransform.Angle = _rotateTransformInitialAngle;
        }
    }
}
