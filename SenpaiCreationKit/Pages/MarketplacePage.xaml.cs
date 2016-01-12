using SenpaiCreationKit.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using Newtonsoft.Json.Linq;
using SenpaiCreationKit.Resources;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;

namespace SenpaiCreationKit.Pages
{
    /// <summary>
    /// Interaktionslogik für MarketplacePage.xaml
    /// </summary>
    public partial class MarketplacePage : Page
    {
        #region Fields
        
        private List<Lesson> localLessons = new List<Lesson>();
        private List<Lesson> uploadedLessons = new List<Lesson>();
        
        private const String marketplaceUrl = "http://localhost:53630/";

        private SenpaiDatabase database;

        #endregion

        #region Constructor

        public MarketplacePage()
        {
            InitializeComponent();
        }
               
        #endregion

        #region Initialize/Deinitialize

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            database = new SenpaiDatabase();
            
            //add lessons from local database
            localLessons.AddRange(database.Lessons);
            
            foreach (Lesson lesson in localLessons)
            {
                localLessonsListbox.Items.Add(lesson);
            }

            //try add lessons from senpaiMarketplace
            try
            {
                List<Lesson> uploadedLessons = GetAllLessons();

                foreach (Lesson lesson in uploadedLessons)
                {
                    uploadedLessonsListbox.Items.Add(lesson);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            database.Dispose();
        }

        #endregion

        #region Button Click

        private void UploadLessons_Click(object sender, RoutedEventArgs e)
        {
            if(localLessonsListbox.SelectedItems.Count == 0)
            {
                MessageBox.Show("Lektion auswählen!!!");
            }
            else
            {
                try
                {
                    List<Lesson> lessonsToUpload = new List<Lesson>();

                    foreach (Lesson lesson in localLessonsListbox.SelectedItems)
                    {
                        lessonsToUpload.Add(lesson);
                    }
                    
                    PostLessons(lessonsToUpload);
                    
                    List<Lesson> uploadedLessons = GetAllLessons();

                    foreach (Lesson lesson in uploadedLessons)
                    {
                        uploadedLessonsListbox.Items.Add(lesson);
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }
        }

        private void UpdateLessons_Click(object sender, RoutedEventArgs e)
        {
            if(localLessonsListbox.SelectedItems.Count != uploadedLessonsListbox.SelectedItems.Count ||
               localLessonsListbox.SelectedItems.Count +  uploadedLessonsListbox.SelectedItems.Count == 0 )
            {
                MessageBox.Show("Lektion auswählen!!!");
            }
            else
            {

            }
        }

        private void DeleteLessons_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Lesson lesson = uploadedLessonsListbox.SelectedItem as Lesson;

            HttpResponseMessage response = client.GetAsync(marketplaceUrl + "api/Lessons/type/" + (int)Lesson.EType.vocab + "/search/Minna").Result;
            
            client.Dispose();

            List<Lesson> lessons = response.Content.ReadAsAsync<List<Lesson>>().Result;

            if(uploadedLessonsListbox.SelectedItems.Count == 0)
            {
                MessageBox.Show("Lektion auswählen!!!");
            }
            else
            {

            }
        }

        private void DeleteDatabase_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(marketplaceUrl);

            HttpResponseMessage response = client.DeleteAsync("api/database/0").Result;
            
            client.Dispose();

            if(response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                MessageBox.Show("Delete hat funktioniert =)");
            }
            else
            {
                MessageBox.Show("Delete hat nich funktioniert =(");
            }
        }

        private void exitMarketplace_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        
        #endregion

        #region HTTP Requests

        private List<Lesson> GetAllLessons()
        {
            HttpClient client = new HttpClient();
            
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(marketplaceUrl + "api/Lessons").Result;
            
            client.Dispose();

            JsonMediaTypeFormatter formatter = new JsonMediaTypeFormatter();
            formatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All;

            return response.Content.ReadAsAsync<List<Lesson>>(new []{ formatter }).Result;
        }

        private void PostLessons(List<Lesson> lessons)
        {
            HttpClient client = new HttpClient();
            
            client.BaseAddress = new Uri(marketplaceUrl);

            JsonMediaTypeFormatter formatter = new JsonMediaTypeFormatter();
            formatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All;

            HttpResponseMessage response = client.PostAsync<List<Lesson>>("api/Lessons", lessons, formatter).Result;
                        
            client.Dispose();
        }

        #endregion

        private void uploadedLessonsListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count > 0)
            {
                Lesson lesson = e.AddedItems[0] as Lesson;

                

            }
        }
    }
}
