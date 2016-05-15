using System;
using System.Threading;
using System.Windows;
using TETCSharpClient;
using TETCSharpClient.Data;

namespace AccessLibrary
{
    public class GazePoint : IGazeListener, IGazePoint
    {
        private bool isStarted;

        public GazePoint()
        {
            isStarted = false;
        }

        public void OnGazeUpdate(GazeData gazeData)
        {
            if (isStarted)
            {
                double gX = gazeData.SmoothedCoordinates.X;
                double gY = gazeData.SmoothedCoordinates.Y;

                // Raise Gaze Point
                RaisePositionChangedEvent(gX, gY);
            }
        }

        public void Start()
        {
            if (!isStarted)
            {
                // Connect client
                GazeManager.Instance.Activate(GazeManager.ApiVersion.VERSION_1_0, GazeManager.ClientMode.Push);

                // Register this class for events
                GazeManager.Instance.AddGazeListener(this);

                isStarted = true;
            }
        }

        public void Stop()
        {
            if (isStarted)
            {
                // Remove listener
                GazeManager.Instance.RemoveGazeListener(this);

                // Disconnect client
                GazeManager.Instance.Deactivate();

                isStarted = false;
            }
        }

        public event EventHandler PositionChanged;

        private void RaisePositionChangedEvent(double x, double y)
        {
            if (PositionChanged != null)
            {
                Point point = new Point(x, y);
                PositionChanged(this, new GazePointEventArgs(point));
            }
        }
    }
}
