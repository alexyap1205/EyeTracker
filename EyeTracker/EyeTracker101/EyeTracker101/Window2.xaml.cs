using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using AccessLibrary;
using Presentation.Helpers;

namespace EyeTracker101
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        private IGazePoint gazePoint;

        public Window2()
        {
            InitializeComponent();

            gazePoint = new GazePoint();
            gazePoint.PositionChanged += GazePoint_PositionChanged;

            this.Loaded += Window2_Loaded;
            this.Unloaded += Window2_Unloaded;
        }

        private void GazePoint_PositionChanged(object sender, EventArgs e)
        {
            GazePointEventArgs args = e as GazePointEventArgs;
            if (args != null)
            {
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                {
                    try
                    {
                        Point canvasPosition = DisplayCanvas.PointFromScreen(args.Position);

                        if (BoundaryHelper.IsWithin(rectSample, args.Position))
                        {
                            txtPosition.Text = "HIT";
                            txtPosition.Background = Brushes.LawnGreen;
                        }
                        else
                        {
                            txtPosition.Text = "NOT";
                            txtPosition.Background = Brushes.Red;
                        }

                        Canvas.SetLeft(txtPosition, canvasPosition.X - 50);
                        Canvas.SetTop(txtPosition, canvasPosition.Y - 10);

                    }
                    catch (Exception)
                    {
                        // Ignore

                    }
                }));
            }
        }

        private void Window2_Unloaded(object sender, RoutedEventArgs e)
        {
            gazePoint.Stop();
        }

        private void Window2_Loaded(object sender, RoutedEventArgs e)
        {
            gazePoint.Start();
        }
    }
}
