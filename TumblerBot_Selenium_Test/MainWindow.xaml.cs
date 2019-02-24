using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NLog;

namespace TumblerBot_Selenium_Test
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Logger Logger;


        public MainWindow()
        {
            InitializeComponent();
            Logger = LogManager.GetLogger("log");
            LoadSettings();
        }

        public void LoadSettings()
        {
            Settings settings = Settings.LoadSettings();
            MaxCountOfLikePerDayTextBox.Text =settings.MaxCountOfLikePerDayText;
            MaxCountOfLikePerUserTextBox.Text=settings.MaxCountOfLikePerUserText;
            MaxCountOfFollowPerDayTextBox.Text=settings.MaxCountOfFollowPerDayText;
            SearchWordsTextBox.Text=settings.SearchWordsText;
        }

        public Settings GetSettings()
        {
            Settings settings =new Settings();
            try
            {
                settings.MaxCountOfLikePerDayText=MaxCountOfLikePerDayTextBox.Text;
                settings.MaxCountOfLikePerUserText = MaxCountOfLikePerUserTextBox.Text;
                settings.MaxCountOfFollowPerDayText= MaxCountOfFollowPerDayTextBox.Text;
                settings.SearchWordsText = SearchWordsTextBox.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return settings;
        }

        private void SaveSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            GetSettings().SaveSettings();
        }

        private void LoadDefaultSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            Settings.GetDefaultSettings().SaveSettings();
            LoadSettings();
        }
    }
}
