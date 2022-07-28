using ReactiveUI;
using System.Reactive.Disposables;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ObserableCollection
{
    /// <summary>
    /// Interaction logic for CustomLineUserControl.xaml
    /// </summary>
    public partial class CustomLineUserControl : IViewFor<CustomLineViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty
            .Register(nameof(ViewModel), typeof(CustomLineViewModel), typeof(CustomLineUserControl), new PropertyMetadata(null));

        public CustomLineUserControl()
        {
            InitializeComponent();

            this.DataContextChanged += (sender, args) => ViewModel = DataContext as CustomLineViewModel;

            this.Loaded += CustomLineUserControl_Loaded;
            this.CustomLineContentControl.Loaded += CustomLineContentControl_Loaded;
            //this.LeftPointThumb.MouseEnter += LeftPointThumb_MouseEnter;
            //this.LeftPointThumb.DragStarted += LeftPointThumb_DragStarted;
            //this.LeftPointThumb.DragDelta += LeftPointThumb_DragDelta;


            this.WhenActivated(cleanup =>
            {
                this.OneWayBind(ViewModel, vm => vm.Name, v => v.Name)
                    .DisposeWith(cleanup);

                this.OneWayBind(ViewModel, vm => vm.LinePoint1.X, v => v.CustomLine.X1)
                    .DisposeWith(cleanup);
                this.OneWayBind(ViewModel, vm => vm.LinePoint1.Y, v => v.CustomLine.Y1)
                    .DisposeWith(cleanup);
                this.OneWayBind(ViewModel, vm => vm.LinePoint2.X, v => v.CustomLine.X2)
                    .DisposeWith(cleanup);
                this.OneWayBind(ViewModel, vm => vm.LinePoint2.Y, v => v.CustomLine.Y2)
                    .DisposeWith(cleanup);
                this.OneWayBind(ViewModel, vm => vm.StrokeColor, v => v.CustomLine.Stroke, Convertors.HexColorToBrush)
                    .DisposeWith(cleanup);
                this.OneWayBind(ViewModel, vm => vm.StrokeThickness, v => v.CustomLine.StrokeThickness)
                    .DisposeWith(cleanup);
                this.OneWayBind(ViewModel, vm => vm.ActualWidth, v => v.CustomLineContentControl.ActualWidth)
                    .DisposeWith(cleanup);
                this.OneWayBind(ViewModel, vm => vm.ActualHeight, v => v.CustomLineContentControl.ActualHeight)
                    .DisposeWith(cleanup);
                this.OneWayBind(ViewModel, vm => vm.ActualWidth, v => v.CustomLine.ActualWidth)
                    .DisposeWith(cleanup);
                this.OneWayBind(ViewModel, vm => vm.ActualHeight, v => v.CustomLine.ActualHeight)
                    .DisposeWith(cleanup);
                this.OneWayBind(ViewModel, vm => vm.ActualWidth, v => v.ActualWidth)
                    .DisposeWith(cleanup);
                this.OneWayBind(ViewModel, vm => vm.ActualHeight, v => v.ActualHeight)
                    .DisposeWith(cleanup);
                this.OneWayBind(ViewModel, vm => vm.RotationTransformInitialAngle, v => v.RotateTransformControl.Angle)
                    .DisposeWith(cleanup);

                //this.OneWayBind(ViewModel, vm => vm.TransformGroup , v => v.CustomLineContentControl.RenderTransform)
                //    .DisposeWith(cleanup);
            });
        }

        private void LeftPointThumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Mouse entered Left point thumb");

            //ViewModel.LinePoint1.X += e.HorizontalChange;
        }

        private bool dragStarted;
        private void LeftPointThumb_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Drag started");
        }

        private void LeftPointThumb_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Mouse entered Left point thumb");
        }

        private void CustomLineContentControl_Loaded(object sender, RoutedEventArgs e)
        {
            var cc = (sender as ContentControl);
            System.Diagnostics.Debug.WriteLine($"Actual width {cc.ActualWidth}. Actual height {cc.ActualHeight}");
            System.Diagnostics.Debug.WriteLine($"Width {cc.Width}. Height {cc.Height}");
            cc.Width = this.ViewModel.ActualWidth;
            cc.Height = this.ViewModel.ActualHeight;
            System.Diagnostics.Debug.WriteLine($"Width {cc.Width}. Height {cc.Height}");
        }

        private void CustomLineUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.Width = this.ViewModel.ActualWidth;
            this.Height = this.ViewModel.ActualHeight;
        }

        public CustomLineViewModel ViewModel { get => (CustomLineViewModel)GetValue(ViewModelProperty); set => SetValue(ViewModelProperty, value); }
        object IViewFor.ViewModel { get => ViewModel; set => ViewModel = (CustomLineViewModel)value; }
    }
}
