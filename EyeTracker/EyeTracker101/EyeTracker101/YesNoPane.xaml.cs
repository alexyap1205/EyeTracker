using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AccessLibrary;
using Presentation.Controls;

namespace EyeTracker101
{
    /// <summary>
    /// Interaction logic for YesNoPane.xaml
    /// </summary>
    public partial class YesNoPane : UserControl, IInteractPane
    {
        public YesNoPane()
        {
            InitializeComponent();
        }

        private IButtonTriggeredListener buttonTriggeredListener;

        public void Initialize(IButtonTriggeredListener listener, IGazePoint gazePoint)
        {
            btnYes.Initialize(gazePoint, "Yes", Brushes.LimeGreen);
            btnYes.ButtonTriggeredEvent += ButtonTriggeredEventHandler;
            btnNo.Initialize(gazePoint, "No", Brushes.OrangeRed);
            btnNo.ButtonTriggeredEvent += ButtonTriggeredEventHandler;

            buttonTriggeredListener = listener;
        }

        public void UnInitialize()
        {
            btnYes.UnInitialize();
            btnNo.UnInitialize();

            buttonTriggeredListener = null;
        }

        private void ButtonTriggeredEventHandler(object sender, ButtonTriggeredEventArgs args)
        {
            buttonTriggeredListener.HandleButtonTriggered(args.Title);
        }

    }
}
