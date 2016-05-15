using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomDataAccessor
{
    public interface ICustomDataOptionAccessor
    {
        CustomData GetCustomData(string filePath);

        void SetCustomData(string filePath, CustomData data);
    }
}
