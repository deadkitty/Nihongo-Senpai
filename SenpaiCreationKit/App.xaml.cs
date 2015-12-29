using SenpaiCreationKit.Data;
using SenpaiCreationKit.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
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
            
            if(Settings.Default.FirstStart)
            {
                DataManager.CreateDatabase();
                Settings.Default.FirstStart = false;
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Settings.Default.Save();
        }
    }
}
