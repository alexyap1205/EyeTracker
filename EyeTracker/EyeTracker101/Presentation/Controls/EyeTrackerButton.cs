using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using AccessLibrary;
using Presentation.Helpers;

namespace Presentation.Controls
{
    public class EyeTrackerButton : Button
    {
        private IGazePoint gazeManager;

        public void Initialize(IGazePoint gazePoint)
        {
            gazeManager = gazePoint;
            gazeManager.PositionChanged += GazeManager_PositionChanged;
        }

        public void UnInitialize()
        {
            if (gazeManager != null)
            {
                gazeManager.PositionChanged -= GazeManager_PositionChanged;
                gazeManager = null;
            }
        }

        private void GazeManager_PositionChanged(object sender, EventArgs e)
        {
            GazePointEventArgs args = e as GazePointEventArgs;
            if (args != null)
            {
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action( () =>
                {
                    if (BoundaryHelper.IsWithin(this, args.Position))
                    {
                        this.Background = Brushes.Blue;
                    }
                    else
                    {
                        this.Background = SystemColors.ControlBrush;
                    }
                }));
            }
        }
    }
}
