using System;
using System.Net;
using Microsoft.Phone.Controls;
using NihongoSenpai.Data.Database;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace NihongoSenpai.Pages
{
    public partial class MarketplacePage : PhoneApplicationPage
    {
        List<Lesson> lessons;

        public const String marketplaceUrl = "http://senpai-marketplace-test.azurewebsites.net/";

        public MarketplacePage()
        {
            InitializeComponent();

            lessons = new List<Lesson>();

            try
            {
                Uri marketplaceUri = new Uri(marketplaceUrl + "api/Lessons");

                WebClient downloader = new WebClient();

                downloader.OpenReadCompleted += new OpenReadCompletedEventHandler(downloader_OpenReadCompleted);
                downloader.OpenReadAsync(marketplaceUri);
            }
            catch (Exception)
            {
                MessageBox.Show("Some Error Occured");
            }
        }

        void downloader_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                Stream responseStream = e.Result;
                
                using (StreamReader rw = new StreamReader(responseStream))
                {
                    lessons = JsonConvert.DeserializeObject<List<Lesson>>(rw.ReadToEnd());
                }
            }
        }
    }
}