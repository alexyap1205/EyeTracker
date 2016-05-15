using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using AccessLibrary;
using Presentation.Helpers;

namespace Presentation.Controls
{
    public class ButtonTriggeredEventArgs : EventArgs
    {
        public ButtonTriggeredEventArgs()
        {
            
        }

        public ButtonTriggeredEventArgs(string content)
        {
            Title = content;
        }

        public string Title { get; set; }
    }

    /// <summary>
    /// Interaction logic for OverlayedButton.xaml
    /// </summary>
    public partial class OverlayedButton : UserControl
    {
        private DispatcherTimer timer;
        private volatile int stage;
        private string title;

        public event EventHandler<ButtonTriggeredEventArgs> ButtonTriggeredEvent;
        private bool eventTriggered;

        public OverlayedButton()
        {
            InitializeComponent();

            isInitialized = false;
        }

        private IGazePoint gazeManager;
        private volatile bool isInitialized;

        public void Initialize(IGazePoint gazePoint, string content, Brush backgroundBrush)
        {
            if (String.IsNullOrEmpty(content))
            {
                return;
            }

            gazeManager = gazePoint;
            gazeManager.PositionChanged += GazeManager_PositionChanged;

            title = content;
            btnDisplay.Content = title;
            btnDisplay.Background = backgroundBrush;

            stage = 0;
            eventTriggered = false;

            isInitialized = true;
        }

        public void StartTimer()
        {
            if (!isInitialized)
            {
                return;
            }

            if (timer != null && timer.IsEnabled)
            {
                // Do nothing
                return;
            }

            StopTimer();

            stage = 1;
            imgFive.Opacity = 0.5;
            //btnDisplay.Background = Brushes.Yellow;

            timer = new DispatcherTimer(new TimeSpan(0, 0, 0, 0, 250), DispatcherPriority.Normal, dispatcherTimer_Tick, Application.Current.Dispatcher);
            timer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (!isInitialized)
            {
                return;
            }
            //btnDisplay.Content = String.Format("Time: {0}:{1}:{2} Stage: {3}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, stage);

            switch (stage)
            {
                case 1:
                    imgFive.Opacity = 0;
                    imgFour.Opacity = 0.5;
                    stage++;
                    break;
                case 2:
                    imgFour.Opacity = 0;
                    imgThree.Opacity = 0.5;
                    stage++;
                    break;
                case 3:
                    imgThree.Opacity = 0;
                    imgTwo.Opacity = 0.5;
                    stage++;
                    break;
                case 4:
                    imgTwo.Opacity = 0;
                    imgOne.Opacity = 0.5;
                    stage++;
                    break;
                case 5:
                    imgOne.Opacity = 0;
                    RaiseButtonTriggeredEvent();
                    StopTimer();
                    break;
                default:
                    StopTimer();
                    break;
            }
        }

        public void StopTimer()
        {
            //btnDisplay.Background = SystemColors.ControlBrush;
            if (timer != null && timer.IsEnabled)
            {
                timer.Stop();
                timer = null;
                Thread.Sleep(1);
                imgFive.Opacity = 0;
                imgFour.Opacity = 0;
                imgThree.Opacity = 0;
                imgTwo.Opacity = 0;
                imgOne.Opacity = 0;
                stage = 0;
            }
        }

        private void RaiseButtonTriggeredEvent()
        {
            if (isInitialized && ButtonTriggeredEvent != null)
            {
                ButtonTriggeredEvent(this, new ButtonTriggeredEventArgs(this.title));
                eventTriggered = true;
            }
        }

        public void UnInitialize()
        {
            isInitialized = false;

            StopTimer();

            if (gazeManager != null)
            {
                gazeManager.PositionChanged -= GazeManager_PositionChanged;
                gazeManager = null;
            }

        }

        private void GazeManager_PositionChanged(object sender, EventArgs e)
        {
            GazePointEventArgs args = e as GazePointEventArgs;
            if (isInitialized && args != null)
            {
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                {
                    try
                    {
                        if (BoundaryHelper.IsWithin(this, args.Position))
                        {
                            //btnDisplay.Content = args.Position.ToString();
                            if (!eventTriggered)
                                StartTimer();
                        }
                        else
                        {
                            StopTimer();
                            //stage = -1;
                            eventTriggered = false;
                        }
                    }
                    catch (Exception)
                    {
                        // Ignore
                    
                    }
                }));
            }
        }
    }
}
