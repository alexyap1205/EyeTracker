using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AccessLibrary;

namespace EyeTracker101
{
    internal interface IInteractPane
    {
        void Initialize(IButtonTriggeredListener listener, IGazePoint gazePoint);

        void UnInitialize();
    }
}
