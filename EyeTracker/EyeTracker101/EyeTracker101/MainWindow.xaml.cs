using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
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
using System.Windows.Threading;
using AccessLibrary;
using CustomDataAccessor;
using Presentation.Controls;

namespace EyeTracker101
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IButtonTriggeredListener
    {
        private IGazePoint gazePoint;
        private IInteractPane currentInteractPane;

        public MainWindow()
        {
            InitializeComponent();

            gazePoint = new GazePoint();

            this.Loaded += MainWindow_Loaded;
            this.Unloaded += MainWindow_Unloaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            gazePoint.Start();

            YesNoPane yesNoPane = new YesNoPane();
            ReplaceInteractPane(yesNoPane, yesNoPane);

            LoadCustomData();

            //testButton.ButtonTriggeredEvent += ButtonTriggeredEventHandler;
            //testButton.Initialize(gazePoint, "Test");
        }

        private void LoadCustomData()
        {
            List<string> customFiles = GetCustomFiles();

            foreach (string customFile in customFiles)
            {
                CustomDataOptionAccessor accessor = new CustomDataOptionAccessor();

                try
                {
                    CustomData data1 = accessor.GetCustomData(customFile);
                    RadioButton button1 = new RadioButton()
                    {
                        Content = data1.DisplayName,
                        Tag = data1
                    };
                    button1.Checked += CustomButton_Checked;
                    optionPanel.Children.Add(button1);
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Failed to load " + customFile);
                    txtMessage.Text = txtMessage.Text + DateTime.Now.ToShortTimeString() + exc;
                }
            }
        }

        private List<string> GetCustomFiles()
        {
            List<string> result = new List<string>();

            string customPath = @".\Custom\";
            if (Directory.Exists(customPath))
            {
                var customList = Directory.GetFiles(customPath, @"Custom*.json");
                result.AddRange(customList);
            }

            return result;
        }

        private void CustomButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            CustomData data = radioButton.Tag as CustomData;

            CustomPane customPane = new CustomPane();
            ReplaceWithCustomPane(customPane, customPane, data);
        }

        private void MainWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            gazePoint.Stop();

            if (currentInteractPane != null)
            {
                currentInteractPane.UnInitialize();
                //mainPane.Children.Clear();
            }

            Application.Current.Shutdown();
            //testButton.UnInitialize();
        }

        //private void ButtonTriggeredEventHandler(object sender, ButtonTriggeredEventArgs args)
        //{
        //    txtMessage.Text = args.Title;

        //    SpeechSynthesizer synthesizer = new SpeechSynthesizer();
        //    synthesizer.SetOutputToDefaultAudioDevice();
        //    synthesizer.SpeakAsync(args.Title);
        //}

        //private void btnStart_Click(object sender, RoutedEventArgs e)
        //{
        //    testButton.StartTimer();
        //}

        //private void btnStop_Click(object sender, RoutedEventArgs e)
        //{
        //    testButton.StopTimer();
        //}
        public void HandleButtonTriggered(string message)
        {
            txtMessage.Text = txtMessage.Text + String.Format("{0} - {1}\n", DateTime.Now.ToShortTimeString(), message);

            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            synthesizer.SetOutputToDefaultAudioDevice();
            synthesizer.SpeakAsync(message);
        }

        private void btnYesNo_Checked(object sender, RoutedEventArgs e)
        {
            if (gazePoint != null)
            {
                YesNoPane yesNoPane = new YesNoPane();
                ReplaceInteractPane(yesNoPane, yesNoPane);
            }
        }
        private void ReplaceWithCustomPane(CustomPane newPane, UserControl newControl, CustomData data)
        {
            if (currentInteractPane != null)
            {
                currentInteractPane.UnInitialize();
                mainPane.Children.Clear();
            }

            newPane.Initialize(this, gazePoint, data);
            mainPane.Children.Add(newControl);
            currentInteractPane = newPane;
        }

        private void ReplaceInteractPane(IInteractPane newPane, UserControl newControl)
        {
            if (currentInteractPane != null)
            {
                currentInteractPane.UnInitialize();
                mainPane.Children.Clear();
            }

            newPane.Initialize(this, gazePoint);
            mainPane.Children.Add(newControl);
            currentInteractPane = newPane;
        }
    }
}
