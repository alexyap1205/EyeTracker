using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace AccessLibrary
{
    public class GazePointEventArgs : EventArgs
    {
        public GazePointEventArgs()
        {
            Position = new Point(0, 0);
        }

        public GazePointEventArgs(Point point)
        {
            Position = point;
        }

        public Point Position { get; set; }

    }
}
