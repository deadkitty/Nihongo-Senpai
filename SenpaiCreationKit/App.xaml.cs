using SenpaiCreationKit.Data;
using SenpaiCreationKit.Properties;
using System.Windows;

namespace SenpaiCreationKit
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Settings.Default.Reset();

            //DataManager.DeleteDatabase();
            DataManager.CreateDatabase();

            if (Settings.Default.FirstStart)
            {
                Settings.Default.FirstStart = false;
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Settings.Default.Save();
        }
    }
}
