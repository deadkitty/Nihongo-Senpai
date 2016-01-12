using System;
using System.Net;
using Microsoft.Phone.Controls;
using NihongoSenpai.Data.Database;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace NihongoSenpai.Pages
{
    public partial class MarketplacePage : PhoneApplicationPage
    {
        public MarketplacePage()
        {
            InitializeComponent();

            List<Lesson> lessons = new List<Lesson>();

            string uri="http://api.windows8central.in/api/Lessons/";
            WebClient client = new WebClient();
            client.Headers["Accept"] = "application/json";
            client.DownloadStringAsync(new Uri(uri));
            client.DownloadStringCompleted += (s1, e1) =>
            {
                lessons = JsonConvert.DeserializeObject<List<Lesson>>(e1.Result.ToString());
            };
        }
    }
}