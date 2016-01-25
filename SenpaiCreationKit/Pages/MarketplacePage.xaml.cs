using SenpaiCreationKit.Controls;
using SenpaiCreationKit.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
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

namespace SenpaiCreationKit.Pages
{
    public partial class MarketplacePage : Page, IPageUpdater
    {
        #region EOperation

        enum EOperation
        {
            postLessons,
            putLessons,
            deleteLessons,
            count,
            undefined = -1
        }

        #endregion

        #region Fields

        private List<Lesson> localLessons;
        private List<Lesson> uploadedLessons;

        private EOperation currentOperation = EOperation.undefined;

        private const String marketplaceUrl = "http://localhost:53630/";

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
            selectLessonsControl.pageUpdater = this;

            localLessons = DataManager.GetLessons();

            uploadedLessons = GetLessons();
            UpdateLessonsListboxes(uploadedLessons, localLessons);
        }

        #endregion

        #region Button Click

        private void uploadLessons_Click(object sender, RoutedEventArgs e)
        {
            selectLessonsControl.FillPostLessons(localLessons, uploadedLessons);
            selectLessonsControl.Visibility = Visibility.Visible;

            currentOperation = EOperation.postLessons;
        }

        private void updateLessons_Click(object sender, RoutedEventArgs e)
        {
            selectLessonsControl.FillPutLessons(localLessons, uploadedLessons);
            selectLessonsControl.Visibility = Visibility.Visible;

            currentOperation = EOperation.putLessons;
        }

        private void deleteLessons_Click(object sender, RoutedEventArgs e)
        {
            selectLessonsControl.FillDeleteLessons(uploadedLessons);
            selectLessonsControl.Visibility = Visibility.Visible;

            currentOperation = EOperation.deleteLessons;
        }

        private void exitMarketplace_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        #endregion

        #region HTTP Requests

        private List<Lesson> GetLessons()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync(marketplaceUrl + "api/Lessons").Result;
                
                JsonMediaTypeFormatter formatter = new JsonMediaTypeFormatter();
                formatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All;

                return response.Content.ReadAsAsync<List<Lesson>>(new[] { formatter }).Result;
            }
        }

        private Lesson GetLesson(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync(marketplaceUrl + "api/Lessons/" + id).Result;
                
                JsonMediaTypeFormatter formatter = new JsonMediaTypeFormatter();
                formatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All;

                return response.Content.ReadAsAsync<Lesson>(new[] { formatter }).Result;
            }
        }

        private HttpResponseMessage PostLessons(List<Lesson> lessons)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(marketplaceUrl);

                JsonMediaTypeFormatter formatter = new JsonMediaTypeFormatter();
                formatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All;

                return client.PostAsync<List<Lesson>>("api/Lessons", lessons, formatter).Result;
            }
        }

        private HttpResponseMessage PutLessons(List<Lesson> lessons)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(marketplaceUrl);

                JsonMediaTypeFormatter formatter = new JsonMediaTypeFormatter();
                formatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All;

                return client.PutAsync<List<Lesson>>("api/Lessons", lessons, formatter).Result;
            }
        }

        private HttpResponseMessage DeleteLessons(List<Lesson> lessons)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(marketplaceUrl);

                HttpResponseMessage response = null;

                foreach (Lesson lesson in lessons)
                {
                    response = client.DeleteAsync("api/Lessons/" + lesson.id).Result;

                    if(response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return response;
                    }
                }

                return response;
            }
        }

        #endregion

        #region Helper

        private void UpdateLessonsListboxes(List<Lesson> uploadedLessons, List<Lesson> localLessons)
        {
            uploadedLessons = uploadedLessons.OrderBy(x => x.Type).ToList();
            localLessons    = localLessons.OrderBy(x => x.Type).ToList();

            uploadedLessonsListbox.Items.Clear();

            foreach (Lesson lesson in uploadedLessons)
            {
                uploadedLessonsListbox.Items.Add(lesson);
            }

            localLessonsListbox.Items.Clear();

            foreach (Lesson lesson in localLessons)
            {
                localLessonsListbox.Items.Add(lesson);
            }
        }

        public void UpdatePage()
        {
            switch(currentOperation)
            {
                case EOperation.postLessons  : PostLessons  (); break;
                case EOperation.putLessons   : PutLessons   (); break;
                case EOperation.deleteLessons: DeleteLessons(); break;
            }

            currentOperation = EOperation.undefined;
        }

        private void PostLessons()
        {
            try
            {
                HttpResponseMessage response = PostLessons(selectLessonsControl.selectedLessons);
                
                uploadedLessons = GetLessons();
                UpdateLessonsListboxes(uploadedLessons, localLessons);

                switch (response.StatusCode)
                {
                    case HttpStatusCode.Conflict  : MessageBox.Show("Mindestens eine der Lektionen existiert bereits =/", "Fehler"); break;
                    case HttpStatusCode.BadRequest: MessageBox.Show("Mindestens eine der Lektionen ist fehlerhaft =(", "Fehler"); break;
                    case HttpStatusCode.Created   : MessageBox.Show("Yay, hat geklappt (^_^)/", "Erstellen erfolgreich"); break;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ihrgent ein Fehler ist aufgetretten =/", "Fehler");
            }
        }
        
        private void PutLessons()
        {
            try
            {
                HttpResponseMessage response = PutLessons(selectLessonsControl.selectedLessons);

                uploadedLessons = GetLessons();
                UpdateLessonsListboxes(uploadedLessons, localLessons);

                switch (response.StatusCode)
                {
                    case HttpStatusCode.NotFound  : MessageBox.Show("Mindestens eine der Lektionen wurde nicht gefunden =/", "Fehler"); break;
                    case HttpStatusCode.BadRequest: MessageBox.Show("Mindestens eine der Lektionen ist fehlerhaft =(", "Fehler"); break;
                    case HttpStatusCode.NoContent : MessageBox.Show("Yay, hat geklappt (^_^)/", "Update erfolgreich"); break;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ihrgent ein Fehler ist aufgetretten =/", "Fehler");
            }
        }

        private void DeleteLessons()
        {
            try
            {
                HttpResponseMessage response = DeleteLessons(selectLessonsControl.selectedLessons);

                uploadedLessons = GetLessons();
                UpdateLessonsListboxes(uploadedLessons, localLessons);

                switch (response.StatusCode)
                {
                    case HttpStatusCode.NotFound: MessageBox.Show("Mindestens eine der Lektionen wurde nicht gefunden =/", "Fehler"); break;
                    case HttpStatusCode.OK      : MessageBox.Show("Yay, hat geklappt (^_^)/", "Löschen erfolgreich"); break;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ihrgent ein Fehler ist aufgetretten =/", "Fehler");
            }
        }

        #endregion
    }
}
