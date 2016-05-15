using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AccessLibrary
{
    public interface IGazePoint
    {
        void Start();

        void Stop();

        event EventHandler PositionChanged;
    }
}
