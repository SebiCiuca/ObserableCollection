using ReactiveUI;
using System;
using System.Linq;
using System.Reactive.Disposables;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ObserableCollection
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        Point startPoint;
        Point endPoint;
        Canvas canvas; 
        public MainWindow(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();

            ViewModel = mainWindowViewModel;

            DataContextChanged += (sender, args) => ViewModel = DataContext as MainWindowViewModel;

            DrawingCanvas.MouseLeftButtonDown += DrawingCanvas_MouseLeftButtonDown;
            DrawingCanvas.MouseMove += DrawingCanvas_MouseMove;
            DrawingCanvas.MouseLeftButtonUp += DrawingCanvas_MouseLeftButtonUp;

            this.WhenActivated(cleanup =>
            {
                //this.BindCommand(ViewModel, vm => vm.RandonListCommand, view => view.RandomButton).DisposeWith(cleanup);
            });
        }

        private void DrawingCanvas_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            System.Diagnostics.Debug.WriteLine($"Mouse released");
            ((Canvas)sender).ReleaseMouseCapture();

            MoveLineIntoContentControl();
        }
        void MoveLineIntoContentControl()
        {
            var lineControl = new CustomLineUserControl();
            var lineControlViewModel = new CustomLineViewModel("ThisIsTheName");
            lineControl.ViewModel = lineControlViewModel;

            var vector = endPoint - startPoint;
            var lineWidth = Math.Sqrt(Math.Pow(vector.X, 2) + Math.Pow(vector.Y, 2));
            var angle = Math.Atan2(endPoint.Y - startPoint.Y, endPoint.X - startPoint.X);
            var angleInDegrees = (180 / Math.PI) * angle;
            var middleYPoint = startPoint.Y + (endPoint.Y - startPoint.Y) / 2;

            lineControl.ViewModel.LinePoint1 = new(5, 5);
            lineControl.ViewModel.ActualHeight = 10;
            lineControl.ViewModel.ActualWidth = lineWidth + 16;
            lineControl.ViewModel.RotationTransformInitialAngle = angleInDegrees;
            var lineControlStartingPoint = new Point(startPoint.X, middleYPoint);

            Canvas.SetLeft(lineControl, lineControlStartingPoint.X - 10);
            Canvas.SetTop(lineControl, lineControlStartingPoint.Y - 5);

            canvas.Children.Add(lineControl);
        }

        private void DrawingCanvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            canvas = (Canvas)sender;
            System.Diagnostics.Debug.WriteLine($"Is mouse captured: {canvas.IsMouseCaptured}");
            if (canvas.IsMouseCaptured && e.LeftButton == MouseButtonState.Pressed)
            {
                System.Diagnostics.Debug.WriteLine($"Mouse is capture updating line");
                var line = canvas.Children.OfType<Line>().LastOrDefault();

                if (line != null)
                {
                    endPoint = e.GetPosition(canvas);
                    line.X2 = endPoint.X;
                    line.Y2 = endPoint.Y;
                }
            }
        }

        private void DrawingCanvas_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"Left button state: {e.LeftButton}");
            

            if (canvas.CaptureMouse())
            {
                System.Diagnostics.Debug.WriteLine($"Capture mouse canvas");
                startPoint = e.GetPosition(canvas);
                var line = new Line
                {
                    Stroke = Brushes.Blue,
                    StrokeThickness = 1,
                    X1 = startPoint.X,
                    Y1 = startPoint.Y,
                    X2 = startPoint.X,
                    Y2 = startPoint.Y,
                    SnapsToDevicePixels = true
                };
                line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Unspecified);

                canvas.Children.Add(line);
            }
        }
    }
}
