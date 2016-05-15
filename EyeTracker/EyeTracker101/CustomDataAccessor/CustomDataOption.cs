using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomDataAccessor
{
    public class CustomData
    {
        public string DisplayName { get; set; }

        public List<CustomDataOption> CustomDataOptions { get; set; }
    }

    public class CustomDataOption
    {
        public CustomDataOption()
        {
            
        }

        public CustomDataOption(string display)
        {
            OptionDisplay = display;
            //ImageFile = image;
        }

        public string OptionDisplay { get; set; }

        //public string ImageFile { get; set; }

    }
}
