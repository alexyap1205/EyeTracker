using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Presentation.Controls;

namespace Presentation.Helpers
{
    public class BoundaryHelper
    {
        public static bool IsWithin(Button element, Point position)
        {
            bool isWithin = false;

            Point topLeftPoint = element.PointToScreen(new Point(0, 0));
            double width = element.ActualWidth;
            double height = element.ActualHeight;

            if ((position.X > topLeftPoint.X && position.X < (topLeftPoint.X + width))
                && (position.Y > (topLeftPoint.Y - 100) && position.Y < (topLeftPoint.Y + height + 200)))
            {
                isWithin = true;
            }

            return isWithin;
        }
        public static bool IsWithin(OverlayedButton element, Point position)
        {
            bool isWithin = false;

            Point topLeftPoint = element.PointToScreen(new Point(0, 0));
            double width = element.ActualWidth;
            double height = element.ActualHeight;

            if ((position.X > topLeftPoint.X && position.X < (topLeftPoint.X + width))
                && (position.Y > topLeftPoint.Y && position.Y < (topLeftPoint.Y + height)))
            {
                isWithin = true;
            }

            return isWithin;
        }
    }
}
