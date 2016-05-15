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
using CustomDataAccessor;
using Presentation.Controls;

namespace EyeTracker101
{
    /// <summary>
    /// Interaction logic for CustomPane.xaml
    /// </summary>
    public partial class CustomPane : UserControl, IInteractPane
    {
        private IButtonTriggeredListener buttonTriggeredListener;

        public CustomPane()
        {
            InitializeComponent();
        }

        public void Initialize(IButtonTriggeredListener listener, IGazePoint gazePoint)
        {
            buttonTriggeredListener = listener;

            button0.ButtonTriggeredEvent += ButtonTriggeredEventHandler;
            button1.ButtonTriggeredEvent += ButtonTriggeredEventHandler;
            button2.ButtonTriggeredEvent += ButtonTriggeredEventHandler;
        }

        public void Initialize(IButtonTriggeredListener listener, IGazePoint gazePoint, CustomData data)
        {
            Initialize(listener, gazePoint);
            button0.Initialize(gazePoint, data.CustomDataOptions[0].OptionDisplay, Brushes.DeepSkyBlue);
            button1.Initialize(gazePoint, data.CustomDataOptions[1].OptionDisplay, Brushes.Yellow);
            button2.Initialize(gazePoint, data.CustomDataOptions[2].OptionDisplay, Brushes.MediumPurple);
        }

        public void UnInitialize()
        {
            button0.UnInitialize();
            button1.UnInitialize();
            button2.UnInitialize();

            buttonTriggeredListener = null;
        }

        private void ButtonTriggeredEventHandler(object sender, ButtonTriggeredEventArgs args)
        {
            buttonTriggeredListener.HandleButtonTriggered(args.Title);
        }
    }
}
