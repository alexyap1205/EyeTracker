using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTracker101
{
    public interface IButtonTriggeredListener
    {
        void HandleButtonTriggered(string message);
    }
}
