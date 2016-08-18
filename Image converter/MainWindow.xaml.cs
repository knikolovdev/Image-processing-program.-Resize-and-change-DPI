using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Image_converter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow UI;
        public static ManualResetEvent mre = new ManualResetEvent(true);
        Thread botThread;
        public MainWindow()
        {
            InitializeComponent();
            UI = this;
        }

        private void StatButton_Click(object sender, RoutedEventArgs e)
        {
            if (SourceF.Text == string.Empty)
            {
                System.Windows.MessageBox.Show("Please select folder.");
            }
            else if ((WidthBox.Text == string.Empty || HeightBox.Text == string.Empty))
            {
                System.Windows.MessageBox.Show("Please enter maximum width and height.");
            }
            else
            {
                botThread = new Thread(Converter.Convert);
                botThread.Start();
            }
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Show();
        }

        private void HelpBtn_Click(object sender, RoutedEventArgs e)
        {
            HelpWindow helpWindow = new HelpWindow();
            helpWindow.Show();
        }

        private void AboutBtn_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.Show();
        }

        private void FolderS_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            DialogResult result = fbd.ShowDialog();

            if (!string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                SourceF.Text = fbd.SelectedPath;
            }

        }

        private void FolderD_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            DialogResult result = fbd.ShowDialog();

            if (!string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                DestF.Text = fbd.SelectedPath;
            }
        }

        private void SameF_Checked(object sender, RoutedEventArgs e)
        {
            DestF.Text = SourceF.Text;
        }

        private void SameF_Unchecked(object sender, RoutedEventArgs e)
        {
            DestF.Clear();
        }
    }
}
