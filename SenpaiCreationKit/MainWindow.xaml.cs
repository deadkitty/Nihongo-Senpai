using System;
using System.Windows;

namespace SenpaiCreationKit
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            rootFrame.Navigate(new Uri("Pages/MainPage.xaml", UriKind.Relative));       
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MinWidth = Width;
            MinHeight = Height;

            //DataManager.ConnectToDatabase();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            //DataManager.CloseConnection();
        }
    }
}
