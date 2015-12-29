using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Newtonsoft.Json;
using NihongoSenpai.Data.Database;

namespace NihongoSenpai.Pages
{
    public partial class MarketplacePage : PhoneApplicationPage
    {
        public MarketplacePage()
        {
            InitializeComponent();

            string uri="http://api.windows8central.in/api/Lessons/";
            WebClient client = new WebClient();
            client.Headers["Accept"] = "application/json";
            client.DownloadStringAsync(new Uri(uri));
            client.DownloadStringCompleted += (s1, e1) =>
            {
                var data = JsonConvert.DeserializeObject<Lesson[]>(e1.Result.ToString());
                
            };
        }
    }
}