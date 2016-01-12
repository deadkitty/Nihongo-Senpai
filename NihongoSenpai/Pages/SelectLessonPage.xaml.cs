using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using NihongoSenpai.Data.Database;
using NihongoSenpai.Data;

namespace NihongoSenpai.Pages
{
    public partial class SelectLessonPage : PhoneApplicationPage
    {
        #region Fields

        private String navigationPageString;
        private String pageTypeString;

        #endregion

        #region Constructor
        
        public SelectLessonPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Events
        
        private void loadLessonsButton_Click(object sender, RoutedEventArgs e)
        {
            if (setsListbox.SelectedItem != null)
            {
                DataManager.InitializeAppData(setsListbox.SelectedItem as Lesson);

                NavigationService.Navigate(new Uri(navigationPageString, UriKind.Relative));

                setsListbox.SelectedItem = null;
            }
            else
            {
                MessageBox.Show("Keine Lektion ausgewählt!");
            }
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            //if i go back to main page, close the database connection
            if (e.NavigationMode == NavigationMode.Back)
            {
                DataManager.DeinitializeAppData();
                DataManager.CloseConnection();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            
            //if i come from main Page open database connection and initialize lessons
            if(e.NavigationMode == NavigationMode.New)
            {
                NavigationContext.QueryString.TryGetValue("pageType", out pageTypeString);

                DataManager.ConnectToDatabase();

                List<Lesson> lessons = new List<Lesson>();

                switch (pageTypeString)
                {
                    case "vocab":
                        
                        navigationPageString = "/Pages/ShowVocabPage.xaml";
                        lessons.AddRange(DataManager.GetLessons(Lesson.EType.vocab));
   
                        break;

                    case "kanji": 
                        
                        navigationPageString = "/Pages/SelectKanjiPage.xaml";
                        lessons.AddRange(DataManager.GetLessons(Lesson.EType.kanji));
                
                        break;
                }

                foreach (Lesson lesson in lessons)
                {
                    setsListbox.Items.Add(lesson);
                }
            }
        }

        #endregion
    }
}